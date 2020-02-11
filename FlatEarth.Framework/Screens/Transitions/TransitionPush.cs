using FlatEarth.Extensions;
using FlatEarth.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Screens.Transitions
{
    public class TransitionPush : ScreenTransition
    {
        public Direction Direction;
        public TransitionPush(int lengthInMilliseconds, Direction direction) : base(lengthInMilliseconds)
        {
            Direction = direction;
        }

        public override void Draw(RenderTarget2D target, SpriteBatch spriteBatch, RenderTarget2D current, RenderTarget2D next)
        {
            Vector2 currentOffset = Vector2.Zero;
            Vector2 nextOffset = Vector2.Zero;

            switch (Direction)
            {
                case Direction.Up:
                    currentOffset = new Vector2(0, -(current.Height * PercentDone));
                    nextOffset = new Vector2(0, current.Height * (1 - PercentDone));
                    break;
                case Direction.Down:
                    currentOffset = new Vector2(0, (current.Height * PercentDone));
                    nextOffset = new Vector2(0, -current.Height * (1 - PercentDone));
                    break;
                case Direction.Left:
                    currentOffset = new Vector2(-(current.Width * PercentDone), 0);
                    nextOffset = new Vector2(current.Width * (1 - PercentDone), 0);
                    break;
                case Direction.Right:
                    currentOffset = new Vector2(current.Width * PercentDone, 0);
                    nextOffset = new Vector2(-current.Width * (1 - PercentDone), 0);
                    break;
            }

            spriteBatch.SetRenderTargetAndClear(target, Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(current, currentOffset, Color.White);
            spriteBatch.Draw(next, nextOffset, Color.White);
            spriteBatch.End();
        }
    }
}
