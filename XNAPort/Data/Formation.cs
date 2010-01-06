using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Utils.Math;
using Utils;

namespace Data
{
    public class Formation
    {
        [ContentSerializer]
        SharedResourceList<Group> mGroups = new SharedResourceList<Group>();

        [ContentSerializerIgnore]
        public List<Group> Groups
        {
            get
            {
                return mGroups;
            }
        }

        [ContentSerializer(SharedResource = true)]
        SharedResourceList<Monster> mMonsters = new SharedResourceList<Monster>();

        [ContentSerializerIgnore]
        public List<Monster> Monsters
        {
            get
            {
                return mMonsters;
            }
        }

        [ContentSerializer]
        SharedResourceList<BezierSpline> mSplines = new SharedResourceList<BezierSpline>();

        [ContentSerializerIgnore]
        public List<BezierSpline> Splines
        {
            get { return mSplines; }
        }

        [ContentSerializer]
        SharedResourceList<Association> mAssociations = new SharedResourceList<Association>();

        [ContentSerializerIgnore]
        public List<Association> Associations
        {
            get { return mAssociations; }
        }

        [ContentSerializer]
        SharedResourceDictionary<BezierSpline, SharedResourceList<Association>> mSplinesAssociations = new SharedResourceDictionary<BezierSpline, SharedResourceList<Association>>();

        [ContentSerializerIgnore]
        public Dictionary<BezierSpline, SharedResourceList<Association>> SplinesAssociations
        {
            get
            {
                return mSplinesAssociations;
            }
        }
    }
}
