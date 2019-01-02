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
        private List<Light> lights;

        public Color AmbientColor { get; set; } = Color.White;
        public bool Enabled { get; set; } = true;

        public LightRoom(GraphicsDevice graphicsDevice)
        {
            lights = new List<Light>();
            spriteBatch = new SpriteBatch(graphicsDevice);
            lightEffect = Content.DefaultResource.LoadEffect("LightEffect");

            OnResolutionChange();
            Resolution.OnResolutionChange += OnResolutionChange;
        }

        public void OnResolutionChange()
        {
            lightRenderTarget = new RenderTarget2D(Renderer.Graphics.GraphicsDevice, Resolution.VirtualWidth, Resolution.VirtualHeight);
        }

        public void Render()
        {
            if (!Enabled)
                return;

            Renderer.Graphics.GraphicsDevice.SetRenderTarget(lightRenderTarget);
            Renderer.Graphics.GraphicsDevice.Clear(AmbientColor);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, samplerState, null, null, null, Engine.Renderer.Camera.Matrix);
            lights.ForEach(light => light.Render(spriteBatch));
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
