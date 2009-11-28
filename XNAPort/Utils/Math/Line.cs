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
    public class Line
    {
        Vector2 mP1, mP2;

        public Vector2 P1
        {
            get { return mP1; }
        }

        public Vector2 P2
        {
            get
            {
                return mP2;
            }
        }
        public Line(Vector2 aP1, Vector2 aP2)
        {
            mP1 = aP1;
            mP2 = aP2;
        }

        public Vector2 At(float t)
        {
            float vx = mP2.X - mP1.X;
            float vy = mP2.Y - mP1.Y;

            return new Vector2(mP1.X + vx * t,mP1.Y + vy * t);
        }
    }
}
