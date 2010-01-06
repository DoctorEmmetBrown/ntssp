using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayEngine.Display2D;
using Microsoft.Xna.Framework.Content;
using Utils;
using ScriptEngine;

namespace Data
{
    public class Character
    {
        Sprite mIdle;

        public Sprite Idle
        {
            get { return mIdle; }
            set { mIdle = value; }
        }

        Sprite mLeft;

        public Sprite Left
        {
            get { return mLeft; }
            set { mLeft = value; }
        }

        Sprite mRight;

        public Sprite Right
        {
            get { return mRight; }
            set { mRight = value; }
        }

        Sprite mShot;

        public Sprite Shot
        {
            get { return mShot; }
            set { mShot = value; }
        }

        Sprite mLowShot;

        public Sprite LowShot
        {
            get { return mLowShot; }
            set { mLowShot = value; }
        }

        Dictionary<String, Sprite> mChargedAttackSprites = new Dictionary<String, Sprite>();

        public Dictionary<String, Sprite> ChargedAttackSprites
        {
            get { return mChargedAttackSprites; }
        }

        Script mChargedAttackScript;

        public Script ChargedAttackScript
        {
            get { return mChargedAttackScript; }
            set { mChargedAttackScript = value; }
        }
    }
}
