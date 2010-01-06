using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace ScriptEngine
{
    public abstract class Script
    {
        private String mName;
        public String Name
        {
            get { return mName; }
        }

        public Script(String aName)
        {
            mName = aName;
        }

        public abstract bool Execute(Game aGame, GameTime aTime, Dictionary<String,Object> aObjects, ref Dictionary<String,Object> aResults);
    }
}
