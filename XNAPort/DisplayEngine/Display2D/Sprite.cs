using System;
using System.Collections.Generic;
using System.Text;
using DisplayEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace DisplayEngine.Display2D
{
    public class Sprite : IDrawable2D
    {

        TimeSpan mLastTime = TimeSpan.Zero;
        TimeSpan mTargetTime = TimeSpan.FromSeconds(1 / 15.0);
        private int mX;
        private int mY;

        public int X
        {
            get { return mX;  }
            set { mX = value; }
        }

        public int Y
        {
            get { return mY; }
            set { mY = value; }
        }

        SpriteSheet mSpriteSheet;

        double mSpeed = 30;

        public double Speed
        {
            get { return mSpeed; }
            set 
            { 
                mSpeed = value;
                mTargetTime = TimeSpan.FromSeconds(1 / mSpeed);
            }
        }

        int mCurrentFrame = 0;

        public Sprite(double aAnimationSpeed, SpriteSheet aSpriteSheet)
        {
            mSpeed = aAnimationSpeed;
            mSpriteSheet = aSpriteSheet;
        }

        public void Update(GameTime aGameTime)
        {
            if (mLastTime == TimeSpan.Zero)
            {
                mLastTime = aGameTime.TotalGameTime;

            }
            if (aGameTime.TotalGameTime > (mLastTime + mTargetTime))
            {
                mCurrentFrame++;

                mLastTime = aGameTime.TotalGameTime;

                if (mCurrentFrame == mSpriteSheet.Count)
                {
                    mCurrentFrame = 0;
                }
            }


        }

        public void Draw()
        {
            Rectangle destRect = new Rectangle(mX,mY,mSpriteSheet.SourceRectangle(mCurrentFrame).Width,mSpriteSheet.SourceRectangle((int) mCurrentFrame).Height);
            DisplayManager.Instance.SpriteBatch.Draw(mSpriteSheet.Texture, destRect, mSpriteSheet.SourceRectangle(mCurrentFrame), Color.White);
        }

    }
}
