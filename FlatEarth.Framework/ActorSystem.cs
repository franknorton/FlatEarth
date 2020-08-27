using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth
{
    static class ActorSystem
    {
        public static void ProcessActor(Actor actor)
        {
            //TODO: Check all actors for collisions every frame once to correct overlap collisions.        

            while (actor.MoveRemainderX > 1 || actor.MoveRemainderX < -1 || actor.MoveRemainderY > 1 || actor.MoveRemainderY < -1)
            {

                //Process X, check for collision, process Y check for collision.
                //TODO: If an object is placed on top of another one then we need to seperate them..
                //..currently they will just not be able to move.
                var xMove = 0;
                if (actor.MoveRemainderX > 1)
                {
                    actor.MoveRemainderX--;
                    xMove++;
                }
                else if (actor.MoveRemainderX < -1)
                {
                    actor.MoveRemainderX++;
                    xMove--;
                }

                actor.Position.X += xMove;
                /*if (actor.CollidesWithAny(out var entities))
                {
                    //Collision occured, revert movement and fire events.
                    if (entities.Any(x => !x.Collider.IsTrigger))
                    {
                        actor.Position.X -= xMove;
                        actor.DisplayPosition.X = actor.Position.X;
                        actor.MoveRemainderX = 0;
                    }
                    entities.ForEach(x => { actor.OnCollision(x); x.OnCollision(actor); });
                }*/

                var yMove = 0;
                if (actor.MoveRemainderY > 1)
                {
                    actor.MoveRemainderY--;
                    yMove++;
                }
                else if (actor.MoveRemainderY < -1)
                {
                    actor.MoveRemainderY++;
                    yMove--;
                }

                actor.Position.Y += yMove;
                /*if (actor.CollidesWithAny(out entities))
                {
                    //Collision occured, revert movement and fire events
                    if (entities.Any(x => !x.Collider.IsTrigger))
                    {
                        actor.Position.Y -= yMove;
                        actor.DisplayPosition.Y -= actor.Position.Y;
                        actor.MoveRemainderY = 0;
                    }
                    entities.ForEach(x => { actor.OnCollision(x); x.OnCollision(actor); });
                }*/
            }

            actor.DisplayPosition = actor.Position;
        }
    }
}
