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

        private int mGuid = 0;

        public int GUID
        {
            get { return mGuid; }
        }

        List<Association> mAssociations = new List<Association>();

        public List<Association> Associations
        {
            get { return mAssociations; }
        }

        Dictionary<Monster, Vector2> mMonsters = new Dictionary<Monster,Vector2>();

        public Dictionary<Monster, Vector2> Monsters
        {
            get { return mMonsters; }
        }

        List<Monster> mMonstersOrder = new List<Monster>();

        public List<Monster> MonstersOrder
        {
            get
            {
                return mMonstersOrder;
            }
        }

        EffectType mEffectType;

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

        float mSpeed;

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

        float mDiffTime;

        public float DiffTime
        {
            get { return mDiffTime; }
            set
            {
            	mDiffTime = value;
            }
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
