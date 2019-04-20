using FlatEarth.DataStructures;
using FlatEarth.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision
{
    public static partial class CollisionChecking
    {

        #region Rectangles
        public static bool Rectangles(float rectangle1X, float rectangle1Y, float rectangle1Width, float rectangle1Height, float rectangle2X, float rectangle2Y, float rectangle2Width, float rectangle2Height)
        {
            return (
                rectangle1X < rectangle2X + rectangle2Width &&
                rectangle1X + rectangle1Width > rectangle2X &&
                rectangle1Y < rectangle2Y + rectangle2Height &&
                rectangle1Y + rectangle1Height > rectangle2Y
            );
        }
        public static bool RectangleToCircle(float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight, float circleX, float circleY, float circleRadius)
        {
            var dx = circleX - Math.Max(rectangleX, Math.Min(circleX, rectangleX + rectangleWidth));
            var dy = circleY - Math.Max(rectangleY, Math.Min(circleY, rectangleY + rectangleHeight));
            return (dx * dx + dy * dy) < (circleRadius * circleRadius);
        }
        public static bool RectangleToLine(float rectangleX, float rectangleY, float rectangleWidth, float RectangleHeight, float lineBeginX, float lineBeginY, float lineEndX, float lineEndY)
        {
            return CohenSutherland.Intersects(rectangleX, rectangleY, rectangleWidth, RectangleHeight, lineBeginX, lineBeginY, lineEndX, lineEndY);
        }

        /// <summary>
        /// Determines whether a rectangle and point are intersecting. 
        /// </summary>
        /// <param name="rectangleX">The X position of the rectangle.</param>
        /// <param name="rectangleY">The Y position of the rectangle.</param>
        /// <param name="rectangleWidth">The width of the rectangle.</param>
        /// <param name="rectangleHeight">The height of the rectangle.</param>
        /// <param name="pointX">The X position of the point.</param>
        /// <param name="pointY">The Y position of the point.</param>
        /// <returns>True or False if the shapes are intersecting or not.</returns>
        public static bool RectangleToPoint(float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight, float pointX, float pointY)
        {
            return (
                rectangleX < pointX &&
                rectangleX + rectangleWidth > pointX &&
                rectangleY < pointY &&
                rectangleY + rectangleHeight > pointY
            );
        }
        #endregion

        #region Circles
        public static bool Circles(float circle1X, float circle1Y, float circle1Radius, float circle2X, float circle2Y, float circle2Radius)
        {
            var dx = circle2X - circle1X;
            var dy = circle2Y - circle1Y;
            var radii = circle1Radius + circle2Radius;
            return (dx * dx) + (dy * dy) <= (radii * radii);
        }

        public static bool CircleToRectangle(float circleX, float circleY, float circleRadius, float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight)
        {
            return RectangleToCircle(rectangleX, rectangleY, rectangleWidth, rectangleHeight, circleX, circleY, circleRadius);
        }

        public static bool CircleToLine(float circleX, float circleY, float circleRadius, float lineBeginX, float lineBeginY, float lineEndX, float lineEndY)
        {
            var dx = lineEndX - lineBeginX;
            var dy = lineEndY - lineBeginY;
            var lengthSquared = dx * dx + dy * dy;
            var u = ((circleX - lineBeginX) * dx + (circleY - lineBeginY) * dy) / lengthSquared;

            if (u > 1) u = 1;
            else if (u < 0) u = 0;

            float closestPointX = lineBeginX + u * dx;
            float closestPointY = lineBeginY + u * dy;

            float distanceX = closestPointX - circleX;
            float distanceY = closestPointY - circleY;

            float distanceSquared = distanceX * distanceX + distanceY * distanceY;

            if ((circleRadius * circleRadius) >= distanceSquared)
                return true;

            return false;
        }

        public static bool CircleToPoint(float circleX, float circleY, float circleRadius, float pointX, float pointY)
        {
            var dx = circleX - pointX;
            var dy = circleY - pointY;

            return (dx * dx + dy * dy) <= circleRadius * circleRadius;
        }
        #endregion

        #region Lines

        public static bool LineToRectangle(float lineBeginX, float lineBeginY, float lineEndX, float lineEndY, float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight)
        {
            return RectangleToLine(rectangleX, rectangleY, rectangleWidth, rectangleHeight, lineBeginX, lineBeginY, lineEndX, lineEndY);
        }

        public static bool LineToCircle(float lineBeginX, float lineBeginY, float lineEndX, float lineEndY, float circleX, float circleY, float circleRadius)
        {
            return CircleToLine(circleX, circleY, circleRadius, lineBeginX, lineBeginY, lineEndX, lineEndY);
        }

        public static bool Lines(float line1BeginX, float line1BeginY, float line1EndX, float line1EndY, float line2BeginX, float line2BeginY, float line2EndX, float line2EndY)
        {
            var aX = line1EndX - line1BeginX;
            var aY = line1EndY - line1BeginY;
            var bX = line2EndX - line2BeginX;
            var bY = line2EndY - line2BeginY;

            float idk = aX * bY - aY * bX;

            if (idk == 0)
                return false;

            var cX = line2BeginX - line1BeginX;
            var cY = line2BeginY - line1BeginY;

            float t = (cX * bY - cY * bX) / idk;
            if (t < 0 || t > 1)
                return false;

            float u = (cX * aY - cY * aX) / idk;
            if (u < 0 || u > 1)
                return false;

            return true;
        }

        public static bool LineToPoint(float lineBeginX, float lineBeginY, float lineEndX, float lineEndY, float pointX, float pointY)
        {
            var crossProduct = (pointY - lineBeginY) * (lineEndX - lineBeginX) - (pointX - lineBeginX) * (lineEndY - lineBeginY);

            if (Math.Abs(crossProduct) > 0.001)
                return false;

            var dotProduct = (pointX - lineBeginX) * (lineEndX - lineBeginX) + (pointY - lineBeginY) * (lineEndY - lineBeginY);
            if (dotProduct < 0)
                return false;

            var squaredLength = (lineEndX - lineBeginX) * (lineEndX - lineBeginX) + (lineEndY - lineBeginY) * (lineEndY - lineBeginY);
            if (dotProduct > squaredLength)
                return false;

            return true;
        }
        #endregion

        #region Points

        public static bool PointToRectangle(float pointX, float pointY, float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight)
        {
            return RectangleToPoint(rectangleX, rectangleY, rectangleWidth, rectangleHeight, pointX, pointY);
        }

        public static bool PointToCircle(float pointX, float pointY, float circleX, float circleY, float circleRadius)
        {
            return CircleToPoint(circleX, circleY, circleRadius, pointX, pointY);
        }

        public static bool PointToLine(float pointX, float pointY, float lineBeginX, float lineBeginY, float lineEndX, float lineEndY)
        {
            return LineToPoint(lineBeginX, lineBeginY, lineEndX, lineEndY, pointX, pointY);
        }

        public static bool Points(float point1X, float point1Y, float point2X, float point2Y)
        {
            return point1X == point2X && point1Y == point2Y;
        }
        #endregion

    }

    //Monogame version
    public static partial class CollisionChecking
    {
        #region Rectangles
        public static bool Rectangles(Rectangle rectangle1, Rectangle rectangle2)
        {
            return Rectangles(rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height);
        }

        public static bool RectangleToCircle(Rectangle rectangle, float circleX, float circleY, float circleRadius)
        {
            return RectangleToCircle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, circleX, circleY, circleRadius);
        }

        public static bool RectangleToLine(Rectangle rectangle, float lineBeginX, float lineBeginY, float lineEndX, float lineEndY)
        {
            return RectangleToLine(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, lineBeginX, lineBeginY, lineEndX, lineEndY);
        }

        public static bool RectangleToPoint(Rectangle rectangle, Vector2 point)
        {
            return rectangle.Contains(point);
        }

        public static bool RectangleToPoint(Rectangle rectangle, Point point)
        {
            return rectangle.Contains(point);
        }
        #endregion

        #region Circles
        public static bool CircleToRectangle(float circleX, float circleY, float circleRadius, Rectangle rectangle)
        {
            return CircleToRectangle(circleX, circleY, circleRadius, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static bool CircleToPoint(float circleX, float circleY, float circleRadius, Vector2 point)
        {
            return CircleToPoint(circleX, circleY, circleRadius, point.X, point.Y);
        }

        public static bool CircleToPoint(float circleX, float circleY, float circleRadius, Point point)
        {
            return CircleToPoint(circleX, circleY, circleRadius, point.X, point.Y);
        }
        #endregion

        #region Lines
        public static bool LineToRectangle(float lineBeginX, float lineBeginY, float lineEndX, float lineEndY, Rectangle rectangle)
        {
            return LineToRectangle(lineBeginX, lineBeginY, lineEndX, lineEndY, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static bool LineToPoint(float lineBeginX, float lineBeginY, float lineEndX, float lineEndY, Vector2 point)
        {
            return LineToPoint(lineBeginX, lineBeginY, lineEndX, lineEndY, point.X, point.Y);
        }

        public static bool LineToPoint(float lineBeginX, float lineBeginY, float lineEndX, float lineEndY, Point point)
        {
            return LineToPoint(lineBeginX, lineBeginY, lineEndX, lineEndY, point.X, point.Y);
        }
        #endregion

        #region Points
        public static bool PointToRectangle(Vector2 point, Rectangle rectangle)
        {
            return PointToRectangle(point.X, point.Y, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static bool PointToRectangle(Point point, Rectangle rectangle)
        {
            return PointToRectangle(point.X, point.Y, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static bool Points(Vector2 point1, Vector2 point2)
        {
            return Points(point1.X, point1.Y, point2.X, point2.Y);
        }

        public static bool Points(Vector2 point1, Point point2)
        {
            return Points(point1.X, point1.Y, point2.X, point2.Y);
        }

        public static bool Points(Point point1, Vector2 point2)
        {
            return Points(point1.X, point1.Y, point2.X, point2.Y);
        }

        public static bool Points(Point point1, Point point2)
        {
            return Points(point1.X, point1.Y, point2.X, point2.Y);
        }
        #endregion
    }

    //Custom version
    public static partial class CollisionChecking
    {
        #region Rectangles
        public static bool RectangleToCircle(Rectangle rectangle, Circle circle)
        {
            return RectangleToCircle(rectangle, circle.X, circle.Y, circle.Radius);
        }

        public static bool RectangleToCircle(float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight, Circle circle)
        {
            return RectangleToCircle(rectangleX, rectangleY, rectangleWidth, rectangleHeight, circle.X, circle.Y, circle.Radius);
        }

        public static bool RectangleToLine(Rectangle rectangle, Line line)
        {
            return RectangleToLine(rectangle, line.BeginX, line.BeginY, line.EndX, line.EndY);   
        }

        public static bool RectangleToLine(float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight, Line line)
        {
            return RectangleToLine(rectangleX, rectangleY, rectangleWidth, rectangleHeight, line.BeginX, line.BeginY, line.EndX, line.EndY);
        }
        #endregion

        #region Circles
        public static bool CircleToRectangle(Circle circle, Rectangle rectangle)
        {
            return CircleToRectangle(circle.X, circle.Y, circle.Radius, rectangle);
        }

        public static bool CircleToRectangle(Circle circle, float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight)
        {
            return CircleToRectangle(circle.X, circle.Y, circle.Radius, rectangleX, rectangleY, rectangleWidth, rectangleHeight);
        }

        public static bool Circles(Circle circle1, Circle circle2)
        {
            return Circles(circle1.X, circle1.Y, circle1.Radius, circle2.X, circle2.Y, circle2.Radius);
        }

        public static bool Circles(Circle circle1, float circle2X, float circle2Y, float circle2Radius)
        {
            return Circles(circle1.X, circle1.Y, circle1.Radius, circle2X, circle2Y, circle2Radius);
        }

        public static bool Circles(float circle1X, float circle1Y, float circle1Radius, Circle circle2)
        {
            return Circles(circle1X, circle1Y, circle1Radius, circle2.X, circle2.Y, circle2.Radius);
        }

        public static bool CircleToLine(Circle circle, Line line)
        {
            return CircleToLine(circle.X, circle.Y, circle.Radius, line.BeginX, line.BeginY, line.EndX, line.EndY);
        }

        public static bool CircleToLine(Circle circle, float lineBeginX, float lineBeginY, float lineEndX, float lineEndY)
        {
            return CircleToLine(circle.X, circle.Y, circle.Radius, lineBeginX, lineBeginY, lineEndX, lineEndY);
        }

        public static bool CircleToLine(float circleX, float circleY, float circleRadius, Line line)
        {
            return CircleToLine(circleX, circleY, circleRadius, line.BeginX, line.BeginY, line.EndX, line.EndY);
        }

        public static bool CircleToPoint(Circle circle, Vector2 point)
        {
            return CircleToPoint(circle.X, circle.Y, circle.Radius, point);
        }

        public static bool CircleToPoint(Circle circle, Point point)
        {
            return CircleToPoint(circle.X, circle.Y, circle.Radius, point);
        }

        public static bool CircleToPoint(Circle circle, float pointX, float pointY)
        {
            return CircleToPoint(circle.X, circle.Y, circle.Radius, pointX, pointY);
        }
        #endregion

        #region Lines

        public static bool LineToRectangle(Line line, Rectangle rectangle)
        {
            return LineToRectangle(line.BeginX, line.BeginY, line.EndX, line.EndY, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static bool LineToRectangle(Line line, float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight)
        {
            return LineToRectangle(line.BeginX, line.BeginY, line.EndX, line.EndY, rectangleX, rectangleY, rectangleWidth, rectangleHeight);
        }

        public static bool LineToCircle(Line line, Circle circle)
        {
            return LineToCircle(line.BeginX, line.BeginY, line.EndX, line.EndY, circle.X, circle.Y, circle.Radius);
        }

        public static bool LineToCircle(Line line, float circleX, float circleY, float circleRadius)
        {
            return LineToCircle(line.BeginX, line.BeginY, line.EndX, line.EndY, circleX, circleY, circleRadius);
        }

        public static bool LineToCircle(float lineBeginX, float lineBeginY, float lineEndX, float lineEndY, Circle circle)
        {
            return LineToCircle(lineBeginX, lineBeginY, lineEndX, lineEndY, circle.X, circle.Y, circle.Radius);
        }

        public static bool Lines(Line line1, Line line2)
        {
            return Lines(line1.BeginX, line1.BeginY, line1.EndX, line1.EndY, line2.BeginX, line2.BeginY, line2.EndX, line2.EndY);
        }

        public static bool Lines(Line line1, float line2BeginX, float line2BeginY, float line2EndX, float line2EndY)
        {
            return Lines(line1.BeginX, line1.BeginY, line1.EndX, line1.EndY, line2BeginX, line2BeginY, line2EndX, line2EndY);
        }

        public static bool Lines(float line1BeginX, float line1BeginY, float line1EndX, float line1EndY, Line line2)
        {
            return Lines(line1BeginX, line1BeginY, line1EndX, line1EndY, line2.BeginX, line2.BeginY, line2.EndX, line2.EndY);
        }

        public static bool LineToPoint(Line line, Vector2 point)
        {
            return LineToPoint(line.BeginX, line.BeginY, line.EndX, line.EndY, point.X, point.Y);
        }

        public static bool LineToPoint(Line line, Point point)
        {
            return LineToPoint(line.BeginX, line.BeginY, line.EndX, line.EndY, point.X, point.Y);
        }

        public static bool LineToPoint(Line line, float pointX, float pointY)
        {
            return LineToPoint(line.BeginX, line.BeginY, line.EndX, line.EndY, pointX, pointY);
        }
        
        #endregion

        #region Points

        public static bool PointToCircle(Vector2 point, Circle circle)
        {
            return PointToCircle(point.X, point.Y, circle.X, circle.Y, circle.Radius);
        }

        public static bool PointToCircle(Point point, Circle circle)
        {
            return PointToCircle(point.X, point.Y, circle.X, circle.Y, circle.Radius);
        }

        public static bool PointToCircle(float pointX, float pointY, Circle circle)
        {
            return PointToCircle(pointX, pointY, circle.X, circle.Y, circle.Radius);
        }

        public static bool PointToLine(Vector2 point, Line line)
        {
            return PointToLine(point.X, point.Y, line.BeginX, line.BeginY, line.EndX, line.EndY);
        }

        public static bool PointToLine(Point point, Line line)
        {
            return PointToLine(point.X, point.Y, line.BeginX, line.BeginY, line.EndX, line.EndY);
        }

        public static bool PointToLine(float pointX, float pointY, Line line)
        {
            return PointToLine(pointX, pointY, line.BeginX, line.BeginY, line.EndX, line.EndY);
        }

        #endregion
    }
}
