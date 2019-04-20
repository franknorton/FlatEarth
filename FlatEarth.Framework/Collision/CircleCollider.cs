using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision
{
    public class CircleCollider : Collider
    {
        private float radius;
        public float Radius { get => radius; set => radius = value; }

        public CircleCollider(float radius, float x = 0, float y = 0)
        {
            Radius = radius;
            Position.X = x;
            Position.Y = y;
        }

        public override bool Collide(CircleCollider circle)
        {
            return Collision.Collide.CircleToCircle(this, circle);
        }

        public override bool Collide(RectCollider rect)
        {
            return Collision.Collide.CircleToRectangle(this, rect);
        }

        public override bool Collide(GridCollider grid)
        {
            throw new NotImplementedException();
        }

        public override void DebugRender()
        {
            DebugDraw.Circle(AbsolutePosition, Radius, Color.White);
        }
    }
}
