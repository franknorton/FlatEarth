using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.DataStructures
{
    public struct Circle
    {
        public float X;
        public float Y;
        public float Radius;

        public Circle(float X, float Y, float radius)
        {
            this.X = X;
            this.Y = Y;
            this.Radius = radius;
        }
    }
}
