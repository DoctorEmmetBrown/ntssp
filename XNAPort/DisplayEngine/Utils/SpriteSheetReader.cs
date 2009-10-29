#region File Description
//-----------------------------------------------------------------------------
// SpriteSheetReader.cs
//
// Microsoft Game Technology Group
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework.Content;
#endregion

using DisplayEngine.Display2D;
namespace DisplayEngine.Utils
{
    /// <summary>
    /// Content pipeline support class for reading sprite sheet data from XNB format.
    /// </summary>
    public class SpriteSheetReader : ContentTypeReader<Sprite>
    {
        /// <summary>
        /// Loads sprite sheet data from an XNB file.
        /// </summary>
        protected override Sprite Read(ContentReader aInput,
                                            Sprite aExistingInstance)
        {
            SpriteSheet s= new SpriteSheet(aInput);
            Sprite sprite = new Sprite(aInput.ReadDouble(),s);
            return sprite;
        }
    }
}
