using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using DisplayEngine;
using DisplayEngine.Display2D;
using System.Threading;
namespace Utils.Math
{
    public class CubicBezier
    {
        [ContentSerializer(SharedResource = true)]
        internal BezierSpline mParent = null;

        [ContentSerializer]
        private Vector2[] mPoints = new Vector2[4];

        [ContentSerializer]
        private double mLength;

        [ContentSerializer]
        float[] mLengths = new float[3];

        [ContentSerializerIgnore]
        private double q1, q2, q3, q4, q5;

        public Vector2[] Points
        {
            get { return mPoints; }
        }

        public double Length
        {
            get { return mLength; }
        }

        public CubicBezier()
        {
        }

        public CubicBezier(Vector2 aP1, Vector2 aP2, Vector2 aP3, Vector2 aP4)
        {
            mPoints[0] = aP1;
            mPoints[1] = aP2;
            mPoints[2] = aP3;
            mPoints[3] = aP4;

            mLength = 0;
        }

        public void Update()
        {
            mLength = BezierArcLength();
        }
        private double balf(double t)
        {
            double result = q5 + t*(q4 + t*(q3 + t*(q2 + t*q1)));
            result = System.Math.Sqrt(result);
            return result;
        }


        private double Simpson(double a, double b, double n_limit, double tolerance)
        {
            int n = 1;
            double multiplier = (b - a)/6.0;
            double endsum = balf(a) + balf(b);
            double interval = (b - a)/2.0;
            double asum = 0.0;
            double bsum = balf(a + interval);
            double est1 = multiplier*(endsum + (2.0*asum) + (4.0*bsum));
            double est0 = 2.0*est1;
            while(n < n_limit && System.Math.Abs(est1 - est0) > tolerance)
            {
                n *= 2;
                multiplier /= 2.0;
                interval /= 2.0;
                asum += bsum;
                bsum = 0.0;
                est0 = est1;
                for(int i = 1; i < n; i+=2)
                {
                    bsum += balf(a + (i*interval));
                    est1 = multiplier*(endsum + (2.0*asum) + (4.0*bsum));
                }
            }
            return est1;
        }
        private double sqr(double d)
        {
            return d*d;
        }

        float[, ,] mTable = new float[3, 1001, 2];
        double BezierArcLength()
        {
            float size = 0.0f;
            mTable[0, 0, 0] = 0.0f;
            mTable[0, 0, 1] = 0.0f;
            int j = 1;
            for (float i = 0.0f; i < 1.0f - 0.001f; i += 0.001f)
            {
                Vector2 p1 = Vector2.CatmullRom(mPoints[0],mPoints[0],mPoints[1],mPoints[2],i);
                Vector2 p2 = Vector2.CatmullRom(mPoints[0], mPoints[0], mPoints[1], mPoints[2], i + 0.001f);
                
                size += Vector2.Distance(p1, p2);

                mTable[0, j, 0] = size;
                mTable[0, j, 1] = i + 0.001f;

                j++;
            }

            mLengths[0] = size;
            mTable[1, 0, 0] = 0.0f;
            mTable[1, 0, 1] = 0.0f;
            j = 1;
            for (float i = 0.0f; i < 1.0f - 0.001f; i += 0.001f)
            {
                Vector2 p1 = Vector2.CatmullRom(mPoints[0], mPoints[1], mPoints[2], mPoints[3], i);
                Vector2 p2 = Vector2.CatmullRom(mPoints[0], mPoints[1], mPoints[2], mPoints[3], i + 0.001f);
                size += Vector2.Distance(p1, p2);

                mTable[1, j, 0] = size - mLengths[0];
                mTable[1, j, 1] = i + 0.001f;

                j++;
            }

            mLengths[1] = size - mLengths[0];
            mTable[2, 0, 0] = 0.0f;
            mTable[2, 0, 1] = 0.0f;
            j = 1;
            for (float i = 0.0f; i < 1.0f - 0.001f; i += 0.001f)
            {
                Vector2 p1 = Vector2.CatmullRom(mPoints[1], mPoints[2], mPoints[3], mPoints[3], i);
                Vector2 p2 = Vector2.CatmullRom(mPoints[1], mPoints[2], mPoints[3], mPoints[3], i + 0.001f);
                size += Vector2.Distance(p1, p2);

                mTable[2, j, 0] = size - mLengths[0] - mLengths[1];
                mTable[2, j, 1] = i + 0.001f;

                j++;
            }

            mLengths[2] = size - mLengths[1] - mLengths[0];

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 1001; k++)
                {
                    mTable[i, k, 0] = mTable[i, k, 0] / mLengths[i];
                }
            }

