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
        private float mX;
        private float mY;
        internal int mNumScreen = 0;

        private bool mVisible = true;
        private int mNumberOfLoop = 0;
        private bool mLooping = true;

        public int NumScreen
        {
            get { return mNumScreen; }
        }
        public bool Looping
        {
            get { return mLooping; }
            set { mLooping = value;}
        }

        public int NumberOfLoop
        {
            get { return mNumberOfLoop; }
            set { mNumberOfLoop = value;}
        }

        public bool Visible
        {
            get { return mVisible; }
            set { mVisible = value; }
        }

        public float X
        {
            get { return mX;  }
            set { mX = value; }
        }

        public float Y
        {
            get { return mY; }
            set { mY = value; }
        }

        private double mRotation = 0;
        public double Rotation
        {
            set { mRotation = value; }
            get { return mRotation; }
        }

        SpriteSheet mSpriteSheet;

        public SpriteSheet SpriteSheet
        {
            get
            {
                return mSpriteSheet;
            }
        }

        double mSpeed = 30;

        public double Speed
        {
            get { return mSpeed; }
            set 
            { 
                mSpeed = value;
                mTargetTime = TimeSpan.FromSeconds(1.0 / mSpeed);
            }
        }

        int mCurrentFrame = 0;

        public Sprite(double aAnimationSpeed, SpriteSheet aSpriteSheet)
        {
            Speed = aAnimationSpeed;
            mSpriteSheet = aSpriteSheet;
        }

        public bool Intersects(Sprite aOther, int aNumScreen)
        {
            if (this.SpriteSheet.Count > 0)
            {
                Rectangle rect = this.SpriteSheet.SourceRectangle(0);
                if(aOther.SpriteSheet.Count > 0)
                {
                    Rectangle nrect = aOther.SpriteSheet.SourceRectangle(0);
                    
                    Vector2 coord1 = new Vector2(mX,mY);
                    Vector2 coord2 = new Vector2(aOther.X, aOther.Y);
                    coord1 = DisplayManager.Instance.ScreenSplitter.TranslateCoordToScreen(coord1,aNumScreen);
                    coord2 = DisplayManager.Instance.ScreenSplitter.TranslateCoordToScreen(coord2, aNumScreen);

                    rect.X = (int)coord1.X;
                    rect.Y = (int)coord1.Y;

                    nrect.X = (int)coord2.X;
                    nrect.Y = (int)coord2.Y;

                    return rect.Intersects(nrect) || rect.Contains(nrect);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return false;

        }

        public bool Contains(Sprite aOther, int aNumScreen)
        {
            if (this.SpriteSheet.Count > 0)
            {
                Rectangle rect = this.SpriteSheet.SourceRectangle(0);
                if (aOther.SpriteSheet.Count > 0)
                {
                    Rectangle nrect = aOther.SpriteSheet.SourceRectangle(0);

                    Vector2 coord1 = new Vector2(mX, mY);
                    Vector2 coord2 = new Vector2(mX, mY);
                    coord1 = DisplayManager.Instance.ScreenSplitter.TranslateCoordToScreen(coord1, aNumScreen);
                    coord2 = DisplayManager.Instance.ScreenSplitter.TranslateCoordToScreen(coord2, aNumScreen);

                    rect.X = (int)coord1.X;
                    rect.Y = (int)coord1.Y;

                    nrect.X = (int)coord2.X;
                    nrect.Y = (int)coord2.Y;

                    return rect.Contains(nrect);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        public void Update(GameTime aGameTime)
        {
            if (mVisible)
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
                        if(!mLooping)
                        {
                            Visible = false;
                        }
                        else
                        {
                            mNumberOfLoop++;
                        }
                        mCurrentFrame = 0;
                    }
                }

            }
        }

        public void Draw(int aNumScreen)
        {
            if (mVisible)
            {
                Vector2 v = DisplayManager.Instance.ScreenSplitter.TranslateCoordToScreen(new Vector2(mX, mY),aNumScreen);
                Rectangle destRect = new Rectangle((int)v.X,(int) v.Y, mSpriteSheet.SourceRectangle(mCurrentFrame).Width, mSpriteSheet.SourceRectangle((int)mCurrentFrame).Height);
                
                Vector2 origins = new Vector2(mSpriteSheet.SourceRectangle(mCurrentFrame).Width/2.0f,mSpriteSheet.SourceRectangle(mCurrentFrame).Height/2.0f);
                
                DisplayManager.Instance.SpriteBatch.Draw(mSpriteSheet.Texture, destRect, mSpriteSheet.SourceRectangle(mCurrentFrame), Color.White,(float)mRotation,origins,SpriteEffects.None,0);
            }
        }

    }
}
