using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision
{
    public static class Collide
    {

        #region Point
        //TODO
        #endregion

        #region Line
        //Point
        //Line
        public static bool LineToLine(float line1StartX, float line1StartY, float line1EndX, float line1EndY, float line2StartX, float line2StartY, float line2EndX, float line2EndY)
        {
            var a = line1EndY - line1StartY;
            var b = line1EndX - line1StartX;
            var a2 = line2EndY - line2StartY;
            var b2 = line2EndX - line2StartX;
            var d = (a * b2 - a2 * b);

            if (d == 0)
                return false;

            var cx = line2StartX - line1StartX;
            var cy = line2StartY - line1StartY;
            float t = (cx * a2 - cy * b2) / d;
            if (t < 0 || t > 1)
                return false;

            float u = (cx * a - cy * b) / d;
            if (u < 0 || u > 1)
                return false;

            return true;
        }
        
        //Circle
        public static bool LineToCircle(float lineStartX, float lineStartY, float lineEndX, float lineEndY, CircleCollider circle) { return CircleToLine(circle, new Vector2(lineStartX, lineStartY), new Vector2(lineEndX, lineEndY)); }

        //Rectangle
        public static bool LineToRectangle(float lineStartX, float lineStartY, float lineEndX, float lineEndY, RectCollider rect) { return RectToLine(rect, lineStartX, lineStartY, lineEndX, lineEndY); }
        #endregion

        #region Circle
        //Point
        public static bool CircleToPoint(float circleX, float circleY, float circleRadius, float pointX, float pointY)
        {
            var dx = circleX - pointX;
            var dy = circleY - pointY;
            return (dx * dx + dy * dy) < (circleRadius * circleRadius);
        }
        public static bool CircleToPoint(CircleCollider circle, Vector2 point) { return CircleToPoint(circle.Position.X, circle.Position.Y, circle.Radius, point.X, point.Y); }

        //Line
        public static bool CircleToLine(CircleCollider circle, Vector2 lineStart, Vector2 lineEnd)
        {
            var direction = lineEnd - lineStart;
            direction.Normalize();
            var distance = Vector2.Dot(direction, circle.Position);
            if (distance >= circle.Radius)
                return true;
            return false;
        }

        //Circle
        public static bool CircleToCircle(CircleCollider circle1, CircleCollider circle2)
        {
            var dx = circle2.Position.X - circle1.Position.X;
            var dy = circle2.Position.Y - circle2.Position.Y;
            var radii = circle1.Radius + circle2.Radius;
            return (dx * dx) + (dy * dy) < (radii * radii);
        }

        //Rectangle
        public static bool CircleToRectangle(CircleCollider circle, RectCollider rect) { return RectToCircle(rect, circle); }
        #endregion

        #region Rectangle
        //Point
        public static bool RectToPoint(float rectX, float rectY, float rectWidth, float rectHeight, Point point)
        {
            return (rectX <= point.X
                && rectX + rectWidth >= point.X
                && rectY <= point.Y
                && rectY + rectHeight >= point.Y);
        }
        public static bool RectToPoint(Rectangle rect, Point point) { return RectToPoint(rect.X, rect.Y, rect.Width, rect.Height, point); }
        public static bool RectToPoint(RectCollider rect, Point point) { return RectToPoint(rect.Position.X, rect.Position.Y, rect.Width, rect.Height, point); }

        //Line
        public static bool RectToLine(float rectX, float rectY, float rectWidth, float rectHeight, float lineStartX, float lineStartY, float lineEndX, float lineEndY) { return CohenSutherland.Intersects(rectX, rectY, rectWidth, rectHeight, lineStartX, lineStartY, lineEndX, lineEndY); }
        public static bool RectToLine(Rectangle rect, float lineStartX, float lineStartY, float lineEndX, float lineEndY) { return RectToLine(rect.X, rect.Y, rect.Width, rect.Height, lineStartX, lineStartY, lineEndX, lineEndY); }
        public static bool RectToLine(RectCollider rect, float lineStartX, float lineStartY, float lineEndX, float lineEndY) { return RectToLine(rect.Position.X, rect.Position.Y, rect.Width, rect.Height, lineStartX, lineStartY, lineEndX, lineEndY); }
        public static bool RectToLine(Rectangle rect, Vector2 lineStart, Vector2 lineEnd) { return CohenSutherland.Intersects(rect, lineStart, lineEnd); }
        public static bool RectToLine(RectCollider rect, Vector2 lineStart, Vector2 lineEnd) { return CohenSutherland.Intersects(rect, lineStart, lineEnd); }

        //Circle
        public static bool RectToCircle(float rectX, float rectY, float rectWidth, float rectHeight, float circleX, float circleY, float circleRadius)
        {
            var dx = circleX - Math.Max(rectX, Math.Min(circleX, rectX + rectWidth));
            var dy = circleY - Math.Max(rectY, Math.Min(circleY, rectY + rectHeight));
            return (dx * dx + dy * dy) < (circleRadius * circleRadius);
        }
        public static bool RectToCircle(Rectangle rect, float circleX, float circleY, float circleRadius) { return RectToCircle(rect.X, rect.Y, rect.Width, rect.Height, circleX, circleY, circleRadius); }
        public static bool RectToCircle(RectCollider rect, float circleX, float circleY, float circleRadius) { return RectToCircle(rect.Position.X, rect.Position.Y, rect.Width, rect.Height, circleX, circleY, circleRadius); }
        public static bool RectToCircle(Rectangle rect, CircleCollider circle) { return RectToCircle(rect.X, rect.Y, rect.Width, rect.Height, circle.Position.X, circle.Position.Y, circle.Radius); }
        public static bool RectToCircle(RectCollider rect, CircleCollider circle) { return RectToCircle(rect.AbsolutePosition.X, rect.AbsolutePosition.Y, rect.Width, rect.Height, circle.AbsolutePosition.X, circle.AbsolutePosition.Y, circle.Radius); }

        //Rectangle
        public static bool RectToRect(float rect1X, float rect1Y, float rect1Width, float rect1Height, float rect2X, float rect2Y, float rect2Width, float rect2Height)
        {
            return (rect1X < rect2X + rect2Width
                && rect1X + rect1Width > rect2X
                && rect1Y < rect2Y + rect2Width
                && rect1Y + rect1Height > rect2Y);
        }
        public static bool RectToRect(Rectangle rect1, Rectangle rect2) { return RectToRect(rect1.X, rect1.Y, rect1.Width, rect1.Height, rect2.X, rect2.Y, rect2.Width, rect2.Height); }
        public static bool RectToRect(RectCollider rect1, RectCollider rect2) { return RectToRect(rect1.AbsolutePosition.X, rect1.AbsolutePosition.Y, rect1.Width, rect1.Height, rect2.AbsolutePosition.X, rect2.AbsolutePosition.Y, rect2.Width, rect2.Height); }
        #endregion
    }
}
