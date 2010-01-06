using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Utils;

namespace Data
{
    public enum EffectType
    {
        Zero,
        Circle,
        Switch,
        Arc,
        Rotate,
        Fixed
    }

    public class Group
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
        SharedResourceList<Association> mAssociations = new SharedResourceList<Association>();

        [ContentSerializerIgnore]
        public List<Association> Associations
        {
            get { return mAssociations; }
        }

        [ContentSerializer(SharedResource=true)]
        SharedResourceDictionary<Monster, Vector2> mMonsters = new SharedResourceDictionary<Monster, Vector2>();

        [ContentSerializerIgnore]
        public Dictionary<Monster, Vector2> Monsters
        {
            get { return mMonsters; }
        }

        [ContentSerializer(SharedResource = true)]
        SharedResourceList<Monster> mMonstersOrder = new SharedResourceList<Monster>();

        [ContentSerializerIgnore]
        public List<Monster> MonstersOrder
        {
            get
            {
                return mMonstersOrder;
            }
        }

        [ContentSerializer]
        EffectType mEffectType;

        [ContentSerializerIgnore]
        public EffectType EffectType
        {
            get
            {
                return mEffectType;
            }
            set
            {
                mEffectType = value;
            }
        }

        [ContentSerializer]
        float mSpeed;

        [ContentSerializerIgnore]
        public float Speed
        {
            get
            {
                return mSpeed;
            }
            set
            {
                mSpeed = value;
            }
        }

        [ContentSerializer]
        float mDiffTime;

        [ContentSerializerIgnore]
        public float DiffTime
        {
            get { return mDiffTime; }
            set
            {
            	mDiffTime = value;
            }
        }

        public Group()
        {
            mGuid = mNbInstance;
            mNbInstance++;
        }

        public Group(EffectType aType, float aSpeed, float aDiffTime)
        {
            mGuid = mNbInstance;
            mNbInstance++;

            mEffectType = aType;
            mSpeed = aSpeed;
            mDiffTime = aDiffTime;
        }

        public string ToString()
        {
            string type = "Normal";
            switch (this.EffectType)
            {
                case EffectType.Zero:
                    type = "Normal";
                    break;
                case EffectType.Circle:
                    type = "Circle";
                    break;
                case EffectType.Switch:
                    type = "Switch";
                    break;
                case EffectType.Arc:
                    type = "Arc";
                    break;
                case EffectType.Fixed:
                    type = "Fixed";
                    break;
            }

            string values = "Group " + GUID + " ";
            values += "Speed : " + Speed + " DiffTime : " + DiffTime + " Type " + type;
            return values;
        }
    }
}
