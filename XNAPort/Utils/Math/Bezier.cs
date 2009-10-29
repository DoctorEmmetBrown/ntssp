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
namespace Utils.Math
{
    public class CubicBezier
    {
        private Vector2[] mPoints = new Vector2[4];
        private double mLength;
        private double q1, q2, q3, q4, q5;

        public Vector2[] Points
        {
            get { return mPoints; }
        }

        public double Length
        {
            get { return mLength; }
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
        double BezierArcLength()
        {
            float size = 0.0f;
            for (float i = 0.0f; i < 1.0f - 0.01f; i+= 0.01f)
            {
                Vector2 p1 = GetPoint(i);
                Vector2 p2 = GetPoint(i+0.01f);
                size += Vector2.Distance(p1, p2);
            }
            return size;

            /*
            double k1X = 0, k1Y = 0, k2X = 0, k2Y = 0, k3X = 0, k3Y = 0, k4X = 0, k4Y = 0;
            
            k1X = -mPoints[0].X + 3*(mPoints[1].X - mPoints[2].X) + mPoints[3].X;
            k2X = 3*(mPoints[0].X + mPoints[2].X) - 6*mPoints[1].X;
            k3X = 3*(mPoints[1].X - mPoints[0].X);
            k4X = mPoints[0].X;
            
            k1Y = -mPoints[0].Y + 3*(mPoints[1].Y - mPoints[2].Y) + mPoints[3].Y;
            k2Y = 3*(mPoints[0].Y + mPoints[2].Y) - 6*mPoints[1].Y;
            k3Y = 3*(mPoints[1].Y- mPoints[0].Y);
            k4Y = mPoints[0].Y;
        
            q1 = 9.0*(sqr(k1X) + sqr(k1Y));
            q2 = 12.0*(k1X*k2X + k1Y*k2Y);
            q3 = 3.0*(k1X*k3X + k1Y*k3Y) + 4.0*(sqr(k2X) + sqr(k2Y));
            q4 = 4.0*(k2X*k3X + k2Y*k3Y);
            q5 = sqr(k3X) + sqr(k3Y);
        
            return Simpson( 0, 1, 1024, 0.001);*/
        }

        public Vector2 GetPoint(float t)
        {
            float x = -1;
            float y = -1;
            if (t >= 0 && t <= 1)
            {
                float t2 = t * t;
                float t3 = t2 * t;

                float a = (-t3 + 3 * t2 - 3 * t + 1);
                float b = (3 * t3 - 6 * t2 + 3 * t);
                float c = (3 * t2 - 3 * t3);
                float d = t3;

                x = a*mPoints[0].X + b*mPoints[1].X + c*mPoints[2].X + d*mPoints[3].X;
                y = a*mPoints[0].Y + b*mPoints[1].Y + c*mPoints[2].Y + d*mPoints[3].Y;
            }
            return new Vector2(x,y);
        }
    }

    public class BezierSpline
    {
        private static int mNbInstance = 0;

        private int mGuid = 0;

        public int GUID
        {
            get { return mGuid; }
        }

        private List<CubicBezier> mCurves = new List<CubicBezier>();
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
            Update();
        }

        void Update()
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
                if (t < totalper)
                {
                    break;
                }
                n += 1;
            }

            if (t == 1.0f)
            {
                n -= 1;
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
