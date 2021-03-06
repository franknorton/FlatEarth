﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlatEarth.Screens.Transitions
{
    public class TransitionCrossFade : ScreenTransition
    {
        public TransitionCrossFade(int lengthInMilliseconds) : base(lengthInMilliseconds)
        {

        }

        public override void Draw(RenderTarget2D target, SpriteBatch spriteBatch, RenderTarget2D currentScreen, RenderTarget2D newScreen)
        {
            spriteBatch.SetRenderTargetAndClear(target, Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(currentScreen, Vector2.Zero, Color.White * (1 - PercentDone));
            spriteBatch.Draw(newScreen, Vector2.Zero, Color.White * PercentDone);
            spriteBatch.End();
        }
    }
}
