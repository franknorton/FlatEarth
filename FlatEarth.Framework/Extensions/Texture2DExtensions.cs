using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Extensions
{
    public static class Texture2DExtensions
    {
        public static FETexture ToFETexture(this Texture2D texture)
        {
            return new FETexture(texture);
        }

        public static void RenderToForeground(this RenderTarget2D target, SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Resolution.ScaleMatrix);
            sb.Draw(target, Vector2.Zero, Color.White);
            sb.End();
        }

        public static void RenderToBackground(this RenderTarget2D target, SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null, null);
            sb.Draw(target, Vector2.Zero, Color.White);
            sb.End();
        }
    }
}