            return size;
        }

        public float GetSegPoint(float t, int i)
        {
            for(int j=0;j<1001;j++)
            {
                if(mTable[i,j,0] > t)
                {
                    return mTable[i,j - 1 , 1] + (t - mTable[i,j-1,0])/(mTable[i,j,0] - mTable[i,j-1,0]) * (mTable[i,j,1] - mTable[i,j-1,1]);
                }
            }
            return 0.0f;
        }
        public Vector2 GetPoint(float t)
        {
            float totalper = 0;
            int seg = 0;
            float per = 0.0f;
            for (int i = 0; i < 3; i++)
            {
                per = mLengths[i] / (float)mLength;
                totalper += per;
                if (t <= totalper)
                {
                    break;
                }
                seg++;
            }

            if (seg > 2)
            {
                seg--;
            }
            totalper -= per;
            float rt = System.Math.Max(System.Math.Min((t - totalper) / per, 1.0f), 0.0f);

            Vector2 firstpoint = mPoints[0];
            Vector2 lastpoint = mPoints[3];

            if (mParent != null)
            {
                for (int i = 0; i < mParent.CurveCount; i++)
                {
                    if (mParent.GetCurve(i) == this)
                    {
                        if (i > 0)
                        {
                            firstpoint = mParent.GetCurve(i - 1).Points[2];
                        }
                        if (i < mParent.CurveCount - 1)
                        {
                            lastpoint = mParent.GetCurve(i + 1).Points[1];
                        }
                    }
                }
            }
            switch (seg)
            {
                case 0:
                    return Vector2.CatmullRom(firstpoint, mPoints[0], mPoints[1], mPoints[2], GetSegPoint(rt, 0));
                case 1:
                    return Vector2.CatmullRom(mPoints[0], mPoints[1], mPoints[2], mPoints[3], GetSegPoint(rt,1));
                case 2:
                    return Vector2.CatmullRom(mPoints[1], mPoints[2], mPoints[3], lastpoint, GetSegPoint(rt, 2));
            }

            return new Vector2();
        }
    }

    public class BezierSpline
    {
        private static int mNbInstance = 0;

        [ContentSerializerIgnore]
        private int mGuid = 0;

        [ContentSerializerIgnore]
        public int GUID
        {
            get { return mGuid; }
        }

        [ContentSerializer]
        private SharedResourceList<CubicBezier> mCurves = new SharedResourceList<CubicBezier>();

        [ContentSerializer]
        private double mLength = 0;

        public int CurveCount
        {
            get{ return mCurves.Count; }
        }

        public BezierSpline()
        {
            mGuid = mNbInstance;
            mNbInstance++;
        }

        public CubicBezier GetCurve(int index)
        {
            if(index >= 0 && index< CurveCount)
            {
                return mCurves[index];
            }
            return null;
        }

        public void AddCurve(CubicBezier curve)
        {
            if (CurveCount> 0)
            {
                if (mCurves.Last().Points[3] != curve.Points[0])
                {
                    return;
                }
            }
            mCurves.Add(curve);
            curve.mParent = this;
            Update();
        }

        public void Update()
        {
            double total = 0;
            foreach(CubicBezier c in mCurves)
            {
                c.Update();
                total += c.Length;
            }
            mLength = total;
        }

        public double Length
        {
            get { return mLength; }
        }
        
        public Vector2 getPoint(float t)
        {
            float totalper = 0;
            int n = 0;
            float per = 0;
            foreach (CubicBezier c in mCurves)
            {
                per = (float)(c.Length/Length);
                totalper += per;
                if (t <= totalper)
                {
                    break;
                }
                n += 1;
            }

            if (n > CurveCount)
            {
                n--;
            }
            totalper -= per;
            float rt = System.Math.Max(System.Math.Min((t - totalper)/per,1.0f),0.0f);
            return mCurves[n].GetPoint(rt);
        }


        private Vector2 translanteCoord(Vector2 aVect, Rectangle screenRect)
        {
            Vector2 res;
            res.X = screenRect.X + (aVect.X / 100.0f) * screenRect.Width;
            res.Y = screenRect.Y + (aVect.Y / 100.0f) * screenRect.Height;

            return res;
        }
        public void DrawControlPoints(Rectangle screenRect)
        {
            BasicPrimitives primitive = new BasicPrimitives(DisplayManager.Instance.Game.GraphicsDevice);

            for(int i=0;i<mCurves.Count;i++)
            {
                for(int j=0;j<mCurves[i].Points.Length - 1;j++)
                {
                    primitive.CreateLine(translanteCoord(mCurves[i].Points[j],screenRect),translanteCoord(mCurves[i].Points[j + 1],screenRect));
                    primitive.Thickness = 2.0f;
                    primitive.Colour = Color.Red;
                    primitive.RenderLinePrimitive(DisplayManager.Instance.SpriteBatch);
                }
            }

            for (int i = 0; i < mCurves.Count; i++)
            {
                for (int j = 0; j < mCurves[i].Points.Length; j++)
                {
                    if (j != 0 || i == 0)
                    {
                        primitive.Colour = Color.Green;
                        primitive.CreateCircle(10, 10);
                        primitive.Position = translanteCoord(new Vector2(mCurves[i].Points[j].X, mCurves[i].Points[j].Y),screenRect);
                        primitive.RenderRoundPrimitive(DisplayManager.Instance.SpriteBatch);
                    }
                }
            }

        }
        public void Draw(Rectangle screenRect)
        {

            BasicPrimitives primitive = new BasicPrimitives(DisplayManager.Instance.Game.GraphicsDevice);

            double t = 0.0;
            int j = 0;
            for(double i=0.00;(float)i<1.0f;i+=0.001)
            {
                if ((float)(i + 0.001) <= 1.0f)
                {
                    primitive.CreateLine(translanteCoord(getPoint((float)i), screenRect), translanteCoord(getPoint((float)(i + 0.001)), screenRect));
                    primitive.Thickness = 2.0f;
                    primitive.RenderLinePrimitive(DisplayManager.Instance.SpriteBatch);
                }
            }
        }
        

    }
}
