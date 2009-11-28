using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
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

        [ContentSerializerIgnore]
        private int mGuid = 0;

        [ContentSerializerIgnore]
        public int GUID
        {
            get { return mGuid; }
        }

        [ContentSerializer]
        AssociationType mType;

        [ContentSerializerIgnore]
        public AssociationType Type
        {
            get
            {
                return mType;
            }
        }

        [ContentSerializer(SharedResource = true)]
        Group mGroup;

        [ContentSerializerIgnore]
        public Group Group
        {
            get { return mGroup; }
        }

        [ContentSerializer(SharedResource = true)]
        BezierSpline mPath;

        [ContentSerializerIgnore]
        public BezierSpline Path
        {
            get { return mPath; }
        }

        [ContentSerializer]
        float mTimeBefore;

        [ContentSerializerIgnore]
        public float TimeBefore
        {
            get
            {
                return mTimeBefore;
            }
        }

        public Association()
        {
            mGuid = mNbInstance;
            mNbInstance++;
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
