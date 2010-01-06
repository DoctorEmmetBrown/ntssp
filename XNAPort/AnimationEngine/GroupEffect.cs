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

        public static GroupEffect GetEffect(Group aGroup)
        {
            switch(aGroup.EffectType)
            {
                case EffectType.Zero:
                    return new NormalEffect(aGroup);
                case EffectType.Switch:
                    return new SwitchEffect(aGroup);
                case EffectType.Arc:
                    return new ArcEffect(aGroup);
                case EffectType.Circle:
                    return new CircleEffect(aGroup);
                case EffectType.Fixed:
                    return new FixedEffect(aGroup);
                case EffectType.Rotate:
                    return new CircleEffect(aGroup);
            }

            return new NormalEffect(aGroup);
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
        TimeSpan mFPSLastTime = TimeSpan.Zero;
        TimeSpan mTargetTime = TimeSpan.FromSeconds(1 / 15.0);

        TimeSpan mLastTime = new TimeSpan();
        int mEnemyIndex;
        int mNbPoints;
        List<Monster> mEnemiesOrder = new List<Monster>();
        Dictionary<Monster, CurrentPath> mEnemies = new Dictionary<Monster,CurrentPath>();
        List<BezierSpline> mPathList = new List<BezierSpline>();
        public NormalEffect(Group aGroup) : base(aGroup)
        {
            mTargetTime = TimeSpan.FromSeconds(1 / ((30.0) * aGroup.Speed));

            foreach (Association a in aGroup.Associations)
            {
                mPathList.Add(a.Path);
                a.Path.Update();
            }

            foreach (Monster m in aGroup.MonstersOrder)
            {
                mEnemies.Add(m, new CurrentPath());
                mEnemies[m].Index = 0;
                mEnemies[m].Percent = 0;
                mEnemies[m].Spline = mPathList[0];

                mEnemiesOrder.Add(m);
            }
            mEnemyIndex = 0;
            mNbPoints = (int)(1000.0f / aGroup.Speed);
        }

        public override void Animate(GameTime aGameTime)
        {
            if (mFPSLastTime == TimeSpan.Zero)
            {
                mFPSLastTime = aGameTime.TotalGameTime;

            }
            if (aGameTime.TotalGameTime > (mFPSLastTime + mTargetTime))
            {

                mFPSLastTime = aGameTime.TotalGameTime;
            }
            else
            {
                return;
            }

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

                    if(m.IsExplosing)
                    {
                        toDelete.Add(m);
                        continue;
                    }

                    m.Sprite.Visible = true;

                    if (percent == mNbPoints)
                    {
                        mEnemies[m].Index += 1;

                        if (mEnemies[m].Index >= mPathList.Count)
                        {
                            toDelete.Add(m);

                            if (EndEnemy != null)
                            {
                                EndEnemy(this);
                                m.Sprite.Visible = false;
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

                    m.X = coord.X;
                    m.Y = coord.Y;

                    mEnemies[m].Percent += 1;
                }

                foreach (Monster m in toDelete)
                {
                    mEnemies.Remove(m);
                    mEnemiesOrder.Remove(m);
                }

                mEnemyIndex = Math.Min(mEnemyIndex, mEnemies.Count - 1);

                TimeSpan span = mLastTime;
                span = span.Add(TimeSpan.FromSeconds(mGroup.DiffTime));

                if (mEnemyIndex < mEnemies.Count - 1 && span <= aGameTime.TotalGameTime)
                {
                    mEnemyIndex += 1;
                    mLastTime = aGameTime.TotalGameTime;
                }
            }
        }

    }

    public class SwitchStruct
    {
        public Line Line;
        public float t;
    }

    public class SwitchEffect : GroupEffect
    {
        TimeSpan mFPSLastTime = TimeSpan.Zero;
        TimeSpan mTargetTime = TimeSpan.FromSeconds(1 / 15.0);

        int mNbPoints;
        Dictionary<Monster, SwitchStruct> mEnemies = new Dictionary<Monster, SwitchStruct>();
        List<BezierSpline> mPathList = new List<BezierSpline>();

        CurrentPath mPath = new CurrentPath();

        Vector2 mCenter = new Vector2();
        public SwitchEffect(Group aGroup)
            : base(aGroup)
        {
            mTargetTime = TimeSpan.FromSeconds(1 / ((30.0) * aGroup.Speed));

            foreach (Association a in aGroup.Associations)
            {
                mPathList.Add(a.Path);
                a.Path.Update();
            }

            mPath.Index = 0;
            mPath.Percent = 0;
            mPath.Spline = mPathList[0];

            foreach (Monster m in aGroup.MonstersOrder)
            {
                mEnemies.Add(m, new SwitchStruct());
                mEnemies[m].Line = new Line(new Vector2(aGroup.Monsters[m].X, aGroup.Monsters[m].Y), new Vector2(-aGroup.Monsters[m].X, aGroup.Monsters[m].Y));
                mEnemies[m].t = 0.0f;
            }
            mNbPoints = (int)(1000.0f / aGroup.Speed);
        }

        public override void Animate(GameTime aGameTime)
        {
            if (mFPSLastTime == TimeSpan.Zero)
            {
                mFPSLastTime = aGameTime.TotalGameTime;

            }
            if (aGameTime.TotalGameTime > (mFPSLastTime + mTargetTime))
            {

                mFPSLastTime = aGameTime.TotalGameTime;
            }
            else
            {
                return;
            }

            if (mPathList.Count > 0)
            {
                BezierSpline path = mPath.Spline;
                    int percent = mPath.Percent;

                    if (percent == mNbPoints)
                    {
                        mPath.Index += 1;

                        if (mPath.Index >= mPathList.Count)
                        {

                            if (End != null)
                            {
                                End(this);
                                foreach (Monster m in mEnemies.Keys)
                                {
                                    m.Sprite.Visible = false;
                                }
                            }
                            return;

                        }
                        else
                        {
                            mPath.Spline = mPathList[mPath.Index];
                            mPath.Percent = 0;
                        }
                    }

                    Vector2 coord = mPath.Spline.getPoint(((float)mPath.Percent) / ((float)mNbPoints));

                    mCenter.X = coord.X;
                    mCenter.Y = coord.Y;

                   mPath.Percent += 1;
       
                foreach(Monster m in mEnemies.Keys)
                {
                    if (m.IsExplosing)
                    {
                        continue;
                    }

                    m.Sprite.Visible = true;

                    SwitchStruct str = mEnemies[m];

                    Vector2 pos = str.Line.At(str.t);

                    float x = mCenter.X + pos.X;
                    float y = mCenter.Y + pos.Y;

                    str.t+= 0.1f;

                    if(str.t >= 1.0f)
                    {
                        str.t=0.0f;
                        str.Line = new Line(str.Line.P2,str.Line.P1);
                    }
 

                    m.X = x;
                    m.Y = y;
                }
            }
        }
    }

    public class ArcEffect : GroupEffect
    {
        TimeSpan mFPSLastTime = TimeSpan.Zero;
        TimeSpan mTargetTime = TimeSpan.FromSeconds(1 / 15.0);

        int mNbPoints;
        Dictionary<Monster, SwitchStruct> mEnemies = new Dictionary<Monster, SwitchStruct>();
        List<BezierSpline> mPathList = new List<BezierSpline>();

        CurrentPath mPath = new CurrentPath();

        Vector2 mCenter = new Vector2();
        public ArcEffect(Group aGroup)
            : base(aGroup)
        {
            mTargetTime = TimeSpan.FromSeconds(1 / ((30.0) * aGroup.Speed));

            foreach (Association a in aGroup.Associations)
            {
                mPathList.Add(a.Path);
                a.Path.Update();
            }

            mPath.Index = 0;
            mPath.Percent = 0;
            mPath.Spline = mPathList[0];

            foreach (Monster m in aGroup.MonstersOrder)
            {
                mEnemies.Add(m, new SwitchStruct());
                mEnemies[m].Line = new Line(new Vector2(aGroup.Monsters[m].X, aGroup.Monsters[m].Y), new Vector2(-aGroup.Monsters[m].X, -aGroup.Monsters[m].Y));
                mEnemies[m].t = 0.0f;
            }
            mNbPoints = (int)(1000.0f / aGroup.Speed);
        }

        public override void Animate(GameTime aGameTime)
        {
            if (mFPSLastTime == TimeSpan.Zero)
            {
                mFPSLastTime = aGameTime.TotalGameTime;

            }
            if (aGameTime.TotalGameTime > (mFPSLastTime + mTargetTime))
            {

                mFPSLastTime = aGameTime.TotalGameTime;
            }
            else
            {
                return;
            }

            if (mPathList.Count > 0)
            {
                BezierSpline path = mPath.Spline;
                int percent = mPath.Percent;

                if (percent == mNbPoints)
                {
                    mPath.Index += 1;

                    if (mPath.Index >= mPathList.Count)
                    {

                        if (End != null)
                        {
                            End(this);
                            foreach (Monster m in mEnemies.Keys)
                            {
                                m.Sprite.Visible = false;
                            }
                        }
                        return;

                    }
                    else
                    {
                        mPath.Spline = mPathList[mPath.Index];
                        mPath.Percent = 0;
                    }
                }

                Vector2 coord = mPath.Spline.getPoint(((float)mPath.Percent) / ((float)mNbPoints));

                mCenter.X = coord.X;
                mCenter.Y = coord.Y;

                mPath.Percent += 1;

                foreach (Monster m in mEnemies.Keys)
                {
                    if (m.IsExplosing)
                    {
                        continue;
                    }

                    m.Sprite.Visible = true;

                    SwitchStruct str = mEnemies[m];

                    Vector2 pos = str.Line.At(str.t);

                    float x = mCenter.X + pos.X;
                    float y = mCenter.Y + pos.Y;

                    str.t += 0.1f;

                    if (str.t >= 1.0f)
                    {
                        str.t = 0.0f;
                        str.Line = new Line(str.Line.P2, str.Line.P1);
                    }


                    m.X = x;
                    m.Y = y;
                }
            }
        }
    }

    public class FixedEffect : GroupEffect
    {
        TimeSpan mFPSLastTime = TimeSpan.Zero;
        TimeSpan mTargetTime = TimeSpan.FromSeconds(1 / 15.0);

        int mNbPoints;
        List<Monster> mEnemies = new List<Monster>();
        List<BezierSpline> mPathList = new List<BezierSpline>();

        CurrentPath mPath = new CurrentPath();

        Vector2 mCenter = new Vector2();
        public FixedEffect(Group aGroup)
            : base(aGroup)
        {
            mTargetTime = TimeSpan.FromSeconds(1 / ((30.0) * aGroup.Speed));

            foreach (Association a in aGroup.Associations)
            {
                mPathList.Add(a.Path);
                a.Path.Update();
            }

            mPath.Index = 0;
            mPath.Percent = 0;
            mPath.Spline = mPathList[0];

            foreach (Monster m in aGroup.MonstersOrder)
            {
                mEnemies.Add(m);
            }
            mNbPoints = (int)(1000.0f / aGroup.Speed);
        }

        public override void Animate(GameTime aGameTime)
        {
            if (mFPSLastTime == TimeSpan.Zero)
            {
                mFPSLastTime = aGameTime.TotalGameTime;

            }
            if (aGameTime.TotalGameTime > (mFPSLastTime + mTargetTime))
            {

                mFPSLastTime = aGameTime.TotalGameTime;
            }
            else
            {
                return;
            }

            if (mPathList.Count > 0)
            {
                BezierSpline path = mPath.Spline;
                int percent = mPath.Percent;

                if (percent == mNbPoints)
                {
                    mPath.Index += 1;

                    if (mPath.Index >= mPathList.Count)
                    {

                        if (End != null)
                        {
                            End(this);
                            foreach (Monster m in mEnemies)
                            {
                                m.Sprite.Visible = false;
                            }
                        }
                        return;

                    }
                    else
                    {
                        mPath.Spline = mPathList[mPath.Index];
                        mPath.Percent = 0;
                    }
                }

                Vector2 coord = mPath.Spline.getPoint(((float)mPath.Percent) / ((float)mNbPoints));

                mCenter.X = coord.X;
                mCenter.Y = coord.Y;

                mPath.Percent += 1;

                foreach (Monster m in mEnemies)
                {
                    if (m.IsExplosing)
                    {
                        continue;
                    }

                    m.Sprite.Visible = true;

                    Vector2 pos = mGroup.Monsters[m];

                    float x = mCenter.X + pos.X;
                    float y = mCenter.Y + pos.Y;

                    m.X = x;
                    m.Y = y;
                }
            }
        }
    }

    public class CircleStruct
    {
        public float Degree;
        public float Ray;
    }
    public class CircleEffect : GroupEffect
    {
        TimeSpan mFPSLastTime = TimeSpan.Zero;
        TimeSpan mTargetTime = TimeSpan.FromSeconds(1 / 15.0);

        int mNbPoints;
        Dictionary<Monster,CircleStruct> mEnemies = new Dictionary<Monster,CircleStruct>();
        List<BezierSpline> mPathList = new List<BezierSpline>();

        CurrentPath mPath = new CurrentPath();

        Vector2 mCenter = new Vector2();
        public CircleEffect(Group aGroup)
            : base(aGroup)
        {
            mTargetTime = TimeSpan.FromSeconds(1 / ((30.0) * aGroup.Speed));

            foreach (Association a in aGroup.Associations)
            {
                mPathList.Add(a.Path);
                a.Path.Update();
            }

            mPath.Index = 0;
            mPath.Percent = 0;
            mPath.Spline = mPathList[0];

            foreach (Monster m in aGroup.MonstersOrder)
            {
                Vector2 v = aGroup.Monsters[m];
                mEnemies.Add(m,new CircleStruct());
                mEnemies[m].Ray = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y);
                if(v.X == 0)
                {
                    mEnemies[m].Degree = 0.0f;
                }
                else
                {
                    mEnemies[m].Degree = (float)Math.Acos(v.X / mEnemies[m].Ray);
                }

                if(v.Y < 0)
                {
                    mEnemies[m].Degree = - mEnemies[m].Degree;
                }
            }
            mNbPoints = (int)(1000.0f / aGroup.Speed);
        }

        public override void Animate(GameTime aGameTime)
        {
            if (mFPSLastTime == TimeSpan.Zero)
            {
                mFPSLastTime = aGameTime.TotalGameTime;

            }
            if (aGameTime.TotalGameTime > (mFPSLastTime + mTargetTime))
            {

                mFPSLastTime = aGameTime.TotalGameTime;
            }
            else
            {
                return;
            }

            if (mPathList.Count > 0)
            {
                BezierSpline path = mPath.Spline;
                int percent = mPath.Percent;

                if (percent == mNbPoints)
                {
                    mPath.Index += 1;

                    if (mPath.Index >= mPathList.Count)
                    {

                        if (End != null)
                        {
                            End(this);
                            foreach (Monster m in mEnemies.Keys)
                            {
                                m.Sprite.Visible = false;
                            }
                        }
                        return;

                    }
                    else
                    {
                        mPath.Spline = mPathList[mPath.Index];
                        mPath.Percent = 0;
                    }
                }

                Vector2 coord = mPath.Spline.getPoint(((float)mPath.Percent) / ((float)mNbPoints));

                mCenter.X = coord.X;
                mCenter.Y = coord.Y;

                mPath.Percent += 1;

                foreach (Monster m in mEnemies.Keys)
                {
                    if (m.IsExplosing)
                    {
                        continue;
                    }

                    m.Sprite.Visible = true;

                    CircleStruct str = mEnemies[m];

                    float x = mCenter.X + str.Ray * (float)Math.Cos(str.Degree);
                    float y = mCenter.Y + str.Ray * (float)Math.Sin(str.Degree);

                    m.X = x;
                    m.Y = y;

                    if(str.Degree > Math.PI)
                    {
                        str.Degree = - (float)Math.PI;
                    }

                    str.Degree += 0.01f;
                }
            }
        }
    }
}
