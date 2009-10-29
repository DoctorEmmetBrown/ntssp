#region File Description
//-----------------------------------------------------------------------------
// SpriteSheetContent.cs
//
// Microsoft Game Technology Group
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace SpriteSheetPipeline
{
    public class BackgroundTileContent
    {
        public int X
        {
            get { return mX; }
            set { mX = value; }
        }

        public int Y
        {
            get { return mY; }
            set { mY = value; }
        }

        public Texture2DContent Tex
        {
            get { return mTex; }
            set { mTex = value; }
        }

        private int mX;
        private int mY;
        private Texture2DContent mTex;
    }

    /// <summary>
    /// Build-time type used to hold the output data from the SpriteSheetProcessor.
    /// This is saved into XNB format by the SpriteSheetWriter helper class, then
    /// at runtime, the SpriteSheetReader loads the data into a SpriteSheet object.
    /// </summary>
    public class BackgroundContent
    {
        // Single texture contains many separate sprite images.
        public BackgroundTileContent[][] TilesContent;

        public double Speed = 0;
        public int Width = 0;
        public int Height = 0;
    }
}
