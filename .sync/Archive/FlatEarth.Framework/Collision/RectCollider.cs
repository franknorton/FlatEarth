using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision
{
    public class RectCollider : Collider
    {
        private float width;
        private float height;

        public float Width { get => width; set => width = value; }
        public float Height { get => height; set => height = value; }

        public RectCollider(float width, float height, float x = 0, float y = 0)
        {
            this.width = width;
            this.height = height;
            this.Position.X = x;
            this.Position.Y = y;
        }

        public override bool Collide(CircleCollider circle)
        {
            return Collision.Collide.RectToCircle(this, circle);
        }

        public override bool Collide(RectCollider rect)
        {
            return Collision.Collide.RectToRect(this, rect);
        }

        public override bool Collide(GridCollider grid)
        {
            return grid.Collide(this);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
        }

        public override void DebugRender()
        {
            DebugDraw.Rectangle(AbsolutePosition, Width, Height, Color.White, true);
        }
    }
}
