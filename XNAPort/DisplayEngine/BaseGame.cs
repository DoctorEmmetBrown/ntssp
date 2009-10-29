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

namespace DisplayEngine
{
    public class BaseGame : Game
    {
        protected GraphicsDeviceManager mGraphics;
        private static IntPtr mDrawSurface = new IntPtr(0);

        public static IntPtr DrawSurface
        {
            get
            {
                return mDrawSurface;
            }

            set
            {
            	mDrawSurface = value;
            }
        }
        //Create and Embed Graphics in a window Handle
        public BaseGame() : base()
        {
            mGraphics = new GraphicsDeviceManager(this);
            mGraphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(PreparingDeviceSettingsEvent);
            System.Windows.Forms.Control.FromHandle((this.Window.Handle)).VisibleChanged += new EventHandler(VisibleChanged);

        }

        private void VisibleChanged(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible == true)
                System.Windows.Forms.Control.FromHandle((this.Window.Handle)).Visible = false;
        }

        private void PreparingDeviceSettingsEvent(object sender, PreparingDeviceSettingsEventArgs aEvent)
        {
            if (mDrawSurface.ToInt32() != 0)
            {
                aEvent.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = mDrawSurface;
            }
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
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="aGameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="aGameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime aGameTime)
        {
            base.Draw(aGameTime);
        }
    }
}
