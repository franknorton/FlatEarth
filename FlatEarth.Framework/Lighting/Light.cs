using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Lighting
{
    public class Light
    {
        public FETexture LightMaskTexture;
        public Color Color;
        public Vector2 Position;
        public int Width, Height;

        public Light(Color color, FETexture lightTextureMask, int width, int height)
        {
            this.Color = color;
            this.LightMaskTexture = lightTextureMask;
            this.Width = width;
            this.Height = height;
        }

        public Light(Color color, Vector2 position, int width, int height)
        {
            this.Color = color;
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.LightMaskTexture = Content.DefaultResource.LoadFETexture("LightMaskDefault");
            this.Width = LightMaskTexture.Width;
            this.Height = LightMaskTexture.Height;
        }

        public void Render(SpriteBatch sb)
        {
            LightMaskTexture.Draw(sb, new Rectangle(Position.ToPoint(), new Point(Width, Height)), Color);
        }
    }
}
