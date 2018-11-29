using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.Tiles
{
    public class Tileset
    {
        private FETexture[,] tiles;
        public Texture2D TileSheet { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int TileBuffer { get; set; }

        public Tileset(Texture2D tileSheet, int tileWidth, int tileHeight, int tileBuffer)
        {
            TileBuffer = tileBuffer;
            TileSheet = tileSheet;
            TileWidth = tileWidth;
            TileHeight = tileHeight;

            var tilesInRow = tileSheet.Width / (tileWidth + tileBuffer + tileBuffer);
            var tilesInColumn = tileSheet.Height / (tileHeight + tileBuffer + tileBuffer);

            tiles = new FETexture[tileSheet.Width / tileWidth, tileSheet.Height / tileHeight];
            for (int x = 0; x < tilesInRow; x++)
            {
                for (int y = 0; y < tilesInColumn; y++)
                {
                    tiles[x, y] = new FETexture(TileSheet, new Rectangle(x * (tileWidth + tileBuffer * 2) + tileBuffer, y * (tileHeight + tileBuffer * 2) + tileBuffer, tileWidth, tileHeight));
                }
            }
        }

        public FETexture this[int x, int y]
        {
            get
            {
                return tiles[x, y];
            }
        }

        public FETexture this[int index]
        {
            get
            {
                return tiles[index % tiles.GetLength(0), index / tiles.GetLength(0)];
            }
        }

    }
}
