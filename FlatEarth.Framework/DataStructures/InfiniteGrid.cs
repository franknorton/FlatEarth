using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth
{
    public class InfiniteGrid<T>
    {
        public Dictionary<Point, T> Cells;
        public InfiniteGrid()
        {
            Cells = new Dictionary<Point, T>();
        }

        public void Set(int x, int y, T obj) { Set(new Point(x, y), obj); }
        public void Set(Point cell, T obj)
        {
            if (Cells.ContainsKey(cell))
                Cells[cell] = obj;
            else
                Cells.Add(cell, obj);
        }
        
        public T Get(int x, int y) { return Get(new Point(x, y)); }
        public T Get(Point cell)
        {
            if (Cells.TryGetValue(cell, out var val))
                return val;
            else
                throw new ArgumentOutOfRangeException($"No cell value exists at {cell.X}, {cell.Y}");
        }
        public bool TryGet(Point cell, out T obj)
        {
            if (Cells.TryGetValue(cell, out var val))
            {
                obj = val;
                return true;
            }

            obj = default(T);
            return false;
        }
        public bool Remove(Point cell)
        {
            return Cells.Remove(cell);
        }
        public bool Contains(Point cell)
        {
            return Cells.ContainsKey(cell);
        }
        public void Clear()
        {
            Cells.Clear();
        }

        public T this[int x, int y]
        {
            get => Get(new Point(x, y));
            set => Set(new Point(x, y), value);
        }
    }
}
