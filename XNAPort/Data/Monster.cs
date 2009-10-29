using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Monster
    {
        private static int mNbInstance = 0;

        List<Group> mGroups = new List<Group>();

        public List<Group> Groups
        {
            get { return mGroups; }
        }


        private int mGuid = 0;

        public int GUID
        {
            get { return mGuid; }
        }

        private int mColor = 0;

        public int Color
        {
            get{ return mColor; }
            set{ mColor = value;}
        }

        private int mSize = 0;

        public int Size
        {
            get { return mSize; }
            set { mSize = value; }
        }

        public Monster(int aSize, int aColor)
        {
            mGuid = mNbInstance;
            mNbInstance++;

            mSize = aSize;
            mColor = aColor;
        }

        public string ToString()
        {
            string values = "";

            values += "Monster " + GUID + " ";
            values += "Color : " + Color + " Size : " + Size;

            return values;
        }
    }
}
