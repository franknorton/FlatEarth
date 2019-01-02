using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Screens.Transitions
{
    public class TransitionGrow : ScreenTransition
    {
        public TransitionGrow(int lengthInMilliseconds) : base(lengthInMilliseconds)
        {
        }

        public override void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, RenderTarget2D current, RenderTarget2D next)
        {
            var newX = (int)((next.Width / 2) * (1 - PercentDone));
            var newY = (int)((next.Height / 2) * (1 - PercentDone));
            var newWidth = (int)(next.Width * PercentDone);
            var newHeight = (int)(next.Height * PercentDone);
            var nextRect = new Rectangle(newX, newY, newWidth, newHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(current, Vector2.Zero, Color.White);
            spriteBatch.Draw(next, nextRect, Color.White);
            spriteBatch.End();
        }
    }
}
