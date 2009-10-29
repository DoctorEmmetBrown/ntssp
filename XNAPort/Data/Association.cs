using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Math;
namespace Data
{
    public enum AssociationType
    {
        Normal,
        WhenPlayerIsFound
    }
    public class Association
    {
        private static int mNbInstance = 0;

        private int mGuid = 0;

        public int GUID
        {
            get { return mGuid; }
        }

        AssociationType mType;

        public AssociationType Type
        {
            get
            {
                return mType;
            }
        }

        Group mGroup;

        public Group Group
        {
            get { return mGroup; }
        }

        BezierSpline mPath;

        public BezierSpline Path
        {
            get { return mPath; }
        }

        float mTimeBefore;
        public float TimeBefore
        {
            get
            {
                return mTimeBefore;
            }
        }

        public Association(AssociationType aType, Group aGroup, BezierSpline aPath, float aTimeBefore)
        {
            mGuid = mNbInstance;
            mNbInstance++;
            mType = aType;
            mGroup = aGroup;
            mPath = aPath;
            mTimeBefore = aTimeBefore;

            mGroup.Associations.Add(this);
        }

        public string ToString()
        {
            string type = "";
            switch (mType)
            {
                case AssociationType.Normal :
                    type = "Normal";
                    break;
                case AssociationType.WhenPlayerIsFound :
                    type = "WhenPlayerIsFound";
                    break;
                default:
                    type = "Normal";
                    break;
            }
            string s = "Association " + GUID + " : Group " + mGroup.GUID + " Path " + mPath.GUID + " " + type + " " + mTimeBefore + "s";
            return s;
        }
    }
}
