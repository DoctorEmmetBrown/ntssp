using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DisplayEngine.Display2D
{
    public class DrawableLevelManager : DrawableGameComponent
    {
        private readonly Dictionary<int,SortedList<uint, List<IDrawable2D>>> mLevels;


        public DrawableLevelManager(Game aGame): base(aGame)
        {
            mLevels = new Dictionary<int, SortedList<uint, List<IDrawable2D>>>();
        }

        public void AddSprite(IDrawable2D aSprite, uint aLevel, int aNumScreen)
        {
            if (!mLevels.ContainsKey(aNumScreen))
            {
                mLevels.Add(aNumScreen, new SortedList<uint, List<IDrawable2D>>());
            }

            if (!mLevels[aNumScreen].ContainsKey(aLevel))
            {
                mLevels[aNumScreen].Add(aLevel, new List<IDrawable2D>());
            }

            mLevels[aNumScreen][aLevel].Add(aSprite);
            if(aSprite is Sprite)
            {
                ((Sprite) aSprite).mNumScreen = aNumScreen;
            }
            
        }

        public List<IDrawable2D> GetSprites(uint aLevel,int aNumScreen)
        {
            if (!mLevels.ContainsKey(aNumScreen) && !mLevels[aNumScreen].ContainsKey(aLevel))
            {
                return new List<IDrawable2D>();
            }
            return mLevels[aNumScreen][aLevel];
        }

        public void RemoveSprite(uint aLevel, Sprite aSprite, int aNumScreen)
        {
            if (!mLevels.ContainsKey(aNumScreen) && !mLevels[aNumScreen].ContainsKey(aLevel))
            {
                return;
            }
            mLevels[aNumScreen][aLevel].Remove(aSprite);
        }

        public override void Draw(GameTime aGameTime)
        {
            foreach (int num in mLevels.Keys)
            {
                for (int i = mLevels[num].Keys.Count - 1; i >= 0; i--)
                {
                    foreach (IDrawable2D sprite in mLevels[num][mLevels[num].Keys[i]])
                    {
                        sprite.Draw(num);
                    }
                }
            }
        }

        public override void Update(GameTime aGameTime)
        {
            foreach (int num in mLevels.Keys)
            {
                for (int i = mLevels[num].Keys.Count - 1; i >= 0; i--)
                {
                    foreach (IDrawable2D sprite in mLevels[num][mLevels[num].Keys[i]])
                    {
                        sprite.Update(aGameTime);
                    }
                }
            }
        }
    }
}