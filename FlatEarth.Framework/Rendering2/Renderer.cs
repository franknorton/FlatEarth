using FlatEarth.Extensions;
using FlatEarth.Lighting;
using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Rendering2
{
    public class Renderer
    {
        private RenderTarget2D layerTarget;
        private RenderTarget2D lightTarget;
        private RenderTarget2D finalTarget;
        private SpriteBatch spriteBatch;
        private Pool<RenderObject> renderObjectPool;
        private Dictionary<string, List<RenderObject>> renderLayers;
        private LightRoom lightRoom;

        public Color ClearColor { get; set; } = Color.CornflowerBlue;
        public Camera Camera { get; set; }

        public Renderer(int viewWidth, int viewHeight, params string[] layersBackToFront)
        {
            layerTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, viewWidth, viewHeight);
            lightTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, viewWidth, viewHeight);
            finalTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, viewWidth, viewHeight);
            spriteBatch = new SpriteBatch(Engine.Graphics.GraphicsDevice);
            renderObjectPool = new Pool<RenderObject>(1000, 150);
            renderLayers = new Dictionary<string, List<RenderObject>>();
            Camera = new Camera(viewWidth, viewHeight);
            lightRoom = new LightRoom(Engine.Graphics.GraphicsDevice);

            foreach (var layerName in layersBackToFront)
                renderLayers.Add(layerName, new List<RenderObject>());
        }

        protected void ResetRenderTarget(int width, int height)
        {
            layerTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, width, height);
            lightTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, width, height);
            finalTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, width, height);
            Camera.ResetViewport(width, height);
        }

        public void AddTexture(string layerName, FETexture texture, Rectangle destination, Vector2 origin, float rotation, Color color)
        {
            if (renderLayers.TryGetValue(layerName, out var layer))
            {
                var pooledObject = renderObjectPool.Get();
                pooledObject.Texture = texture;
                pooledObject.Destination = destination;
                pooledObject.Origin = origin;
                pooledObject.Rotation = rotation;
                pooledObject.Color = color;

                layer.Add(pooledObject);
            }
            else
                throw new ArgumentException($"No layer exists with name '{layerName}'.");
        }

        public void AddLight(Light light)
        {
            lightRoom.AddLight(light);
        }
        public void AddLight(Color color, Vector2 position, int width, int height)
        {
            lightRoom.AddLight(new Light(color, position, width, height));
        }

        public void Clear()
        {
            foreach(var layer in renderLayers.Values)
            {
                foreach(var renderObject in layer)
                {
                    renderObjectPool.Put(renderObject);
                }
            }

            foreach (var layer in renderLayers.Values)
                layer.Clear();

            lightRoom.Clear();
        }

        public RenderTarget2D Render()
        {
            spriteBatch.SetRenderTargetAndClear(layerTarget, ClearColor);

            //Render all objects by layers back to front.
            spriteBatch.Begin();
            foreach (var layer in renderLayers.Values)
                RenderLayer(layer);
            spriteBatch.End();

            //Render Lights
            //I think the lighting should be baked into the renderer, so an Add light function. 
            var lightTarget = lightRoom.Render();

            //Create the final render target from lighting and layers.
            spriteBatch.SetRenderTargetAndClear(finalTarget, ClearColor);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            lightRoom.Apply();
            spriteBatch.Draw(layerTarget, Vector2.Zero, Color.White);
            spriteBatch.End();

            spriteBatch.SetRenderTarget(null);
            
            Clear();
            return finalTarget;
        }

        private void RenderLayer(List<RenderObject> layerObjects)
        {
            layerObjects.ForEach(o => o.Render(spriteBatch));
        }
    }
}
