#region File Description
//-----------------------------------------------------------------------------
// SpriteSheetWriter.cs
//
// Microsoft Game Technology Group
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
#endregion

namespace SpriteSheetPipeline
{
    /// <summary>
    /// Content pipeline support class for saving sprite sheet data into XNB format.
    /// </summary>
    [ContentTypeWriter]
    public class SpriteSheetWriter : ContentTypeWriter<SpriteSheetContent>
    {
        /// <summary>
        /// Saves sprite sheet data into an XNB file.
        /// </summary>
        protected override void Write(ContentWriter aOutput, SpriteSheetContent aValue)
        {
            aOutput.WriteObject(aValue.Texture);
            aOutput.WriteObject(aValue.SpriteRectangles.ToArray());
            aOutput.WriteObject(aValue.SpriteNames);
            aOutput.WriteObject(aValue.AnimationSpeed);
        }


        /// <summary>
        /// Tells the content pipeline what worker type
        /// will be used to load the sprite sheet data.
        /// </summary>
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "NTSSP.Utils.SpriteSheetReader, " +
                   "NTSSP";
        }


        /// <summary>
        /// Tells the content pipeline what CLR type the sprite sheet
        /// data will be loaded into at runtime.
        /// </summary>
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return "NTSSP.Utils.Sprite, " +
                   "NTSSP";
        }
    }
}
