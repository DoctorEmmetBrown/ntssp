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
using DisplayEngine.Utils;
using DisplayEngine.Display2D;

namespace DisplayEngine
{
    public class DisplayManager
    {
        private ScreenSplitter mScreenSplitter;

        public ScreenSplitter ScreenSplitter
        {
            get { return mScreenSplitter; }
        }
        private Game mGame;

        public Game Game
        {
            get
            {
                return mGame;
            }
        }
        private RoundLineManager mRoundLineManager;

        public RoundLineManager RoundLineManager
        {
            get { return mRoundLineManager; }
        }
        private SpriteBatch mSpriteBatch;

        public SpriteBatch SpriteBatch
        {
            get
            {
                return mSpriteBatch;
            }
        }

        private PrimitiveBatch mPrimitiveBatch;

        public PrimitiveBatch PrimitiveBatch
        {
            get
            {
                return mPrimitiveBatch;
            }
        }

        private DrawableLevelManager mDrawLevelManager;

        public DrawableLevelManager DrawableLevelManager
        {
            get
            {
                return mDrawLevelManager;
            }
        }

        private static DisplayManager mInstance = null;

        private FrameRateCounter mFpsCounter = null;

        private DisplayManager(Game aGame, int aNbPlayer)
        {
            mGame = aGame;
            mSpriteBatch = new SpriteBatch(aGame.GraphicsDevice);
            mPrimitiveBatch = new PrimitiveBatch(aGame.GraphicsDevice,aGame);
            mDrawLevelManager = new DrawableLevelManager(aGame);
            mRoundLineManager = new RoundLineManager();
            mRoundLineManager.Init(aGame.GraphicsDevice, aGame.Content);
            Rectangle rect = new Rectangle(0,0,aGame.GraphicsDevice.Viewport.Width,aGame.GraphicsDevice.Viewport.Height);
            mScreenSplitter = new ScreenSplitter(rect, aNbPlayer);

            //mFpsCounter = new FrameRateCounter(mGame);

            //mGame.Components.Add(mFpsCounter);

            mGame.Components.Add(mDrawLevelManager);
        }

        public void SetViewPortSize(int aWidth,int aHeight)
        {
            Viewport view = new Viewport();
            view.X = mGame.GraphicsDevice.Viewport.X;
            view.Y = mGame.GraphicsDevice.Viewport.Y;
            view.Width = aWidth;//mGame.GraphicsDevice.Viewport.Width;
            view.Height = aHeight;//mGame.GraphicsDevice.Viewport.Height;
            //view.AspectRatio = aWidth / aHeight;
            mGame.GraphicsDevice.Viewport = view;
        }

        public static DisplayManager Instance
        {
            get
            {
                return mInstance;
            }
        }

        public static bool CreateManager(Game aGame,int aNbPlayer)
        {
            mInstance = new DisplayManager(aGame, aNbPlayer);

            return true;
        }
    }
}
