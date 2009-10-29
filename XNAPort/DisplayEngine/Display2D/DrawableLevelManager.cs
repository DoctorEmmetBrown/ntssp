using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DisplayEngine.Display2D
{
    public class DrawableLevelManager : DrawableGameComponent
    {
        private readonly SortedList<uint, List<IDrawable2D>> mLevels;

        public List<uint> Levels
        {
            get { return new List<uint>(mLevels.Keys); }
        }

        public DrawableLevelManager(Game aGame): base(aGame)
        {
            mLevels = new SortedList<uint, List<IDrawable2D>>();
        }

        public void AddSprite(IDrawable2D aSprite, uint aLevel)
        {
            if (!mLevels.ContainsKey(aLevel))
            {
                mLevels.Add(aLevel, new List<IDrawable2D>());
            }

            mLevels[aLevel].Add(aSprite);
        }

        public List<IDrawable2D> GetSprites(uint aLevel)
        {
            if (!mLevels.ContainsKey(aLevel))
            {
                return new List<IDrawable2D>();
            }
            return mLevels[aLevel];
        }

        public override void Draw(GameTime aGameTime)
        {

           for(int i=mLevels.Keys.Count - 1; i >= 0; i--)
           {
               foreach (IDrawable2D sprite in mLevels[mLevels.Keys[i]])
               {
                   sprite.Draw();
               }
           }
        }

        public override void Update(GameTime aGameTime)
        {
            for (int i = mLevels.Keys.Count - 1; i >= 0; i--)
            {
                foreach (IDrawable2D sprite in mLevels[mLevels.Keys[i]])
                {
                    sprite.Update(aGameTime);
                }
            }
        }
    }
}