using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.ECS.Components
{
    public class SpriteComponent : Component
    {
        public FETexture texture;
        public int Width;
        public int Height;
        public Color Color;
        public string LayerName;
        public int OffsetX;
        public int OffsetY;

        public SpriteComponent(FETexture texture, string layerName, int offsetX = 0, int offsetY = 0)
        {
            this.texture = texture;
            Width = texture.Width;
            Height = texture.Height;
            Color = Color.White;
            LayerName = layerName;
            OffsetX = offsetX;
            OffsetY = offsetY;
        }

        public override void Render()
        {
            var offsetX = -texture.Width / 2;
            var offsetY = -texture.Height / 2;
            var destRect = new Rectangle(Entity.DisplayPosition.ToPoint() + new Point(OffsetX, OffsetY), new Point(Width, Height));
            Engine.Renderer.AddObject(
                LayerName,
                texture,
                destRect,
                Color,
                (int)((Entity.Position.Y + texture.Height - offsetY) / 16) * 16);
        }
    }
}
