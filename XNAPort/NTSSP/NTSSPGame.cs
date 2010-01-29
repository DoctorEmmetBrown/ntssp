using System;
using System.Collections.Generic;
using System.Linq;
using Data;
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
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework.GamerServices;
using Input;
using DefaultScript;

namespace NTSSP
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class NTSSPGame : BaseGame
    {
        Sprite mSprite;
        GraphicsDeviceManager mGraphics;
        private List<ContentManager> mCopyContentManager = new List<ContentManager>();
        private Formation mTestFormation;
        private Level mLevel, mLevel2;
        private LevelFlow mLevelFlow;
        private LevelFlow mLevelFlow2;
        private Character mTestCharacter;
        private Player mPlayer, mPlayer2;
        private CollisionManager mCollisionManager, mCollisionManager2;
        private static NTSSPGame mInstance;
        public NTSSPGame() : base()
        {
            mInstance = this;
            Content.RootDirectory = "Content";
            mCopyContentManager.Add(new ContentManager(Services));
            mCopyContentManager[0].RootDirectory = "Content";
        }

        public static NTSSPGame Instance
        {
            get { return mInstance; }
        }

        public Player GetPlayer(int aNum)
        {
            if(aNum == 0)
            {
                return mPlayer;
            }
            else
            {
                return mPlayer2;
            }
            
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
            InputManager.CreateInstance(this);
            DisplayManager.CreateManager(this, 2);
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            

            /*FileStream myStream = new FileStream("..\\..\\..\\Content\\testFormation.xml",FileMode.Open);
            XmlReader reader = XmlReader.Create(myStream);
            mTestFormation = IntermediateSerializer.Deserialize<Formation>(reader, null);

            reader.Close();
            myStream.Close();*/

            mTestCharacter = Content.Load<Character>("TestCharacter");

            mTestCharacter.ChargedAttackScript = new DefaultSuperAttack();

            mPlayer = new Player(this,0);

            mPlayer2 = new Player(this,1);

            mPlayer2.Character = mTestCharacter;

            mPlayer.Character = mTestCharacter;

            Components.Add(mPlayer2);

            Components.Add(mPlayer);

            mTestFormation = Content.Load<Formation>("testFormation");

            mLevel = new Level();

            mLevel2 = new Level();

            mLevel2.Formations.Add(mCopyContentManager[0].Load<Formation>("testFormation"));
            mLevel.Formations.Add(mTestFormation);

            mLevelFlow = new LevelFlow(mLevel,0);
            mLevelFlow2 = new LevelFlow(mLevel2,1);

            mCollisionManager = new CollisionManager(mPlayer,mLevelFlow,0);
            mCollisionManager2 = new CollisionManager(mPlayer2,mLevelFlow2,1);

            base.LoadContent();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                this.Exit();

            mLevelFlow.Update(aGameTime);

            mLevelFlow2.Update(aGameTime);

            mCollisionManager.Update(aGameTime);

            mCollisionManager2.Update(aGameTime);

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

            mCollisionManager.Draw();

            DisplayManager.Instance.SpriteBatch.End();
        }
    }
}
