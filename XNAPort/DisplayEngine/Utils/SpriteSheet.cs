#region File Description
//-----------------------------------------------------------------------------
// SpriteSheet.cs
//
// Microsoft Game Technology Group
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace DisplayEngine.Utils
{
    /// <summary>
    /// A sprite sheet contains many individual sprite images, packed into different
    /// areas of a single larger texture, along with information describing where in
    /// that texture each sprite is located. Sprite sheets can make your game drawing
    /// more efficient, because they reduce the number of times the graphics hardware
    /// needs to switch from one texture to another.
    /// </summary>
    public class SpriteSheet
    {
        // Single texture contains many separate sprite images.
        Texture2D mTexture;

        // Remember where in the texture each sprite has been placed.
        Rectangle[] mSpriteRectangles;

        // Store the original sprite filenames, so we can look up sprites by name.
        Dictionary<string, int> mSpriteNames;


        /// <summary>
        /// The constructor is internal: this should only be
        /// called by the SpriteSheetReader support class.
        /// </summary>
        internal SpriteSheet(ContentReader aInput)
        {
            mTexture = aInput.ReadObject<Texture2D>();
            mSpriteRectangles = aInput.ReadObject<Rectangle[]>();
            mSpriteNames = aInput.ReadObject<Dictionary<string, int>>();
        }

        public int Count
        {
            get { return mSpriteRectangles.Length; }
        }

        /// <summary>
        /// Gets the single large texture used by this sprite sheet.
        /// </summary>
        public Texture2D Texture
        {
            get { return mTexture; }
        }


        /// <summary>
        /// Looks up the location of the specified sprite within the big texture.
        /// </summary>
        public Rectangle SourceRectangle(string aSpriteName)
        {
            int spriteIndex = GetIndex(aSpriteName);

            return mSpriteRectangles[spriteIndex];
        }


        /// <summary>
        /// Looks up the location of the specified sprite within the big texture.
        /// </summary>
        public Rectangle SourceRectangle(int aSpriteIndex)
        {
            if ((aSpriteIndex < 0) || (aSpriteIndex >= mSpriteRectangles.Length))
                throw new ArgumentOutOfRangeException("spriteIndex");

            return mSpriteRectangles[aSpriteIndex];
        }


        /// <summary>
        /// Looks up the numeric index of the specified sprite. This is useful when
        /// implementing animation by cycling through a series of related sprites.
        /// </summary>
        public int GetIndex(string aSpriteName)
        {
            int index;

            if (!mSpriteNames.TryGetValue(aSpriteName, out index))
            {
                string error = "SpriteSheet does not contain a sprite named '{0}'.";

                throw new KeyNotFoundException(string.Format(error, aSpriteName));
            }

            return index;
        }
    }
}
