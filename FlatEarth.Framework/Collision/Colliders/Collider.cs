using FlatEarth.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision
{
    public abstract class Collider
    {
        public Vector2 Position;
        public bool IsTrigger;
        public Entity Entity;
        public bool Active = true;

        public Vector2 AbsolutePosition {
            get => Position + (Entity != null ? Entity.Position : Vector2.Zero);
        }

        public bool Collide(Entity entity)
        {
            return Collide(entity.Collider);
        }
        public bool Collide(Collider collider)
        {
            if (collider == null)
                return false;

            switch (collider) {
                case RectCollider rectCollider:
                    return Collide(rectCollider);
                case CircleCollider circleCollider:
                    return Collide(circleCollider);
                case GridCollider gridCollider:
                    return Collide(gridCollider);
                default:
                    throw new ArgumentException($"Collider is of unknown type ({collider.GetType()})");
            }
        }
        public abstract bool Collide(CircleCollider circle);
        public abstract bool Collide(RectCollider rect);
        public abstract bool Collide(GridCollider grid);
        public abstract void DebugRender();
    }
}
