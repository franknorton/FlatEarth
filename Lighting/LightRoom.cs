using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.Lighting
{
    public class LightRoom
    {

        private RenderTarget2D lightRenderTarget;
        private Effect lightEffect;
        private SpriteBatch spriteBatch;
        private SamplerState samplerState;

        public Color AmbientColor { get; set; } = Color.White;
        public bool Enabled { get; set; } = true;

        public LightRoom(GraphicsDevice graphicsDevice)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Render()
        {
            if (!Enabled)
                return;

            Renderer.Graphics.GraphicsDevice.SetRenderTarget(lightRenderTarget);
            Renderer.Graphics.GraphicsDevice.Clear(AmbientColor);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, samplerState, null, null, null, Engine.Renderer.Camera.Matrix);
            //Render lights here.
            spriteBatch.End();
            Renderer.Graphics.GraphicsDevice.SetRenderTarget(null);
        }

        public void Apply()
        {
            if (!Enabled)
                return;

            lightEffect.Parameters["lightMask"].SetValue(lightRenderTarget);
            lightEffect.CurrentTechnique.Passes[0].Apply();
        }
        
    }
}
