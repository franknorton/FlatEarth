using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.DataStructures
{
    public struct Line
    {
        public float BeginX;
        public float BeginY;
        public float EndX;
        public float EndY;

        public Line(float beginX, float beginY, float endX, float endY)
        {
            this.BeginX = beginX;
            this.BeginY = beginY;
            this.EndX = endX;
            this.EndY = endY;
        }
    }
}
