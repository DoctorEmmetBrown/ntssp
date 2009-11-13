using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayEngine.Display2D;

namespace Data
{
    public class Monster
    {
        private static int mNbInstance = 0;


        private Sprite mSprite;

        public Sprite Sprite
        {
            get { return mSprite; }
        }

        public int X
        {
            get{ return mSprite.X;}
            set { mSprite.X = value; }
        }

        public int Y
        {
            get { return mSprite.Y; }
            set { mSprite.Y = value; }
        }

        List<Group> mGroups = new List<Group>();

        public List<Group> Groups
        {
            get { return mGroups; }
        }


        private int mGuid = 0;

        public int GUID
        {
            get { return mGuid; }
        }

        private int mColor = 0;

        public int Color
        {
            get{ return mColor; }
            set{ mColor = value;}
        }

        private int mSize = 0;

        public int Size
        {
            get { return mSize; }
            set { mSize = value; }
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
