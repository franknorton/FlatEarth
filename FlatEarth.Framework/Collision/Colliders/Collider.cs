using FlatEarth.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision.Colliders
{
    public abstract class Collider : ICollider
    {
        protected Vector2 position;
        protected bool isTrigger;

        /// <summary>
        /// Initializes the collider at position 0,0.
        /// </summary>
        public Collider()
        {
            position = Vector2.Zero;
            isTrigger = false;
        }
        /// <summary>
        /// Initializes the collider at the specified position.
        /// </summary>
        /// <param name="position">The initial position.</param>
        /// <param name="isTrigger">Whether this collider is a trigger. Triggers detect but do not respond to collisions.</param>
        public Collider(Vector2 position, bool isTrigger)
        {
            this.position = position;
            this.isTrigger = isTrigger;
        }
        /// <summary>
        /// Initializes the collider at the specified position.
        /// </summary>
        /// <param name="x">The initial x position.</param>
        /// <param name="y">The initial y position.</param>
        /// <param name="isTrigger">Whether this collider is a trigger. Triggers detect but do not respond to collisions.</param>
        public Collider(float x, float y, bool isTrigger)
        {
            position = new Vector2(x, y);
            this.isTrigger = isTrigger;
        }

        public Vector2 Position { get => position; set => position = value; }
        public bool IsTrigger { get => isTrigger; set => isTrigger = value; }

        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        public void SetPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public void MovePosition(Vector2 moveAmount)
        {
            position += moveAmount;
        }
        public void MovePosition(float xAmount, float yAmount)
        {
            position.X += xAmount;
            position.Y += yAmount;
        }
    }
}
