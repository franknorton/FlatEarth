using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision
{
    public static class CollisionChecking
    {
        
        public static bool Rectangles(float rectangle1X, float rectangle1Y, float rectangle1Width, float rectangle1Height, float rectangle2X, float rectangle2Y, float rectangle2Width, float rectangle2Height)
        {
            return (
                rectangle1X < rectangle2X + rectangle2Width &&
                rectangle1X + rectangle1Width > rectangle2X &&
                rectangle1Y < rectangle2Y + rectangle2Height &&
                rectangle1Y + rectangle1Height > rectangle2Y
            );
        }
        public static bool Circles(float circle1X, float circle1Y, float circle1Radius, float circle2X, float circle2Y, float circle2Radius)
        {
            var dx = circle2X - circle1X;
            var dy = circle2Y - circle1Y;
            var radii = circle1Radius + circle2Radius;
            return (dx * dx) + (dy * dy) < (radii * radii);
        }
        public static bool CircleToRectangle(float circleX, float circleY, float circleRadius, float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight)
        {
            var dx = circleX - Math.Max(rectangleX, Math.Min(circleX, rectangleX + rectangleWidth));
            var dy = circleY - Math.Max(rectangleY, Math.Min(circleY, rectangleY + rectangleHeight));
            return (dx * dx + dy * dy) < (circleRadius * circleRadius);
        }

        public static bool Check(RectCollider rect1, RectCollider rect2)
        {
            return Rectangles(rect1.Position.X, rect1.Position.Y, rect1.Width, rect1.Height, rect2.Position.X, rect2.Position.Y, rect2.Width, rect2.Height);
        }
        public static bool Check(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Contains(rectangle2);
        }

    }
}
