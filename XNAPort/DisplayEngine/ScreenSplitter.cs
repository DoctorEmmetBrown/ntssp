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

namespace DisplayEngine
{
    public class ScreenSplitter
    {
        private const int BAR_SIZE = 10;
        private Rectangle mGlobalScreen;

        private List<Rectangle> mSplitScreens = new List<Rectangle>();
        public List<Rectangle> SplitScreens
        {
            get { return mSplitScreens; }
        }

        public ScreenSplitter(Rectangle aScreen, int aNbPlayer)
        {
            int w = aScreen.Width/aNbPlayer;

            if (aNbPlayer > 1)
            {
                w -= BAR_SIZE/(aNbPlayer - 1);
            }

            for(int i=0;i<aNbPlayer;i++)
            {
                Rectangle res;
                if (aNbPlayer > 1)
                {
                    res = new Rectangle(aScreen.X + i*(w + BAR_SIZE/(aNbPlayer - 1)), aScreen.Y, w, aScreen.Height);
                }
                else
                {
                    res = new Rectangle(aScreen.X , aScreen.Y, w, aScreen.Height);

                }
                mSplitScreens.Add(res);
            }

            mSplitScreens.Add(aScreen);
        }

        public float TranslateHeightFromScreen(float aH, int aNumPlayer)
        {
            float r = aH / (mSplitScreens[aNumPlayer].Height) * 100.0f;
            return r;
        }

        public float TranslateWidthFromScreen(float aW, int aNumPlayer)
        {
            float r = aW / (mSplitScreens[aNumPlayer].Width) * 100.0f;
            return r;
        }

        public Vector2 TranslateCoordToScreen(Vector2 aVect,int aNumPlayer)
        {
            float x = mSplitScreens[aNumPlayer].X + (aVect.X / 100.0f) * (mSplitScreens[aNumPlayer].Width);
            float y = mSplitScreens[aNumPlayer].Y + (aVect.Y / 100.0f) * (mSplitScreens[aNumPlayer].Height);

            return new Vector2(x, y);
        }

        public Vector2 TranslateCoordFromScreen(Vector2 aVect, int aNumPlayer)
        {
            float x = (aVect.X - mSplitScreens[aNumPlayer].X)/ (mSplitScreens[aNumPlayer].Width) * 100.0f;
            float y = (aVect.Y - mSplitScreens[aNumPlayer].Y)/ (mSplitScreens[aNumPlayer].Height) * 100.0f;

            return new Vector2(x, y);
        }
    }
}
