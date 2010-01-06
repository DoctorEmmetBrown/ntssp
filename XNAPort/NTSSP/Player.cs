using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using DisplayEngine.Display2D;
using DisplayEngine;
using Data;
using Input;

namespace NTSSP
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : GameComponent
    {
        internal List<Sprite> mCurrentShots = new List<Sprite>();
        internal List<Sprite> mChargedAttackCollisions = new List<Sprite>();

        PlayerInputConfiguration mInputConfiguration = new PlayerInputConfiguration();

        private TimeSpan mCharging;
        private int mChargeState = 0;
        private bool mShooting = false;
        public float mX = 20.0f, mY = 20.0f;
        internal bool mSuperAttacking = false;
        private int mNumPlayer;

        private Sprite mCurrentSprite = null;
        private int mLife = 10;

        public int Life
        {
            get { return mLife; }
            set { mLife = value; }
        }

        private Character mCharacter;

        public Character Character
        {
            get { return mCharacter; }
            set 
            { 
                mCharacter = value;

                DisplayManager.Instance.DrawableLevelManager.AddSprite(mCharacter.Idle, 100, mNumPlayer);
                DisplayManager.Instance.DrawableLevelManager.AddSprite(mCharacter.Left, 100, mNumPlayer);
                DisplayManager.Instance.DrawableLevelManager.AddSprite(mCharacter.Right, 100, mNumPlayer);
                mCharacter.Idle.Visible = false;
                mCharacter.Left.Visible = false;
                mCharacter.Right.Visible = false;
            }
        }

        public Player(Game game, int aNumPlayer): base(game)
        {
            mNumPlayer = aNumPlayer;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        internal void RemoveShot(Sprite s)
        {
            mCurrentShots.Remove(s);
            DisplayManager.Instance.DrawableLevelManager.RemoveSprite(50, s, mNumPlayer);
        }

        public void AddShot(float X, float Y, bool isLow)
        {
            Sprite s = (isLow) ? new Sprite(mCharacter.LowShot.Speed, mCharacter.LowShot.SpriteSheet) : new Sprite(mCharacter.Shot.Speed, mCharacter.Shot.SpriteSheet);
            mCurrentShots.Add(s);

            DisplayManager.Instance.DrawableLevelManager.AddSprite(s, 50, mNumPlayer);

            s.X = X;
            s.Y = Y;

            s.Visible = true;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (mCurrentSprite == null)
            {
                mCurrentSprite = mCharacter.Idle;
                mCurrentSprite.Visible = true;
            }

            if (InputManager.Instance.IsKeyDown(mInputConfiguration.Up))
            {
                mCurrentSprite.Visible = false;
                mCurrentSprite = mCharacter.Idle;
                mCurrentSprite.Visible = true;
                mY--;
            }

            if (InputManager.Instance.IsKeyDown(mInputConfiguration.Down))
            {
                mCurrentSprite.Visible = false;
                mCurrentSprite = mCharacter.Idle;
                mCurrentSprite.Visible = true;
                mY++;
            }

            if (InputManager.Instance.IsKeyDown(mInputConfiguration.Left))
            {
                mCurrentSprite.Visible = false;
                mCurrentSprite = mCharacter.Left;
                mCurrentSprite.Visible = true;
                mX++;
            }

            if (InputManager.Instance.IsKeyDown(mInputConfiguration.Right))
            {
                mCurrentSprite.Visible = false;
                mCurrentSprite = mCharacter.Right ;
                mCurrentSprite.Visible = true;
                mX--;
            }

            if (InputManager.Instance.IsKeyDown(mInputConfiguration.Shot) && !mShooting)
            {
                mShooting = true;
                AddShot(mX, mY - DisplayManager.Instance.ScreenSplitter.TranslateHeightFromScreen(mCharacter.Shot.SpriteSheet.SourceRectangle(0).Height,mNumPlayer), false);
                mCharging = new TimeSpan(gameTime.TotalGameTime.Ticks);
                mChargeState = 0;
                mCharging = mCharging.Add(new TimeSpan(0, 0, 1));
            }

            if (InputManager.Instance.IsKeyDown(mInputConfiguration.Shot) && mShooting)
            {
                if (mCharging != new TimeSpan())
                {
                    if (gameTime.TotalGameTime >= mCharging)
                    {
                        mChargeState = Math.Min(mChargeState + 1,3);
                        mCharging = mCharging.Add(new TimeSpan(0, 0, 1));  
                    }
                }
            }

            if (InputManager.Instance.IsKeyUp(mInputConfiguration.Shot) && !mSuperAttacking)
            {
                //Launch Super Attack
                if (mChargeState > 0)
                {
                    mSuperAttacking = true;
                }
                mChargeState = 0;
                mCharging = new TimeSpan();
                mShooting = false;
            }

            if(mSuperAttacking)
            {
                Dictionary<String, Object> dicts = new Dictionary<String, Object>();
                dicts.Add("NumPlayer", mNumPlayer);
                dicts.Add("Coord", new Vector2(mX, mY));
                dicts.Add("Character", mCharacter);

                Dictionary<String, Object> results = new Dictionary<String, Object>();
                if (!mCharacter.ChargedAttackScript.Execute(DisplayManager.Instance.Game, gameTime, dicts, ref results))
                {
                    mSuperAttacking = false;
                }

                mChargedAttackCollisions.AddRange((List<Sprite>)results["Collisions"]);

            }
            mX = Math.Min(Math.Max(mX, 0), 100 - DisplayManager.Instance.ScreenSplitter.TranslateWidthFromScreen(mCharacter.Idle.SpriteSheet.SourceRectangle(0).Width,mNumPlayer));
            mY = Math.Min(Math.Max(mY, 0), 100 - DisplayManager.Instance.ScreenSplitter.TranslateHeightFromScreen(mCharacter.Idle.SpriteSheet.SourceRectangle(0).Height,mNumPlayer));
            mCurrentSprite.X = mX;
            mCurrentSprite.Y = mY;

            List<Sprite> toDelete = new List<Sprite>();

            foreach (Sprite s in mCurrentShots)
            {
                if (s.Y < -DisplayManager.Instance.ScreenSplitter.TranslateHeightFromScreen(mCharacter.Shot.SpriteSheet.SourceRectangle(0).Height,mNumPlayer))
                {
                    toDelete.Add(s);
                }

                s.Y -= 1f;
            }

            foreach (Sprite s in toDelete)
            {
                RemoveShot(s);
            }

            base.Update(gameTime);
        }
    }
}