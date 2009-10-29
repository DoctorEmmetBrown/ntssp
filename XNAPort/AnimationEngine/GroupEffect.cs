using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Utils.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AnimationEngine
{
    public class GroupEffect
    {

        public delegate void EndDelegate(GroupEffect g);

        public EndDelegate End = null;

        public delegate void EndEnemyDelegate(GroupEffect g);

        public EndEnemyDelegate EndEnemy = null;

        protected Group mGroup;

        public Group Group
        {
            get { return mGroup; }
        }

        public GroupEffect(Group aGroup)
        {
            mGroup = aGroup;
        }

        public virtual void Animate(GameTime aGameTime)
        {
        }
    }
    class CurrentPath
    {
        public BezierSpline Spline;
        public int Percent;
        public int Index;
    }
    public class NormalEffect : GroupEffect
    {
        TimeSpan mLastTime = new TimeSpan();
        int mEnemyIndex;
        int mNbPoints;
        List<Monster> mEnemiesOrder = new List<Monster>();
        Dictionary<Monster, CurrentPath> mEnemies = new Dictionary<Monster,CurrentPath>();
        List<BezierSpline> mPathList = new List<BezierSpline>();
        public NormalEffect(Group aGroup) : base(aGroup)
        {
            foreach (Monster m in aGroup.MonstersOrder)
            {
                mEnemies.Add(m, new CurrentPath());
                mEnemies[m].Index = 0;
                mEnemies[m].Percent = 0;

                mEnemiesOrder.Add(m);
            }
            mEnemyIndex = 0;
            mNbPoints = (int)(1000.0f / aGroup.Speed);

            foreach (Association a in aGroup.Associations)
            {
                mPathList.Add(a.Path);
            }
        }

        public virtual void Animate(GameTime aGameTime)
        {
            if (mPathList.Count > 0)
            {
                if (mEnemies.Count == 0)
                {
                    if (End != null)
                    {
                        End(this);
                    }
                }

                if (mLastTime == new TimeSpan())
                {
                    mLastTime = aGameTime.TotalGameTime;
                }

                List<Monster> toDelete = new List<Monster>();
                for (int i = 0; i<=mEnemyIndex; i++)
                {
                    Monster m = mEnemiesOrder[i];
                    BezierSpline path = mEnemies[m].Spline;
                    int percent = mEnemies[m].Percent;

                    if (percent == mNbPoints)
                    {
                        mEnemies[m].Index += 1;

                        if (mEnemies[m].Index >= mPathList.Count)
                        {
                            toDelete.Add(m);

                            if (EndEnemy != null)
                            {
                                EndEnemy(this);
                            }
                            continue;

                        }
                        else
                        {
                            mEnemies[m].Spline = mPathList[mEnemies[m].Index];
                            mEnemies[m].Percent = 0;
                        }
                    }

                    Vector2 coord = mEnemies[m].Spline.getPoint(((float)mEnemies[m].Percent) / ((float)mNbPoints));

                    mEnemies[m].Percent += 1;
                }

                TimeSpan span = mLastTime;
                span.Add(TimeSpan.FromSeconds(mGroup.DiffTime));

                if (mEnemyIndex < mGroup.MonstersOrder.Count && span <= aGameTime.TotalGameTime)
                {
                    mEnemyIndex += 1;
                    mLastTime = aGameTime.TotalGameTime;
                }
            }
        }

    }
}
