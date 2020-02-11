using FlatEarth.DataStructures;
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
    public class DynamicAspectView : View
    {
        public Percentage WidthPercentage { get; set; }
        public Percentage HeightPercentage { get; set; }

        public DynamicAspectView(Anchor anchor, Percentage widthPercentage, Percentage heightPercentage) : base(anchor)
        {
            WidthPercentage = widthPercentage;
            HeightPercentage = heightPercentage;
        }

        public override Rectangle GetBounds(int containerWidth, int containerHeight)
        {
            var bounds = new Rectangle();
            bounds.Width = containerWidth * WidthPercentage;
            bounds.Height = containerHeight * HeightPercentage;
            bounds.X = Anchor.GetXPosition(bounds.Width, containerWidth);
            bounds.Y = Anchor.GetYPosition(bounds.Height, containerHeight);
            return bounds;
        }
    }
}
