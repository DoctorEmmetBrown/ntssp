using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public abstract Vector2 At(float t);
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

            Line2 = new Line(Line2.P1,new Vector2(-50.0f,a * (-50.0f) + b));
        }

        public override Vector2 At(float t)
        {
            float totalSize = Line1.Size + Line2.Size;

            if(t <= (Line1.Size / totalSize))
            {
                return Line1.At((t - (Line1.Size / totalSize)) / (Line1.Size / totalSize));
            }
            else
            {
                return Line2.At((t - 1.0f) / (Line2.Size / totalSize));
            }
        }
    }

    public class ParabolicPath : FlamePath
    {
        private CubicBezier mSpline;
        public ParabolicPath(Vector2 aMonster, Vector2 aPlayer2)
            : base(aMonster, aPlayer2)
        {
            float x = (aMonster.X + aPlayer2.X) / 2.0f;
            float t = Math.Abs(aMonster.X - aPlayer2.X);
            float sign = aMonster.X - aPlayer2.X;
            sign /= sign;

            Vector2 vect = new Vector2(x,0);

            float a = (vect.Y - aPlayer2.Y) / (vect.X - aPlayer2.X);
            float b = vect.Y - a * vect.X;

            mSpline = new CubicBezier(aMonster, new Vector2(x + (sign * t / 3.0f), aMonster.Y / 3.0f), new Vector2(x - (sign * t / 3.0f), aMonster.Y / 3.0f), new Vector2(-50.0f, a * (-50.0f) + b));

            mSpline.Update();
        }

        public override Vector2 At(float t)
        {
            return mSpline.GetPoint(t);
        }
    }

    public class HorizontalPath : FlamePath
    {
        private CubicBezier mSpline;
        public HorizontalPath(Vector2 aMonster, Vector2 aPlayer2)
            : base(aMonster, aPlayer2)
        {
            float x = (aMonster.X + aPlayer2.X) / 2.0f;
            float t = Math.Abs(aMonster.X - aPlayer2.X);
            float sign = aMonster.X - aPlayer2.X;
            sign /= sign;

            Vector2 vect = new Vector2(x, 0);

            float a = (vect.Y - aPlayer2.Y) / (vect.X - aPlayer2.X);
            float b = vect.Y - a * vect.X;

            mSpline = new CubicBezier(aMonster, new Vector2(aPlayer2.X, 0), new Vector2(aPlayer2.X, aPlayer2.Y / 2.0f), new Vector2(-50.0f, a * (-50.0f) + b) );
            mSpline.Update();
        }

        public override Vector2 At(float t)
        {
            return mSpline.GetPoint(t);
        }
    }
}
