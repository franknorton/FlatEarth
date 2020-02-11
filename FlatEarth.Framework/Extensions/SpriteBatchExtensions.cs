using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void SetRenderTargetAndClear(this SpriteBatch sb, RenderTarget2D target, Color clearColor)
        {
            sb.GraphicsDevice.SetRenderTarget(target);
            sb.GraphicsDevice.Clear(clearColor);
        }

        public static void SetRenderTarget(this SpriteBatch sb, RenderTarget2D target)
        {
            sb.GraphicsDevice.SetRenderTarget(target);
        }
    }
}
