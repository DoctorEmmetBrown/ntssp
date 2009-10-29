using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DisplayEngine.Display2D
{
    class BackgroundReader : ContentTypeReader<Background>
    {
        protected override Background Read(ContentReader aInput, Background aExistingInstance)
        {
            Background toRead = new Background();

            toRead.Speed = aInput.ReadObject<double>();
            toRead.mWidth = aInput.ReadObject<int>();
            toRead.mHeight = aInput.ReadObject<int>();

            int w = aInput.ReadObject<int>();
            int h = aInput.ReadObject<int>();

            toRead.Tiles = new BackgroundTile[w][];
            for (int i = 0; i < w; i++)
            {
                toRead.Tiles[i] = new BackgroundTile[h];
                for (int j = 0; j < h; j++)
                {
                    toRead.Tiles[i][j] = new BackgroundTile();
                    toRead.Tiles[i][j].X = aInput.ReadObject<int>();
                    toRead.Tiles[i][j].Y = aInput.ReadObject<int>();
                    toRead.Tiles[i][j].Tex = aInput.ReadObject<Texture2D>();
                }
            }

            return toRead;
        }
    }
}
