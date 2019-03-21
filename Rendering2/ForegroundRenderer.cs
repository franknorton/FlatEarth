using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace FlatEarth.Rendering2
{
    public class ForegroundRenderer : Renderer
    {
        public ForegroundRenderer(params string[] layersBackToFront) : base(Resolution.VirtualWidth, Resolution.VirtualHeight, layersBackToFront)
        {
            Resolution.OnResolutionChange += Resolution_OnResolutionChange;
        }

        private void Resolution_OnResolutionChange()
        {
            ResetRenderTarget(Resolution.VirtualWidth, Resolution.VirtualHeight);
        }
    }
}
