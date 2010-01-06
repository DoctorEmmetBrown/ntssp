using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayEngine.Display2D;
using Microsoft.Xna.Framework.Content;
using Utils;
using ScriptEngine;

namespace Data.Utils
{
    class CharacterReader : ContentTypeReader<Character>
    {
        protected override Character Read(ContentReader aInput,
                                    Character aExistingInstance)
        {
            aExistingInstance = new Character();

            string s = "";

            s = aInput.ReadObject<string>();
            aExistingInstance.Idle = aInput.ContentManager.Load<Sprite>(s);

            s = aInput.ReadObject<string>();
            aExistingInstance.Left = aInput.ContentManager.Load<Sprite>(s);

            s = aInput.ReadObject<string>();
            aExistingInstance.Right = aInput.ContentManager.Load<Sprite>(s);

            s = aInput.ReadObject<string>();
            aExistingInstance.Shot = aInput.ContentManager.Load<Sprite>(s);

            s = aInput.ReadObject<string>();
            aExistingInstance.LowShot = aInput.ContentManager.Load<Sprite>(s);

            Dictionary<String, String> dicts = new Dictionary<String, String>();
            dicts = aInput.ReadObject<Dictionary<String, String>>();
            foreach (String key in dicts.Keys)
            {
                aExistingInstance.ChargedAttackSprites.Add(key, aInput.ContentManager.Load<Sprite>(dicts[key]));

            }

            return aExistingInstance;
            //aInput.ReadObject(value.ChargedAttackScriptPath);
        }
    }
}
