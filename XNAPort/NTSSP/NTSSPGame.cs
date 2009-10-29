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

namespace NTSSP
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class NTSSPGame : BaseGame
    {
        Sprite mSprite;
        GraphicsDeviceManager mGraphics;

        public NTSSPGame()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            DisplayManager.CreateManager(this);
        }

        public GraphicsDeviceManager Graphics
        {
            get { return mGraphics; }
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
            mSprite = Content.Load<Sprite>("XMLFile1");

            Background back = Content.Load<Background>("Background");

            DisplayManager.Instance.DrawableLevelManager.AddSprite((IDrawable2D) back, 10);
            DisplayManager.Instance.DrawableLevelManager.AddSprite((IDrawable2D)mSprite, 1);
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
        /// <param name="aGameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime aGameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(aGameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="aGameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime aGameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DisplayManager.Instance.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            // TODO: Add your drawing code here

            base.Draw(aGameTime);

            DisplayManager.Instance.SpriteBatch.End();
        }
    }
}
