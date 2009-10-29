#region File Description
//-----------------------------------------------------------------------------
// SpriteSheetWriter.cs
//
// Microsoft Game Technology Group
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
#endregion

namespace SpriteSheetPipeline
{
    /// <summary>
    /// Content pipeline support class for saving sprite sheet data into XNB format.
    /// </summary>
    [ContentTypeWriter]
    public class BackgroundWriter : ContentTypeWriter<BackgroundContent>
    {
        /// <summary>
        /// Saves sprite sheet data into an XNB file.
        /// </summary>
        protected override void Write(ContentWriter aOutput, BackgroundContent aValue)
        {
            aOutput.WriteObject(aValue.Speed);
            aOutput.WriteObject(aValue.Width);
            aOutput.WriteObject(aValue.Height);
            aOutput.WriteObject(aValue.TilesContent.Length);
            if (aValue.TilesContent.Length > 0)
            {
                aOutput.WriteObject(aValue.TilesContent[0].Length);
                for (int i = 0; i < aValue.TilesContent.Length; i++)
                {
                    for (int j = 0; j < aValue.TilesContent[i].Length; j++)
                    {
                        aOutput.WriteObject(aValue.TilesContent[i][j].X);
                        aOutput.WriteObject(aValue.TilesContent[i][j].Y);
                        aOutput.WriteObject(aValue.TilesContent[i][j].Tex);
                    }
                }
            }
        }


        /// <summary>
        /// Tells the content pipeline what worker type
        /// will be used to load the sprite sheet data.
        /// </summary>
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "DisplayEngine.Display2D.BackgroundReader, " +
                   "DisplayEngine";
        }


        /// <summary>
        /// Tells the content pipeline what CLR type the sprite sheet
        /// data will be loaded into at runtime.
        /// </summary>
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return "DisplayEngine.Display2D.Background, " +
                   "DisplayEngine";
        }
    }
}
