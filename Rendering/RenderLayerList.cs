using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.Rendering
{
    public class RenderLayerList
    {
        private List<RenderLayer> renderLayers;
        private Dictionary<string, RenderLayer> renderLayersMap;

        public RenderLayerList()
        {
            renderLayers = new List<RenderLayer>();
            renderLayersMap = new Dictionary<string, RenderLayer>();
        }

        public void AddLayer(string layerName)
        {
            var renderLayer = new RenderLayer(layerName);
            renderLayers.Add(renderLayer);
            renderLayersMap.Add(layerName, renderLayer);
        }

        public void AddObjectToLayer(string layerName, RenderObject obj)
        {
            if (renderLayersMap.TryGetValue(layerName, out var layer))
            {
                layer.AddRenderObject(obj);
            }
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            renderLayers.ForEach((layer) =>
            {
                layer.Render(spriteBatch);
            });
        }

        public virtual void Clear()
        {
            ClearLayers();
            renderLayers.Clear();
            renderLayersMap.Clear();
        }

        public virtual void ClearLayers()
        {
            renderLayers.ForEach((layer) => layer.Clear());
        }
    }
}
