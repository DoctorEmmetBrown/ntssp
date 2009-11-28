using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimationEngine;
using Data;
using Microsoft.Xna.Framework;
using AnimationEngine;
namespace NTSSP
{
    class LevelFlow
    {
        private Level mLevel = null;
        private Formation mCurrentFormation = null;
        private Engine mCurrentEngine = null;
        private int mIndex = 0;

        public LevelFlow(Level aLevel)
        {
            mLevel = aLevel;
        }


        public void Update(GameTime aGameTime)
        {
            if(mCurrentFormation == null)
            {
                if (mLevel.Formations.Count > 0)
                {
                    mCurrentFormation = mLevel.Formations[mIndex];

                    foreach(Monster m in mLevel.Formations[mIndex].Monsters)
                    {
                        m.Sprite = Monster.GetDefaultMonster(m.Size, m.Color);
                        DisplayEngine.DisplayManager.Instance.DrawableLevelManager.AddSprite(m.Sprite, 100);
                    }
                }
                if(mCurrentFormation == null)
                {
                    return;
                }
            }

            if (mCurrentEngine == null)
            {
                mCurrentEngine = new Engine(mCurrentFormation);
            }

            if(!mCurrentEngine.GlobalAnimation(aGameTime))
            {
                foreach (Monster m in mCurrentFormation.Monsters)
                {
                    DisplayEngine.DisplayManager.Instance.DrawableLevelManager.RemoveSprite(100,m.Sprite);
                }

                mCurrentFormation = null;
                mCurrentEngine = null;
            }
        }
    }
}
