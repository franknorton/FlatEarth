using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.Rendering
{
    public class RenderObject
    {
        public FETexture Texture { get; set; }
        public Rectangle Destination { get; set; }
        public Color Color { get; set; }
        public float Y { get; set; }
    }
}
