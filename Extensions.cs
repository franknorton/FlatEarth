using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth
{
    public static class Extensions
    {

        #region Texture2D
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
        #region


        #region SpriteBatch
        public static void SetRenderTargetAndClear(this SpriteBatch sb, RenderTarget2D target, Color clearColor)
        {
            sb.GraphicsDevice.SetRenderTarget(target);
            sb.GraphicsDevice.Clear(clearColor);
        }
        #region
    }
}
