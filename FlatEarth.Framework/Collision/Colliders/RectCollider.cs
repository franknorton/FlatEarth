using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision.Colliders
{
    public class RectCollider : Collider
    {
        private float width;
        private float height;

        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }

        public float Left => Position.X;
        public float Right => Position.X + Width;
        public float Top => Position.Y;
        public float Bottom => Position.Y + Height;

        public RectCollider(Vector2 position, float width, float height, bool isTrigger = false) : base(position, isTrigger)
        {
            this.width = width;
            this.height = height;
        }
        public RectCollider(float x, float y, float width, float height, bool isTrigger = false) : base(x, y, isTrigger)
        {
            this.width = width;
            this.height = height;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
        }
    }
}
