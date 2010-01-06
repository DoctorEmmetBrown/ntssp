using System;
using System.Collections.Generic;
using System.Linq;
using DisplayEngine.Display2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using ScriptEngine;
using Data;
using DisplayEngine;

namespace DefaultScript
{
    public class DefaultSuperAttack : Script
    {
        TimeSpan mSuperAttackTime = new TimeSpan();
        private bool mStarted = false;

        public DefaultSuperAttack():base("DefaultSuperAttack")
        {
        }

        public override bool Execute(Game aGame, GameTime aTime, Dictionary<String, Object> aObjects, ref Dictionary<String,Object> aResults)
        {
            Vector2 coord = (Vector2)aObjects["Coord"];
            Character charact = (Character)aObjects["Character"];
            int numPlayer = (int) aObjects["NumPlayer"];
            aResults.Add("Collisions",new List<Sprite>());
            ((List<Sprite>)aResults["Collisions"]).Add(charact.ChargedAttackSprites["DefaultSpriteSuperAttack"]);

            if(!mStarted)
            {
                mSuperAttackTime = new TimeSpan(aTime.TotalGameTime.Ticks);
                mSuperAttackTime = mSuperAttackTime.Add(new TimeSpan(0, 0, 3));
                mStarted = true;
                charact.ChargedAttackSprites["DefaultSpriteSuperAttack"].Visible = true;
                DisplayManager.Instance.DrawableLevelManager.AddSprite(charact.ChargedAttackSprites["DefaultSpriteSuperAttack"], 50, numPlayer);
                charact.ChargedAttackSprites["DefaultSpriteSuperAttack"].X = (int)coord.X;
                charact.ChargedAttackSprites["DefaultSpriteSuperAttack"].Y = (int)coord.Y - DisplayManager.Instance.ScreenSplitter.TranslateHeightFromScreen(charact.ChargedAttackSprites["DefaultSpriteSuperAttack"].SpriteSheet.SourceRectangle(0).Height, numPlayer);
            }
            else
            {             
                if(aTime.TotalGameTime >= mSuperAttackTime)
                {
                    mStarted = false;
                    charact.ChargedAttackSprites["DefaultSpriteSuperAttack"].Visible = false;
                    DisplayManager.Instance.DrawableLevelManager.RemoveSprite(50, charact.ChargedAttackSprites["DefaultSpriteSuperAttack"], numPlayer);
                    return false;
                }
                else
                {
                    charact.ChargedAttackSprites["DefaultSpriteSuperAttack"].X = (int)coord.X;
                    charact.ChargedAttackSprites["DefaultSpriteSuperAttack"].Y = (int)coord.Y - DisplayManager.Instance.ScreenSplitter.TranslateHeightFromScreen(charact.ChargedAttackSprites["DefaultSpriteSuperAttack"].SpriteSheet.SourceRectangle(0).Height, numPlayer);
                }
            }

            return true;
        }
    }
}
