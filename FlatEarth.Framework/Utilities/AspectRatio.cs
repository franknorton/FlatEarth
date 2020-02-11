using FlatEarth.DataStructures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Utilities
{
    public class AspectRatio
    {

        public float WidthRatio { get; protected set; }
        public float HeightRatio { get; protected set; }

        public AspectRatio(float width, float height)
        {
            WidthRatio = width / height;
            HeightRatio = 1 / WidthRatio;
        }

        public float CalculateWidthFromHeight(float height)
        {
            return height * WidthRatio;
        }
        public float CalculateHeightFromWidth(float width)
        {
            return width * HeightRatio;
        }
        public RectangluarArea CalculateLargestRectangleFromBoundaries(int maxWidth, int maxHeight)
        {
            var largestRectangle = new RectangluarArea();
            if(maxWidth / WidthRatio < maxHeight / HeightRatio) //Letterbox
            {
                largestRectangle.Width = maxWidth;
                largestRectangle.Height = (int)CalculateHeightFromWidth(largestRectangle.Width);
            }
            else //Pillarbox
            {
                largestRectangle.Height = maxHeight;
                largestRectangle.Width = (int)CalculateWidthFromHeight(largestRectangle.Height);
            }

            return largestRectangle;
        }
    }
}
