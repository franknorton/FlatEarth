using FlatEarth.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Extensions
{
    public static class AnchorExtensions
    {
        public static int GetXPosition(this Anchor anchor, int width, int maxWidth)
        {
            switch (anchor)
            {
                case Anchor.TopLeft:
                case Anchor.CenterLeft:
                case Anchor.BottomLeft:
                    return 0;
                case Anchor.TopCenter:
                case Anchor.Center:
                case Anchor.BottomCenter:
                    return maxWidth / 2 - width / 2;
                case Anchor.TopRight:
                case Anchor.CenterRight:
                case Anchor.BottomRight:
                    return maxWidth - width;
                default:
                    throw new NotImplementedException();
            }
        }

        public static int GetYPosition(this Anchor anchor, int height, int maxHeight)
        {
            switch (anchor)
            {
                case Anchor.TopLeft:
                case Anchor.TopCenter:
                case Anchor.TopRight:
                    return 0;
                case Anchor.CenterLeft:
                case Anchor.Center:
                case Anchor.CenterRight:
                    return maxHeight / 2 - height / 2;
                case Anchor.BottomLeft:
                case Anchor.BottomCenter:
                case Anchor.BottomRight:
                    return maxHeight - height;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
