using FlatEarth.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.Tiles
{
    public static class AutoTileParser
    {
        static Dictionary<byte, Point> byteToPointMap = new Dictionary<byte, Point>()
        {
            { 0 , new Point(0, 5)},
            {1 , new Point(7, 2) },
            {4 , new Point(1, 5) },
            {5 , new Point(0, 4) },
            {7 , new Point(4, 5) },
            {16 , new Point(7, 3) },
            {17 , new Point(7, 1) },
            {20 , new Point(0, 0) },
            {21 , new Point(0, 1) },
            {23 , new Point(0, 3) },
            {28 , new Point(4, 0) },
            {29 , new Point(0, 2) },
            {31 , new Point(3, 3) },
            {64 , new Point(7, 5) },
            {65 , new Point(7, 4) },
            {68 , new Point(1, 0) },
            {69 , new Point(1, 4) },
            {71 , new Point(2, 5) },
            {80 , new Point(7, 0) },
            {81 , new Point(2, 3) },
            {84 , new Point(1, 1) },
            {85 , new Point(2, 2) },
            {87 , new Point(2, 1) },
            {92 , new Point(2, 0) },
            {93 , new Point(2, 4) },
            {95 , new Point(3, 2) },
            {112 , new Point(3, 0) },
            {113 , new Point(6, 3) },
            {116 , new Point(6, 0) },
            {117 , new Point(1, 2) },
            {119 , new Point(3, 4) },
            {124 , new Point(5, 0) },
            {125 , new Point(5, 3) },
            {127 , new Point(4, 1) },
            {193 , new Point(3, 5) },
            {197 , new Point(6, 5) },
            {199 , new Point(5, 5) },
            {209 , new Point(6, 2) },
            {213 , new Point(1, 3) },
            {215 , new Point(5, 2) },
            {221 , new Point(3, 1) },
            {223 , new Point(4, 4) },
            {241 , new Point(6, 1) },
            {245 , new Point(6, 4) },
            {247 , new Point(4, 2) },
            {253 , new Point(4, 3) },
            {255 , new Point(5, 1) }
        };

        public static void Parse(Tile[][] map)
        {
            var random = new Random();
            for (int x = 0; x < map.Length; x++)
            {
                for (int y = 0; y < map[x].Length; y++)
                {
                    if (map[x][y].TileNum == 1)
                        map[x][y].TileIndex = GetTextureLocation(map, x, y);
                    else
                        map[x][y].TileIndex = new Point(random.Next(0, 2), 6);
                }
            }
        }

        static Point GetTextureLocation(Tile[][] map, int x, int y)
        {
            var tileByte = GetByte(map, x, y);
            var tileLocation = byteToPointMap[tileByte];
            return tileLocation;
        }

        static byte GetByte(Tile[][] map, int x, int y)
        {
            var startX = Math.Max(x - 1, 0);
            var startY = Math.Max(y - 1, 0);
            var endX = Math.Min(x + 1, map.Length);
            var endY = Math.Min(y + 1, map[0].Length);

            byte tileByte = 0;
            tileByte += checkNorth(map, x, y);
            tileByte += checkEast(map, x, y);
            tileByte += checkWest(map, x, y);
            tileByte += checkSouth(map, x, y);
            if (BitOperations.ContainsAll(tileByte, 5))
                tileByte += checkNorthEast(map, x, y);
            if (BitOperations.ContainsAll(tileByte, 65))
                tileByte += checkNorthWest(map, x, y);
            if (BitOperations.ContainsAll(tileByte, 20))
                tileByte += checkSouthEast(map, x, y);
            if (BitOperations.ContainsAll(tileByte, 80))
                tileByte += checkSouthWest(map, x, y);

            return tileByte;

        }

        static bool OutOfBounds(Tile[][] map, int x, int y)
        {
            return x < 0 || x >= map.Length || y < 0 || y >= map[0].Length;
        }

        static byte checkNorth(Tile[][] map, int x, int y)
        {
            if (OutOfBounds(map, x, y - 1))
                return 0;

            if (map[x][y - 1].TileNum == 1)
                return 1;

            return 0;
        }
        static byte checkEast(Tile[][] map, int x, int y)
        {
            if (OutOfBounds(map, x + 1, y))
                return 0;

            if (map[x + 1][y].TileNum == 1)
                return 4;

            return 0;
        }
        static byte checkSouth(Tile[][] map, int x, int y)
        {
            if (OutOfBounds(map, x, y + 1))
                return 0;

            if (map[x][y + 1].TileNum == 1)
                return 16;

            return 0;
        }
        static byte checkWest(Tile[][] map, int x, int y)
        {
            if (OutOfBounds(map, x - 1, y))
                return 0;

            if (map[x - 1][y].TileNum == 1)
                return 64;

            return 0;
        }
        static byte checkNorthWest(Tile[][] map, int x, int y)
        {
            if (OutOfBounds(map, x - 1, y - 1))
                return 0;

            if (map[x - 1][y - 1].TileNum == 1)
                return 128;

            return 0;
        }
        static byte checkNorthEast(Tile[][] map, int x, int y)
        {
            if (OutOfBounds(map, x + 1, y - 1))
                return 0;

            if (map[x + 1][y - 1].TileNum == 1)
                return 2;

            return 0;
        }
        static byte checkSouthEast(Tile[][] map, int x, int y)
        {
            if (OutOfBounds(map, x + 1, y + 1))
                return 0;

            if (map[x + 1][y + 1].TileNum == 1)
                return 8;

            return 0;
        }
        static byte checkSouthWest(Tile[][] map, int x, int y)
        {
            if (OutOfBounds(map, x - 1, y + 1))
                return 0;

            if (map[x - 1][y + 1].TileNum == 1)
                return 32;

            return 0;
        }
    }
}
