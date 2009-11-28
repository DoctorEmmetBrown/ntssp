using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public int X
        {
            get { return Sprite.X; }
            set { Sprite.X = value; }
        }

        [ContentSerializerIgnore]
        public int Y
        {
            get { return Sprite.Y; }
            set { Sprite.Y = value; }
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
        public int Color
        {
            get{ return mColor; }
            set{ mColor = value;}
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

            DisplayEngine.DisplayManager.Instance.DrawableLevelManager.AddSprite(mSprite, 100);
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
