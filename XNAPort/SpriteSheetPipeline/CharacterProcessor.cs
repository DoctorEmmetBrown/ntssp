using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace SpriteSheetPipeline
{
    public class CharacterContent
    {
        public String IdleSpriteName;

        public String LeftSpriteName;

        public String RightSpriteName;

        public String ShotSpriteName;

        public String LowShotSpriteName;

        public Dictionary<String, String> ChargedAttackSpritesNames = new Dictionary<String, String>();

        public String ChargedAttackScriptPath;
    }

    public class CharacterXmlData
    {
        public string IdleSpriteName;

        public string LeftSpriteName;

        public string RightSpriteName;

        public string ShotSpriteName;

        public string LowShotSpriteName;

        public string[] ChargedAttackSpritesNames;

        public string ChargedAttackScriptPath;
    }
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    /// .
    [ContentProcessor]
    public class CharacterProcessor : ContentProcessor<CharacterXmlData, CharacterContent>
    {
        public override CharacterContent Process(CharacterXmlData input, ContentProcessorContext context)
        {
            CharacterContent result = new CharacterContent();

            result.IdleSpriteName = input.IdleSpriteName;
            result.LeftSpriteName = input.LeftSpriteName;
            result.RightSpriteName = input.RightSpriteName;
            result.ShotSpriteName = input.ShotSpriteName;
            result.LowShotSpriteName = input.LowShotSpriteName;

            for (int i = 0; i < input.ChargedAttackSpritesNames.Length; i++)
            {
                String text = input.ChargedAttackSpritesNames[i];
                string[] splited = text.Split(';');

                if (splited.Length != 2)
                {
                }

                result.ChargedAttackSpritesNames.Add(splited[0], splited[1]);
            }

            result.ChargedAttackScriptPath = input.ChargedAttackScriptPath;

            return result;
        }
    }
}