using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DisplayEngine.Display2D
{
    public class BackgroundTile
    {
        private int mX;
        private int mY;
        private Texture2D mTex;

        public int X
        {
            get { return mX; }
            set { mX = value; }
        }

        public int Y
        {
            get { return mY; }
            set { mY = value; }
        }

        public Texture2D Tex
        {
            get { return mTex; }
            set { mTex = value; }
        }
    }
    public class Background : IDrawable2D
    {
        private List<Rectangle> mRects = new List<Rectangle>();

        TimeSpan mLastTime = TimeSpan.Zero;

        TimeSpan mTargetTime = TimeSpan.FromSeconds(1 / 30.0);

        private int mY = 0;

        private BackgroundTile[][] mTiles;

        internal BackgroundTile[][] Tiles
        {
            get { return mTiles; }
            set { mTiles = value; }
        }

        internal int mWidth = 0;

        public int Width
        {
            get { return mWidth; }
        }

        internal int mHeight = 0;

        public int Height
        {
            get { return mHeight; }
        }

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

        private void Scroll()
        {
            double ratio = ((double)DisplayManager.Instance.Game.GraphicsDevice.Viewport.Width) / ((double)mWidth);
            int Height = (int)(((double)DisplayManager.Instance.Game.GraphicsDevice.Viewport.Height) / ratio);

            mRects.Clear();

            if(mY <= -Height)
            {
                mY += mHeight;
            }
            if(mY < 0)
            {
                Rectangle r = new Rectangle(0,0,mWidth,0);
                r.Y = mHeight + mY;
                r.Height = mHeight - r.Y;
                mRects.Add(r);

                Rectangle r1 = new Rectangle(0, 0, mWidth, Height - r.Height);
                mRects.Add(r1);
            }
            else
            {
                Rectangle r = new Rectangle(0, mY, mWidth, Height);
                mRects.Add(r);
            }
            mY -= 10;
        }
        public void Update(GameTime gameTime)
        {
            if (mLastTime == TimeSpan.Zero)
            {
                mLastTime = gameTime.TotalGameTime;

            }
            if (gameTime.TotalGameTime > (mLastTime + mTargetTime))
            {
                mLastTime = gameTime.TotalGameTime;
                Scroll();
            }
        }

        public void DrawRect(Rectangle aSrc, Vector2 aDst)
        {
            double ratio = ((double)DisplayManager.Instance.Game.GraphicsDevice.Viewport.Width) / ((double)mWidth);

            int h = 0;

            if (mTiles.Length > 0)
            {
                h = aSrc.Y/(mHeight/mTiles[0].Length);

                int bottom = Math.Min(aSrc.Bottom/(mHeight/mTiles[0].Length), mTiles[0].Length - 1);

                for (int j = 0; j < mTiles.Length; j++)
                {
                    int height = (int) (aDst.Y*ratio);
                    for (int i = h; i <= bottom; i++)
                    {
                        Rectangle rect = new Rectangle(0, 0, mTiles[j][i].Tex.Width, 0);
                        if (i == h)
                        {
                            rect.Y = aSrc.Y - mTiles[j][i].Y;
                        }
                        if (i == bottom)
                        {
                            rect.Height = (aSrc.Bottom - mTiles[j][i].Y) - rect.Y;
                        }
                        else
                        {
                            rect.Height = mTiles[j][i].Tex.Height - rect.Y;
                        }

                        Rectangle dest = new Rectangle((int) ((double) mTiles[j][i].X*ratio), height,
                                                       (int) (((double) rect.Width)*ratio),
                                                       (int) (((double) rect.Height)*ratio));
                        DisplayManager.Instance.SpriteBatch.Draw(mTiles[j][i].Tex, dest, rect, Color.White);
                        height += (int) ((double) rect.Height*ratio);
                    }
                }
            }
        }

        public void Draw(int aNumScreen)
        {
            int height = 0;
            foreach (Rectangle r in mRects)
            {
                DrawRect(r, new Vector2(0, height));
                height += r.Height;
            }
        }
    }
}
