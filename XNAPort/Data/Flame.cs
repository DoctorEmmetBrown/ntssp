using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayEngine;
using DisplayEngine.Display2D;
using Microsoft.Xna.Framework.Content;

namespace Data
{
    public enum FlameType
    {
        SMALL,
        NORMAL,
        BIG,
        BIGGEST
    }
    public class Flame
    {
        private FlameType mType;

        public FlameType Type
        {
            get { return mType; }
        }
        private int mLife = 0;

        public int Life
        {
            get { return mLife;}
            set { mLife = value;}
        }
        private Sprite mSprite;

        public Sprite Sprite
        {
            get { return mSprite; }
        }

        [ContentSerializerIgnore]
        public float X
        {
            get { return Sprite.X; }
            set { Sprite.X = value/* - DisplayManager.Instance.ScreenSplitter.TranslateWidthFromScreen(Sprite.SpriteSheet.SourceRectangle(0).Width / 2.0f, Sprite.NumScreen)*/; }
        }

        [ContentSerializerIgnore]
        public float Y
        {
            get { return Sprite.Y; }
            set { Sprite.Y = value/* - DisplayManager.Instance.ScreenSplitter.TranslateHeightFromScreen(Sprite.SpriteSheet.SourceRectangle(0).Height / 2.0f, Sprite.NumScreen)*/; }
        }

        public static Sprite GetDefaultFlame(FlameType mType)
        {
            Sprite result = null;
            switch (mType)
            {
                case FlameType.SMALL:
                    result = DisplayManager.Instance.Game.Content.Load<Sprite>("DefaultFlammeSmall");
                    break;
                case FlameType.NORMAL:
                    result = DisplayManager.Instance.Game.Content.Load<Sprite>("DefaultFlammeNormal");
                    break;
                case FlameType.BIG:
                    result = DisplayManager.Instance.Game.Content.Load<Sprite>("DefaultFlammeBig");
                    break;
                case FlameType.BIGGEST:
                    result = DisplayManager.Instance.Game.Content.Load<Sprite>("DefaultFlammeBiggest");
                    break;
            }
            result = new Sprite(result.Speed,result.SpriteSheet);

            return result;
        }

        public Flame(FlameType aType)
        {
            mType = aType;
            mSprite = GetDefaultFlame(aType);

            switch (mType)
            {
                case FlameType.SMALL:
                    mLife = 2;
                    break;
                case FlameType.NORMAL:
                    mLife = 3;
                    break;
                case FlameType.BIG:
                    mLife = 4; 
                    break;
                case FlameType.BIGGEST:
                    mLife = 5; 
                    break;
            }
            
        }
    }
}
