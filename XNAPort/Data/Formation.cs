using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Math;

namespace Data
{
    public class Formation
    {
        List<Group> mGroups = new List<Group>();

        public List<Group> Groups
        {
            get
            {
                return mGroups;
            }
        }

        List<Monster> mMonsters = new List<Monster>();

        public List<Monster> Monsters
        {
            get
            {
                return mMonsters;
            }
        }
        List<BezierSpline> mSplines = new List<BezierSpline>();

        public List<BezierSpline> Splines
        {
            get { return mSplines; }
        }

        List<Association> mAssociations = new List<Association>();

        public List<Association> Associations
        {
            get { return mAssociations; }
        }

        Dictionary<BezierSpline, List<Association>> mSplinesAssociations = new Dictionary<BezierSpline, List<Association>>();

        public Dictionary<BezierSpline, List<Association>> SplinesAssociations
        {
            get
            {
                return mSplinesAssociations;
            }
        }
    }
}
