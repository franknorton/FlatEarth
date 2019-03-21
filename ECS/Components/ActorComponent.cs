using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.ECS.Components
{
    public class ActorComponent : Component
    {
        public Vector2 MoveRemainder;
        public short CollisionLayer = 0;

        public ActorComponent()
        {
        }

        public void Move(float x, float y)
        {
            MoveRemainder.X += x;
            MoveRemainder.Y += y;
        }

        public void Move(Vector2 movement)
        {
            MoveRemainder += movement;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
