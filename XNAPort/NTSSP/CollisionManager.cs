using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using DisplayEngine;
using DisplayEngine.Display2D;
using Microsoft.Xna.Framework;

namespace NTSSP
{
    internal enum ChainType
    {
        HORIZONTAL,
        PARABOLIC,
        REFLECT
    }
    class Chain
    {
        private static int mNbChain = 0;
        private int mNumPlayer;
        public int mChainGUID;
        public int mMonstersNumber = 0;
        private ChainType mType;
        public List<Monster> mMonsters = new List<Monster>();
        public Dictionary<Flame, FlamePath> mFlames = new Dictionary<Flame, FlamePath>();


        public Chain(int aNumPlayer)
        {
            mNumPlayer = aNumPlayer;
            mChainGUID = mNbChain;
            mType = (ChainType)(mChainGUID%3);
            mNbChain++;
        }

        public void Add(Monster aMonster)
        {
            mMonsters.Add(aMonster);
            mMonstersNumber++;

            Flame f = null;
            if (mMonstersNumber == 4)
            {
                f = new Flame(FlameType.SMALL);
            }

            if (mMonstersNumber == 6 || mMonstersNumber == 8 || mMonstersNumber == 10)
            {
                f = new Flame(FlameType.NORMAL);
            }

            if (mMonstersNumber == 12 || mMonstersNumber == 14)
            {
                f = new Flame(FlameType.BIG);
            }

            if (mMonstersNumber > 15 && mMonstersNumber%2 == 0)
            {
                f = new Flame(FlameType.BIGGEST);
            }

            if (f != null)
            {
                DisplayManager.Instance.DrawableLevelManager.AddSprite(f.Sprite, 100, DisplayManager.Instance.ScreenSplitter.SplitScreens.Count - 1);
                f.Sprite.Visible = true;

                Vector2 MonsterCoord = new Vector2(aMonster.X, aMonster.Y);
                MonsterCoord = DisplayManager.Instance.ScreenSplitter.TranslateCoordToScreen(MonsterCoord, mNumPlayer);
                MonsterCoord = DisplayManager.Instance.ScreenSplitter.TranslateCoordFromScreen(MonsterCoord,DisplayManager.Instance.ScreenSplitter.SplitScreens.Count -1);

                Player p = NTSSPGame.Instance.GetPlayer((mNumPlayer == 0) ? 1 : 0);
                Vector2 PlayerCoord = new Vector2(p.mX, p.mY);
                PlayerCoord = DisplayManager.Instance.ScreenSplitter.TranslateCoordToScreen(PlayerCoord,(mNumPlayer == 0) ? 1 : 0);
                PlayerCoord = DisplayManager.Instance.ScreenSplitter.TranslateCoordFromScreen(PlayerCoord,DisplayManager.Instance.ScreenSplitter.SplitScreens.Count - 1);

                FlamePath fp = null;
                switch (mType)
                {
                    case ChainType.HORIZONTAL:
                        fp = new HorizontalPath(MonsterCoord, PlayerCoord);
                        break;
                    case ChainType.PARABOLIC:
                        fp = new ParabolicPath(MonsterCoord, PlayerCoord);
                        break;
                    case ChainType.REFLECT:
                        fp = new ParabolicPath(MonsterCoord, PlayerCoord);
                        break;
                }
                mFlames.Add(f, fp);
            }
        }

        public bool Remove(Monster aMonster)
        {
            mMonsters.Remove(aMonster);
            if (mMonsters.Count == 0 && mFlames.Count == 0)
            {
                return true;
            }

            return false;
        }

        public void Update(GameTime aTime)
        {
            List<Flame> toDelete = new List<Flame>();
            foreach(Flame f in mFlames.Keys)
            {
                FlamePath fp = mFlames[f];

                Vector2 coord = fp.At(fp.t);
                f.X = coord.X;
                f.Y = coord.Y;

                fp.t += 0.001f;

                if(fp.t > 1.0f)
                {
                    toDelete.Add(f);
                }
            }

            foreach (Flame f in toDelete)
            {
                mFlames.Remove(f);
            }
        }

    }

    class CollisionManager
    {
        private Player mPlayer;
        private LevelFlow mLevelFlow;
        private int mNumPlayer;
        private Dictionary<int, Chain> mExplosingMonsters = new Dictionary<int, Chain>();
        private Dictionary<Monster, int> mToAddExplosingMonsters = new Dictionary<Monster, int>();

