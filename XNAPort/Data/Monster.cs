using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayEngine;
using DisplayEngine.Display2D;
using Microsoft.Xna.Framework.Content;
using Utils;

namespace Data
{
    public class Monster
    {
        private static int mNbInstance = 0;

        [ContentSerializerIgnore]
        private Sprite mSprite = null;

        [ContentSerializerIgnore]
        public Sprite Sprite
        {
            get
            {
                return mSprite;
            }

            set
            {
                mSprite = value;
            }
        }

        [ContentSerializerIgnore]
        private Sprite mExplosion = null;

        [ContentSerializerIgnore]
        public Sprite Explosion
        {
            get
            {
                return mExplosion;
            }

            set
            {
                mExplosion = value;
            }
        }

        [ContentSerializerIgnore]
        public float X
        {
            get { return Sprite.X; }
            set { Sprite.X = value - DisplayManager.Instance.ScreenSplitter.TranslateWidthFromScreen(Sprite.SpriteSheet.SourceRectangle(0).Width/2.0f,Sprite.NumScreen); }
        }

        [ContentSerializerIgnore]
        public float Y
        {
            get { return Sprite.Y; }
            set { Sprite.Y = value - DisplayManager.Instance.ScreenSplitter.TranslateHeightFromScreen(Sprite.SpriteSheet.SourceRectangle(0).Height / 2.0f, Sprite.NumScreen); }
        }

        [ContentSerializer(SharedResource = true)]
        SharedResourceList<Group> mGroups = new SharedResourceList<Group>();

        [ContentSerializerIgnore]
        public List<Group> Groups
        {
            get { return mGroups; }
        }

        [ContentSerializerIgnore]
        private int mGuid = 0;

        [ContentSerializerIgnore]
        public int GUID
        {
            get { return mGuid; }
        }

        [ContentSerializer]
        private int mColor = 0;

        [ContentSerializerIgnore]
        private int mExplosingColor = 0;

        [ContentSerializerIgnore]
        public int ExplosingColor
        {
            get { return mExplosingColor; }
        }

        [ContentSerializerIgnore]
        public int Color
        {
            get{ return mColor; }
            set
            {
                if(value == 0)
                {
                    mExplosingColor = mColor;
                    mExplosion = GetDefaultExplosion(mSize, mColor);
                    mExplosion.Looping = false;
                }

                mColor = value;
                if(mColor == 0)
                {
                    mExplosion.X = X + DisplayManager.Instance.ScreenSplitter.TranslateHeightFromScreen((Sprite.SpriteSheet.SourceRectangle(0).Width / 2.0f) - (Explosion.SpriteSheet.SourceRectangle(0).Width / 2.0f),Sprite.NumScreen);
                    mExplosion.Y = Y + DisplayManager.Instance.ScreenSplitter.TranslateHeightFromScreen((Sprite.SpriteSheet.SourceRectangle(0).Height / 2.0f) - (Explosion.SpriteSheet.SourceRectangle(0).Height / 2.0f),Sprite.NumScreen)  ;
                    mExplosion.Visible = true;
                    mIsExplosing = true;
                    mSprite.Visible = false;    
                }
            }
        }

        [ContentSerializerIgnore]
        private bool mIsExplosing = false;

        [ContentSerializerIgnore]
        public bool IsExplosing
        {
            get { return mIsExplosing; }
            set { mIsExplosing = value;}
        }

        [ContentSerializer]
        private int mSize = 0;

        [ContentSerializerIgnore]
        public int Size
        {
            get { return mSize; }
            set { mSize = value; }
        }

        public Monster()
        {
            mGuid = mNbInstance;
            mNbInstance++;   
        }

        public Monster(int aSize, int aColor)
        {
            mGuid = mNbInstance;
            mNbInstance++;

            mSize = aSize;
            mColor = aColor;

            mSprite = GetDefaultMonster(aSize, aColor);

            DisplayEngine.DisplayManager.Instance.DrawableLevelManager.AddSprite(mSprite, 100, 0);
        }

        public static Sprite GetDefaultExplosion(int aSize, int aColor)
        {
            string name = "E";
            switch (aSize)
            {
                case 1:
                    name += 1;
                    break;
                case 2:
                    name += 2;
                    break;
                case 3:
                    name += 3;
                    break;
                case 4:
                    name += 4;
                    break;
                case 5:
                    name += 5;
                    break;
            }
            switch (aColor)
            {
                case 1:
                    name += "Rouge";
                    break;
                case 2:
                    name += "Jaune";
                    break;
                case 3:
                    name += "Vert";
                    break;
                case 4:
                    name += "Bleu";
                    break;
                case 5:
                    name += "Violet";
                    break;
            }

            Sprite s = DisplayEngine.DisplayManager.Instance.Game.Content.Load<Sprite>(name);
            return new Sprite(s.Speed, s.SpriteSheet);
        }

        public static Sprite GetDefaultMonster(int aSize, int aColor)
        {
            string name = "M";
            switch (aSize)
            {
                case 1:
                    name += 1;
                    break;
                case 2:
                    name += 2;
                    break;
                case 3:
                    name += 3;
                    break;
                case 4:
                    name += 4;
                    break;
                case 5:
                    name += 5;
                    break;
            }
            switch (aColor)
            {
                case 1:
                    name += "Rouge";
                    break;
                case 2:
                    name += "Jaune";
                    break;
                case 3:
                    name += "Vert";
                    break;
                case 4:
                    name += "Bleu";
                    break;
                case 5:
                    name += "Violet";
                    break;
            }

            Sprite s = DisplayEngine.DisplayManager.Instance.Game.Content.Load<Sprite>(name);
            return new Sprite(s.Speed,s.SpriteSheet);
        }

        public string ToString()
        {
            string values = "";

            values += "Monster " + GUID + " ";
            values += "Color : " + Color + " Size : " + Size;

            return values;
        }

    }
}
