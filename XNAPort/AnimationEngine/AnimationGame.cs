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
using Utils.Math;
using DisplayEngine;
using DisplayEngine.Display2D;
using Shapes.Geometry;

namespace AnimationEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class AnimationGame : BaseGame
    {
        public delegate void RenderDelegate();

        RenderDelegate mRenderDelegate = null;

        public RenderDelegate Render
        {
            get { return mRenderDelegate; }
            set
            {
                mRenderDelegate = value;
            }
        }


        public AnimationGame():base()
        {
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            DisplayManager.CreateManager(this);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (mRenderDelegate != null)
            {
                mRenderDelegate();
            }
            /*System.Windows.Forms.Control c = System.Windows.Forms.Control.FromHandle(BaseGame.DrawSurface);
            
            Rectangle rect = new Rectangle(0, 0, 800, 600); ;
            /*if (c != null)
            {
                rect = new Rectangle(0, 0, c.Width, c.Height);
                DisplayManager.Instance.SetViewPortSize(rect.Width, rect.Height);
            }
            else
            {
                rect = new Rectangle(0, 0, DisplayManager.Instance.Game.GraphicsDevice.Viewport.Width, DisplayManager.Instance.Game.GraphicsDevice.Viewport.Height);
                DisplayManager.Instance.SetViewPortSize(rect.Width, rect.Height);
            }
            DisplayManager.Instance.SpriteBatch.Begin();
            BezierSpline bez = new BezierSpline();
            bez.AddCurve(new CubicBezier(new Vector2(50.0f, 0.0f), new Vector2(30.0f, 10.0f), new Vector2(30.0f, 20.0f), new Vector2(10.0f, 100.0f)));



            //rect = new Rectangle(0, 0, 800, 600);
            bez.Draw(rect);
            bez.DrawControlPoints(rect);
            DisplayManager.Instance.SpriteBatch.End();*/
            base.Draw(gameTime);
        }
    }
}
