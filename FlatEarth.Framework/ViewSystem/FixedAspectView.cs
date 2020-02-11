using FlatEarth.Extensions;
using FlatEarth.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.ViewSystem
{
    public class FixedAspectView : View
    {
        public AspectRatio AspectRatio { get; set; }
        public FixedAspectView(Anchor anchor, AspectRatio aspectRatio) : base(anchor)
        {
            AspectRatio = aspectRatio;
        }

        public override Rectangle GetBounds(int windowWidth, int windowHeight)
        {
            var boundsArea = AspectRatio.CalculateLargestRectangleFromBoundaries(windowWidth, windowHeight);
            var bounds = new Rectangle();
            bounds.Width = boundsArea.Width;
            bounds.Height = boundsArea.Height;
            bounds.X = Anchor.GetXPosition(bounds.Width, windowWidth);
            bounds.Y = Anchor.GetYPosition(bounds.Height, windowHeight);
            return bounds;
        }
    }
}