        public CollisionManager(Player aPlayer, LevelFlow aLevelFlow, int aNumPlayer)
        {
            mPlayer = aPlayer;
            mLevelFlow = aLevelFlow;
            mNumPlayer = aNumPlayer;
        }

        private void PlayerMonstersCollisions()
        {
            //Player/Monster Collisions
            if (mLevelFlow.mCurrentFormation != null)
            {
                foreach (Monster m in mLevelFlow.mCurrentFormation.Monsters)
                {
                    if (m.Sprite.Intersects(mPlayer.Character.Idle, mNumPlayer))
                    {

                    }
                }
            }
        }

        private void MonstersShotsCollisions()
        {
            //Monsters/Shots Collisions
            if (mLevelFlow.mCurrentFormation != null)
            {
                List<Sprite> toDelete = new List<Sprite>();
                foreach (Sprite s in mPlayer.mCurrentShots)
                {
                    foreach (Monster m in mLevelFlow.mCurrentFormation.Monsters)
                    {
                        if (s.Intersects(m.Sprite, mNumPlayer) && !m.IsExplosing)
                        {
                            m.Color--;

                            if (m.Color == 0)
                            {
                                mToAddExplosingMonsters.Add(m,-1);
                                DisplayManager.Instance.DrawableLevelManager.AddSprite(m.Explosion,100,mNumPlayer);
                            }

                            toDelete.Add(s);
                        }
                    }
                }
                foreach (Sprite s in toDelete)
                {
                    mPlayer.RemoveShot(s);
                }
            }
        }

        private void MonstersSuperAttackCollisions()
        {
            //Monsters/SuperAttackCollision
            if (mLevelFlow.mCurrentFormation != null)
            {
                if (mPlayer.mSuperAttacking)
                {
                    foreach (Monster m in mLevelFlow.mCurrentFormation.Monsters)
                    {

                        foreach (Sprite s in mPlayer.mChargedAttackCollisions)
                        {
                            if (s.Intersects(m.Sprite, mNumPlayer))
                            {
                                if (!m.IsExplosing)
                                {
                                    m.Color = 0;
                                    mToAddExplosingMonsters.Add(m, -1);
                                    DisplayManager.Instance.DrawableLevelManager.AddSprite(m.Explosion, 100, mNumPlayer);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MonstersExplosionsCollisions()
        {
            if (mLevelFlow.mCurrentFormation != null)
            {
                List<Chain> toDeleteChains = new List<Chain>();
                foreach (Chain m1 in mExplosingMonsters.Values)
                {
                    List<Monster> toDelete = new List<Monster>();
                    foreach(Monster m2 in m1.mMonsters)
                    {
                        foreach (Monster m in mLevelFlow.mCurrentFormation.Monsters)
                        {
                            if (!m.IsExplosing && m2.Explosion != null &&
                                m2.Explosion.Intersects(m.Sprite, mNumPlayer))
                            {
                                m.Color = Math.Max(0, m.Color - m2.ExplosingColor);

                                if (m.Color == 0)
                                {
                                    mToAddExplosingMonsters.Add(m, m1.mChainGUID);
                                    DisplayManager.Instance.DrawableLevelManager.AddSprite(m.Explosion, 100, mNumPlayer);
                                }
                            }

                        }
                        if (m2.Explosion == null || !m2.Explosion.Visible)
                        {
                            toDelete.Add(m2);
                        }
                    }
                    foreach (Monster m in toDelete)
                    {
                        if(m1.Remove(m))
                        {
                            toDeleteChains.Add(m1);
                        }
                    }


                }
                foreach (Chain c in toDeleteChains)
                {
                    mExplosingMonsters.Remove(c.mChainGUID);
                }


            }
        }

        public void Update(GameTime aGameTime)
        {
            mToAddExplosingMonsters.Clear();

            PlayerMonstersCollisions();

            MonstersShotsCollisions();

            MonstersSuperAttackCollisions();

            MonstersExplosionsCollisions();

            foreach (Monster m in mToAddExplosingMonsters.Keys)
            {
                if(mToAddExplosingMonsters[m] == -1)
                {
                    Chain c = new Chain(mNumPlayer);
                    c.Add(m);
                    mExplosingMonsters.Add(c.mChainGUID,c);
                }
                else
                {
                    mExplosingMonsters[mToAddExplosingMonsters[m]].Add(m);
                }
            }

            foreach(Chain c in mExplosingMonsters.Values)
            {
                c.Update(aGameTime);
            }
        }
    }
}
