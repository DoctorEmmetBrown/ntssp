using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayEngine.Display2D;
using ScriptEngine;
using Utils.Math;

namespace Data
{
    class Boss
    {
        Dictionary<String, Sprite> mWanderSprites = new Dictionary<string, Sprite>();

        public Dictionary<String,Sprite> WanderSprites
        {
            get { return mWanderSprites; }
            set { mWanderSprites = value;}
        }

        Dictionary<String,BezierSpline> mWanderSplines = new Dictionary<string, BezierSpline>();

        public Dictionary<String,BezierSpline> WanderSplines
        {
            get { return mWanderSplines; }
            set { mWanderSplines = value;}
        }

        private Script mWanderScript = null;

        public Script WanderScript
        {
            get { return mWanderScript; }
            set { mWanderScript = value;}
        }

        Dictionary<String, Sprite> mEscapeSprites = new Dictionary<string, Sprite>();

        public Dictionary<String, Sprite> EscapeSprites
        {
            get { return mEscapeSprites; }
            set { mEscapeSprites = value; }
        }

        Dictionary<String, BezierSpline> mEscapeSplines = new Dictionary<string, BezierSpline>();

        public Dictionary<String, BezierSpline> EscapeSplines
        {
            get { return mEscapeSplines; }
            set { mEscapeSplines = value; }
        }

        private Script mEscapeScript = null;

        public Script EscapeScript
        {
            get { return mEscapeScript; }
            set { mEscapeScript = value; }
        }

        Dictionary<String, Sprite> mAttacksSprites = new Dictionary<string, Sprite>();

        public Dictionary<String, Sprite> AttacksSprites
        {
            get { return mAttacksSprites; }
            set { mAttacksSprites = value; }
        }

        Dictionary<String, BezierSpline> mAttacksSplines = new Dictionary<string, BezierSpline>();

        public Dictionary<String, BezierSpline> AttacksSplines
        {
            get { return mAttacksSplines; }
            set { mAttacksSplines = value; }
        }

        private List<Script> mAttacksScripts = new List<Script>();

        public List<Script> AttacksScripts
        {
            get { return mAttacksScripts; }
            set { mAttacksScripts = value; }
        }

    }
}
