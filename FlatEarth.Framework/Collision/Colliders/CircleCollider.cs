using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision.Colliders
{
    public class CircleCollider : Collider
    {
        private float radius;

        public float Radius { get => radius; set => radius = value; }
        public float Left => Position.X - Radius;
        public float Right => Position.X + Radius;
        public float Top => Position.Y - Radius;
        public float Bottom => Position.Y + Radius;

        public CircleCollider(float radius, float x, float y, bool isTrigger = false) : base(x, y, false)
        {
            Radius = radius;
        }
    }
}
  