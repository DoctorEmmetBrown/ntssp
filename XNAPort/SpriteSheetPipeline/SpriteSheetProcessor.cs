#region File Description
//-----------------------------------------------------------------------------
// SpriteSheetProcessor.cs
//
// Microsoft Game Technology Group
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace SpriteSheetPipeline
{
    public class SpriteSheetXMLData
    {
        public Color colorKey;
        public string[] files;

        public double animationSpeed;
    }
    /// <summary>
    /// Custom content processor takes an array of individual sprite filenames (which
    /// will typically be imported from an XML file), reads them all into memory,
    /// arranges them onto a single larger texture, and returns the resulting sprite
    /// sheet object.
    /// </summary>
    [ContentProcessor]
    public class SpriteSheetProcessor : ContentProcessor<SpriteSheetXMLData, SpriteSheetContent>
    {
        /// <summary>
        /// Converts an array of sprite filenames into a sprite sheet object.
        /// </summary>
        public override SpriteSheetContent Process(SpriteSheetXMLData aInput,
                                                   ContentProcessorContext aContext)
        {
            SpriteSheetContent spriteSheet = new SpriteSheetContent();
            List<BitmapContent> sourceSprites = new List<BitmapContent>();

            // Loop over each input sprite filename.
            foreach (string inputFilename in aInput.files)
            {
                // Store the name of this sprite.
                string spriteName = Path.GetFileNameWithoutExtension(inputFilename);

                spriteSheet.SpriteNames.Add(spriteName, sourceSprites.Count);

                // Load the sprite texture into memory.
                ExternalReference<TextureContent> textureReference =
                                new ExternalReference<TextureContent>(inputFilename);

                TextureContent texture =
                    aContext.BuildAndLoadAsset<TextureContent,
                                              TextureContent>(textureReference, null);

                ((PixelBitmapContent<Color>)texture.Faces[0][0]).ReplaceColor(aInput.colorKey, Color.TransparentBlack);

                sourceSprites.Add(texture.Faces[0][0]);
            }

            spriteSheet.AnimationSpeed = aInput.animationSpeed;

            // Pack all the sprites into a single large texture.
            BitmapContent packedSprites = SpritePacker.PackSprites(sourceSprites,
                                                spriteSheet.SpriteRectangles, aContext);

            spriteSheet.Texture.Mipmaps.Add(packedSprites);

            return spriteSheet;
        }
    }
}
