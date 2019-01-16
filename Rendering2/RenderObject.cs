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
    class RenderObject
    {
        public FETexture Texture { get; set; }
        public Rectangle Destination { get; set; }
        public float Rotation { get; set; }
        public Color Color { get; set; }
        public Vector2 Origin { get; set; }

        public void Render(SpriteBatch sb)
        {
            Texture.Draw(sb, Destination, Color, Rotation, Origin);
        }
    }
}
