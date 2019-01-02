using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Screens
{
    public abstract class ScreenTransition
    {
        public int lengthInMilliseconds;
        public float milliseconds;

        public float PercentDone => MathHelper.Clamp(milliseconds / lengthInMilliseconds, 0, 1.0f);
        public bool Done => milliseconds / lengthInMilliseconds >= 1;

        public ScreenTransition(int lengthInMilliseconds)
        {
            this.lengthInMilliseconds = lengthInMilliseconds;
        }

        public virtual void Update(GameTime gameTime)
        {
            milliseconds += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public abstract void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, RenderTarget2D currentScreen, RenderTarget2D nextScreen);
    }
}
