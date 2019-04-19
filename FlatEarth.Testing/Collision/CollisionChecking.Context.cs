using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Testing.Collision
{
    
    class CollisionCheckingContext
    {
        public CollisionCheckingContext() { }

        public Rectangle RectangleBase = new Rectangle()
        {
            X = 10,
            Y = 10,
            Width = 20,
            Height = 10
        };
        public Rectangle RectangleSeperate = new Rectangle()
        {
            X = 40,
            Y = 10,
            Width = 20,
            Height = 10
        };
        public Rectangle RectangleCollide = new Rectangle()
        {
            X = 25,
            Y = 15,
            Width = 20,
            Height = 10
        };
    }
}
