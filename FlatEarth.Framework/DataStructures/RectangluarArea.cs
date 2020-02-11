using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.DataStructures
{
    public class RectangluarArea
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public RectangluarArea() { }
        public RectangluarArea(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
