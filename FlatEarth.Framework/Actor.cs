using FlatEarth.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth
{
    public class Actor : Entity
    {
        public float MoveRemainderX;
        public float MoveRemainderY;

        public Actor() {

        }

        public override void Move(float x, float y) {
            MoveRemainderX += x;
            MoveRemainderY += y;
            //DisplayPosition.X += x;
            //DisplayPosition.Y += y;
        }
        public override void Move(Vector2 movement)
        {
            MoveRemainderX += movement.X;
            MoveRemainderY += movement.Y;
            //DisplayPosition += movement;
        }

        public override void Update()
        {
            base.Update();

            ActorSystem.ProcessActor(this); //Handles movement and collision events.
        }
    }
}
