using FlatEarth.ECS;
using FlatEarth.Lighting;
using FlatEarth.Particles;
using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth
{
    public class Renderer
    {
        public static GraphicsDeviceManager Graphics { get; private set; }
        private static Pool<RenderObject> renderObjectPool = new Pool<RenderObject>(2000, 25);
        public static RenderObject NewRenderObject()
        {
            return renderObjectPool.Get();
        }
        public static void RecycleRenderObject(RenderObject obj)
        {
            renderObjectPool.Put(obj);
        }


        private RenderTarget2D layerRenderTarget;
        private RenderLayerList renderLayers;
        private SpriteBatch spriteBatch;

        public BlendState BlendState;
        public SamplerState SamplerState;
        public Effect Effect;
        public Camera Camera;
        public LightRoom Lighting;

        public static event EventHandler GraphicsDeviceCreated;
        public static event EventHandler GraphicsDeviceReset;
        

        static Renderer()
        {
            Graphics = Engine.Graphics;
            Graphics.DeviceCreated += OnGraphicsDeviceCreated;
            Graphics.DeviceReset += OnGraphicsDeviceReset;
        }

        private static void OnGraphicsDeviceReset(object sender, EventArgs e)
        {
            GraphicsDeviceReset?.Invoke(sender, e);
        }
        private static void OnGraphicsDeviceCreated(object sender, EventArgs e)
        {
            GraphicsDeviceCreated?.Invoke(sender, e);
        }

        public Renderer()
        {
            renderLayers = new RenderLayerList();
            spriteBatch = new SpriteBatch(Graphics.GraphicsDevice);

            BlendState = BlendState.AlphaBlend;
            SamplerState = SamplerState.LinearClamp;
            Effect = new BasicEffect(Graphics.GraphicsDevice);
            Camera = new Camera();
            Lighting = new LightRoom(Graphics.GraphicsDevice);
            Lighting.Enabled = false;
            OnResolutionChange();
            Resolution.OnResolutionChange += OnResolutionChange;
        }

        public void OnResolutionChange()
        {
            layerRenderTarget = new RenderTarget2D(Graphics.GraphicsDevice, Resolution.VirtualWidth, Resolution.VirtualHeight);
        }

        /// <summary>
        /// Removes all render objects and layers and creates new layers.
        /// </summary>
        /// <param name="layersBottomToTop">An array of layer names where the first entry is the bottom-most layer.</param>
        public void SetLayerList(params string[] layersBottomToTop)
        {
            renderLayers.Clear();

            foreach (var layerName in layersBottomToTop)
                renderLayers.AddLayer(layerName);
        }

        public void AddObject(string layerName, FETexture texture, Point position, Point offset, Color color, float yPos)
        {
            var renderObject = NewRenderObject();
            renderObject.Texture = texture;
            renderObject.Destination = new Rectangle(position.X + offset.X, position.Y + offset.Y, texture.Width, texture.Height);
            renderObject.Color = color;
            renderObject.Y = yPos;
            renderLayers.AddObjectToLayer(layerName, renderObject);
        }

        public void AddObject(string layerName, FETexture texture, Rectangle destinationRect, Color color, float yPos)
        {
            var renderObject = NewRenderObject();
            renderObject.Texture = texture;
            renderObject.Destination = destinationRect;
            renderObject.Color = color;
            renderObject.Y = yPos;
            renderLayers.AddObjectToLayer(layerName, renderObject);
        }

        protected virtual void BeforeRender()
        {

        }

        public virtual void Render()
        {
            BeforeRender();

            //Background goes here.
            
            spriteBatch.GraphicsDevice.SetRenderTarget(layerRenderTarget);                
            spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState, SamplerState, null, null, null, null);
            renderLayers.Render(spriteBatch);
            ParticleEngine.Render(spriteBatch);
            spriteBatch.End();

            Lighting.Render();

            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            Resolution.SwitchToVirtualViewport();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState, null, null, null, Resolution.ScaleMatrix);
            Lighting.Apply();
            spriteBatch.Draw(layerRenderTarget, Vector2.Zero, Color.White);
            spriteBatch.End();

            //Transitions go here.

            AfterRender();
        }

        protected virtual void AfterRender()
        {

        }

        public static void RenderToFull(RenderTarget2D renderTarget, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        public static void RenderToVirtual(RenderTarget2D renderTarget, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null, null, Resolution.ScaleMatrix);
            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
