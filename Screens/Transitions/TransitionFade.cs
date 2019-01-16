using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatEarth.Rendering2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlatEarth.Screens.Transitions
{
    public class TransitionFade : ScreenTransition
    {
        public Color fadeToColor;

        public TransitionFade(int lengthInMilliseconds) : this(lengthInMilliseconds, Color.Black) { }
        public TransitionFade(int lengthInMilliseconds, Color fadeToColor) : base(lengthInMilliseconds)
        {
            this.fadeToColor = fadeToColor;
        }

        public override void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, RenderTarget2D currentScreen, RenderTarget2D nextScreen)
        {
            graphicsDevice.Clear(fadeToColor);

            var currentOpacity = 1 - MathHelper.Clamp(PercentDone / 0.4f, 0, 1);
            var nextOpacity = MathHelper.Clamp(PercentDone - 0.6f, 0, 1) / 0.4f;

            spriteBatch.Begin();
            spriteBatch.Draw(currentScreen, Vector2.Zero, Color.White * currentOpacity);
            spriteBatch.Draw(nextScreen, Vector2.Zero, Color.White * nextOpacity);
            spriteBatch.End();
        }
    }
}
