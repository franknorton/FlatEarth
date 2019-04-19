using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace FlatEarth.Rendering2
{
    public class BackgroundRenderer : Renderer
    {
        public BackgroundRenderer() : base(Resolution.Width, Resolution.Height)
        {
            Resolution.OnResolutionChange += Resolution_OnResolutionChange;
        }

        private void Resolution_OnResolutionChange()
        {
            ResetRenderTarget(Resolution.Width, Resolution.Height);
        }
    }
}
