using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.Rendering
{
    public class RenderLayer
    {
        protected List<RenderObject> renderObjects;

        public string Name { get; set; }

        public RenderLayer(string name)
        {
            Name = name;
            renderObjects = new List<RenderObject>();
        }

        public virtual void AddRenderObject(RenderObject obj)
        {
            renderObjects.Add(obj);
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            foreach(var obj in renderObjects)
            {
                obj.Texture.Draw(spriteBatch, obj.Destination, obj.Color);
            }
        }

        public virtual void Clear()
        {
            renderObjects.ForEach(obj => Renderer.RecycleRenderObject(obj));
            renderObjects.Clear();
        }
    }
}
