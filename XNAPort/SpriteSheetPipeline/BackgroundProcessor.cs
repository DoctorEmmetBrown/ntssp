#region File Description
//-----------------------------------------------------------------------------
// SpriteSheetProcessor.cs
//
// Microsoft Game Technology Group
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace SpriteSheetPipeline
{
    public class BackgroundXMLData
    {
        public string file;

        public double speed;
    }
    /// <summary>
    /// Custom content processor takes an array of individual sprite filenames (which
    /// will typically be imported from an XML file), reads them all into memory,
    /// arranges them onto a single larger texture, and returns the resulting sprite
    /// sheet object.
    /// </summary>
    [ContentProcessor]
    public class BackgroundProcessor : ContentProcessor<BackgroundXMLData, BackgroundContent>
    {
        private const int TILE_SIZE = 512;
        /// <summary>
        /// Converts an array of sprite filenames into a sprite sheet object.
        /// </summary>
        public override BackgroundContent Process(BackgroundXMLData aInput, ContentProcessorContext aContext)
        {
            BackgroundContent output = new BackgroundContent();


            FreeImageAPI.FIBITMAP b = FreeImageAPI.FreeImage.LoadEx(aInput.file);

            FreeImageAPI.BITMAPINFO info = FreeImageAPI.FreeImage.GetInfoEx(b);


            //Resize to power of two
            int newWidth = RoundUpPowerOf2(info.bmiHeader.biWidth);
            int newHeight = RoundUpPowerOf2(info.bmiHeader.biHeight);

            b = FreeImageAPI.FreeImage.Rescale(b, newWidth, newHeight, FreeImageAPI.FREE_IMAGE_FILTER.FILTER_BICUBIC);
            b = FreeImageAPI.FreeImage.RotateClassic(b, 180);
            FreeImageAPI.FreeImage.FlipHorizontal(b);

            int nbW = 1, nbH = 1;
            if (newWidth > TILE_SIZE)
            {
                nbW = newWidth / TILE_SIZE;
            }
            if (newHeight > TILE_SIZE)
            {
                nbH = newHeight / TILE_SIZE;
            }

            output.Width = newWidth;
            output.Height = newHeight;

            BackgroundTileContent[][] tilesContent = new BackgroundTileContent[nbW][];
            for (int i = 0; i < nbW; i++)
            {
                tilesContent[i] = new BackgroundTileContent[nbH];
                for(int j = 0;j < nbH;j++)
                {
                    BackgroundTileContent tileContent = new BackgroundTileContent();
                    tileContent.X = i*512;
                    tileContent.Y = j*512;
                    tileContent.Tex = new Texture2DContent();
                    BitmapContent tile = new PixelBitmapContent<Color>(Math.Min(TILE_SIZE, newWidth), Math.Min(TILE_SIZE, newHeight));

                    Rectangle source = new Rectangle(i * TILE_SIZE, j * TILE_SIZE, Math.Min(TILE_SIZE, newWidth), Math.Min(TILE_SIZE, newHeight));
                    Rectangle dest = new Rectangle(0, 0, Math.Min(TILE_SIZE, newWidth), Math.Min(TILE_SIZE, newHeight));


                    for(int k = 0;k<Math.Min(512, newWidth);k++)
                    {
                        for(int l=0;l<Math.Min(512, newHeight);l++)
                        {
                            FreeImageAPI.RGBQUAD rgb;
                            FreeImageAPI.FreeImage.GetPixelColor(b, (uint)(tileContent.X + k),(uint) (tileContent.Y + l), out rgb);
                            ((PixelBitmapContent<Color>)tile).SetPixel(k,l,new Color(rgb.rgbRed,rgb.rgbGreen,rgb.rgbBlue));
                            
                        }
                    }
                    tileContent.Tex.Mipmaps.Add(tile);

                    tilesContent[i][j] = tileContent; 
                }
            }
            output.TilesContent = tilesContent;
            output.Speed = aInput.speed;

            return output;
        }

        // from http://graphics.stanford.edu/~seander/bithacks.html#DetermineIfPowerOf2
        private static bool IsPowerOfTwo(int n)
        {
            return ((n & (n - 1)) == 0);
        }

        // from http://graphics.stanford.edu/~seander/bithacks.html#RoundUpPowerOf2
        private static int RoundUpPowerOf2(int n)
        {
            n--;
            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;
            n++;

            return n;
        }
    }
}
