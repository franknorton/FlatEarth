using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision.Colliders
{
    /// <summary>
    /// An infinite grid of collidable cells.
    /// </summary>
    public class GridCollider : Collider
    {
        InfiniteGrid<bool> grid;
        public float CellWidth { get; set; }
        public float CellHeight { get; set; }

        public GridCollider(int cellWidth, int cellHeight) {
            grid = new InfiniteGrid<bool>();
            CellWidth = cellWidth;
            CellHeight = cellHeight;
        }

        public void Set(Point cell, bool collidable) {
            if (collidable)
                grid.Set(cell, true);
            else
                grid.Remove(cell);
        }
        public void Set(Point[] cells, bool collidable) {
            if (collidable)
                Array.ForEach(cells, (cell => grid.Set(cell, true)));
            else
                Array.ForEach(cells, (cell => grid.Remove(cell)));
        }
        public void Clear() {

        }

        //Rectangle
        public bool Collide(float left, float top, float right, float bottom) {
            //Need to check every tile that this rect contains for collisions.
            var startX = (int)(left / CellWidth);
            var startY = (int)(top / CellHeight);
            var endX = (int)(right / CellWidth);
            var endY = (int)(bottom / CellHeight);

            var key = new Point(0, 0);
            for (int x = startX; x <= endX; x++) {
                for (int y = startY; y <= endY; y++) {
                    key.X = x;
                    key.Y = y;
                    if (grid.Contains(key))
                        return true;
                }
            }
            return false;
        }
        public bool Collide(Rectangle rect) { return Collide(rect.Left, rect.Top, rect.Right, rect.Bottom); }

        public bool this[int x, int y]
        {
            get
            {
                return grid[x, y];
            }
            set
            {
                grid[x, y] = value;
            }
        }
    }
}
