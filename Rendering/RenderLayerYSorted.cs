using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.Rendering
{
    public class RenderLayerYSorted : RenderLayer
    {
        public RenderLayerYSorted(string name) : base(name)
        {
        }

        public override void Render(SpriteBatch sb)
        {
            renderObjects.Sort((a, b) => a.Y.CompareTo(b.Y));
            base.Render(sb);
        }
    }
}
