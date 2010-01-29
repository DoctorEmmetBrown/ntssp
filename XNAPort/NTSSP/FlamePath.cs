using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayEngine;
using Microsoft.Xna.Framework;
using Utils.Math;

namespace NTSSP
{
    public abstract class FlamePath
    {
        protected Vector2 mMonsterCoord;
        protected Vector2 mPlayerCoord;
        public float t = 0.0f;

        public FlamePath(Vector2 aMonster, Vector2 aPlayer2)
        {
            mMonsterCoord = aMonster;
            mPlayerCoord = aPlayer2;
        }

        public abstract Vector2 At(float t, out double angle);

        public abstract void Draw();
    }

    public class ReflectPath : FlamePath
    {
        private Line Line1, Line2;
        public ReflectPath(Vector2 aMonster, Vector2 aPlayer2):base(aMonster,aPlayer2)
        {
            float x = (aMonster.X + aPlayer2.X)/2.0f;
            Line1 = new Line(aMonster,new Vector2(x,0));
            Line2 = new Line(new Vector2(x, 0),aPlayer2);

            float a = (Line2.P1.Y - Line2.P2.Y)/(Line2.P1.X - Line2.P2.X);
            float b = Line2.P1.Y - a * Line2.P1.X;

            float sign = aMonster.X - aPlayer2.X;

            float nx = (sign > 0) ? -50 : 150;
             
            Line2 = new Line(Line2.P1, new Vector2(nx, a * nx + b));
        }

        public override Vector2 At(float t, out double aAngle)
        {
            float totalSize = Line1.Size + Line2.Size;

            if(t <= (Line1.Size / totalSize))
            {
                Vector2 Direction = Line1.P2 - Line1.P1;
                Direction.Normalize();
                aAngle = Math.Atan2(Direction.Y, Direction.X);
                return Line1.At((t) / (Line1.Size / totalSize));
            }
            else
            {
                Vector2 Direction = Line2.P2 - Line2.P1;
                Direction.Normalize();
                aAngle = Math.Atan2(Direction.Y, Direction.X);
                return Line2.At((t - (Line1.Size / totalSize)) / (Line2.Size / totalSize));
            }
        }
        public override void Draw()
        {
            
        }
    }

    public class ParabolicPath : FlamePath
    {
        private List<Line> mLines = new List<Line>();

        Vector2 g = new Vector2(0,-1);
        Vector2 V0 = new Vector2(0);
        Vector2 V = new Vector2(0);
        Vector2 orig = new Vector2(0);
        private float mTotalSize;
        public ParabolicPath(Vector2 aMonster, Vector2 aPlayer2)
            : base(aMonster, aPlayer2)
        {
            orig = aMonster;
            

            Random r = new Random((int)DateTime.Now.Ticks);

            double t = r.NextDouble();

            float h = MathHelper.Lerp(0, aMonster.Y, 0.8f * (float)t);

            V0.Y =(float) -Math.Sqrt((double)(2.0f*(float)Math.Abs(g.Y)*(aMonster.Y - h)));

            float tpeak = V0.Y/g.Y;

            float delta = V0.Y*V0.Y - 4.0f*(g.Y/2.0f*(-(aMonster.Y - aPlayer2.Y)));

            float t1 = Math.Abs(-V0.Y - (float)Math.Sqrt(Math.Abs(delta))/g.Y);
            float t2 = Math.Abs(-V0.Y + (float) Math.Sqrt(Math.Abs(delta))/g.Y);


            if (t1 > tpeak)
            {
                V0.X = (aPlayer2.X - aMonster.X) / t1;
            }
            else
            {
                V0.X = (aPlayer2.X - aMonster.X) / t2;
            }

            V = V0;
         }

        private Vector2 last;
        public override Vector2 At(float t, out double aAngle)
        {
            Vector2 v = orig + new Vector2(V0.X * t, V0.Y * t - g.Y / 2.0f * t * t);
            if(t == 0.0f)
            {
                aAngle = Math.PI/2.0f;
            }
            else
            {
                Vector2 direction = v - last;
                aAngle = Math.Atan2(direction.Y, direction.X);
            }

            last = v;

            return v;
        }

        public override void Draw()
        {
            //mSpline.Draw(DisplayManager.Instance.ScreenSplitter.SplitScreens.Last());
            //mSpline.DrawControlPoints(DisplayManager.Instance.ScreenSplitter.SplitScreens.Last());
        }
    }

    public class HorizontalPath : FlamePath
    {
        private List<Line> mLines = new List<Line>();
        private float mTotalSize;
        public HorizontalPath(Vector2 aMonster, Vector2 aPlayer2)
            : base(aMonster, aPlayer2)
        {
            float sign = aMonster.X - aPlayer2.X;

            mLines.Add(new Line(aMonster, new Vector2(aPlayer2.X, 0)));
            mLines.Add(new Line(new Vector2(aPlayer2.X, 0),aPlayer2));

            foreach (Line l in mLines)
            {
                mTotalSize = l.Size;
            }
        }

        public override Vector2 At(float t, out double aAngle)
        {
            float totalper = 0;
            int n = 0;
            float per = 0;
            foreach (Line l in mLines)
            {
                per = (float)(l.Size / mTotalSize);
                totalper += per;
                if (t <= totalper)
                {
                    break;
                }
                n += 1;
            }

            if (n > mLines.Count)
            {
                n--;
            }
            totalper -= per;
            float rt = System.Math.Max(System.Math.Min((t - totalper) / per, 1.0f), 0.0f);

            Vector2 Direction = mLines[n].P2 - mLines[n].P1;
            Direction.Normalize();
            aAngle = Math.Atan2(Direction.Y, Direction.X);

            return mLines[n].At(rt);
        }

        public override void Draw()
        {

        }
    }
}
