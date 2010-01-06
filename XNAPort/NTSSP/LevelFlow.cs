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
        internal Level mLevel = null;
        internal Formation mCurrentFormation = null;
        internal Engine mCurrentEngine = null;
        private int mIndex = 0;
        private int mNumPlayer;
        private List<Monster> mBackup = new List<Monster>();

        public LevelFlow(Level aLevel, int aNumPlayer)
        {
            mNumPlayer = aNumPlayer;
            mLevel = aLevel;
        }

        private void BackupMonsters()
        {
            mBackup.Clear();

            if (mLevel.Formations.Count > 0)
            {
                mCurrentFormation = mLevel.Formations[mIndex];

                foreach (Monster m in mLevel.Formations[mIndex].Monsters)
                {
                    Monster m2 = new Monster();
                    m2.Size = m.Size;
                    m2.Color = m.Color;
                    mBackup.Add(m2);
                }
            }
        }

        private void RestoreMonsters()
        {
            if (mLevel.Formations.Count > 0)
            {
                mCurrentFormation = mLevel.Formations[mIndex];

                int i = 0;
                foreach (Monster m in mLevel.Formations[mIndex].Monsters)
                {
                    m.Size = mBackup[i].Size;
                    m.Color = mBackup[i].Color;
                    m.Explosion = null;
                    m.Sprite = null;
                    m.IsExplosing = false;
                    i++;
                }
            }
        }

        public void Update(GameTime aGameTime)
        {
            if(mCurrentFormation == null)
            {
                if (mLevel.Formations.Count > 0)
                {
                    BackupMonsters();
                    mCurrentFormation = mLevel.Formations[mIndex];

                    foreach(Monster m in mLevel.Formations[mIndex].Monsters)
                    {
                        m.Sprite = Monster.GetDefaultMonster(m.Size, m.Color);
                        m.Sprite.Visible = false;
                        DisplayEngine.DisplayManager.Instance.DrawableLevelManager.AddSprite(m.Sprite, 100, mNumPlayer);
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
                    DisplayEngine.DisplayManager.Instance.DrawableLevelManager.RemoveSprite(100, m.Sprite, mNumPlayer);
                    DisplayEngine.DisplayManager.Instance.DrawableLevelManager.RemoveSprite(100, m.Explosion, mNumPlayer);
                }

                RestoreMonsters();

                mCurrentFormation = null;
                mCurrentEngine = null;

                mIndex = (mIndex + 1) % mLevel.Formations.Count;
            }
        }
    }
}
