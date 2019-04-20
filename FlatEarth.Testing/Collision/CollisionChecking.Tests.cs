using FlatEarth.Collision;
using FlatEarth.DataStructures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FlatEarth.Testing.Collision
{
    public class CollisionCheckingTests
    {
        private CollisionCheckingContext context;
        public CollisionCheckingTests()
        {
            context = new CollisionCheckingContext();
        }

        #region Rectangle / Rectangle
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.RectanglesSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void Rectangles_Seperate(Rectangle rectangle1, Rectangle rectangle2)
        {
            Assert.False(
                CollisionChecking.Rectangles(
                    rectangle1.X,
                    rectangle1.Y,
                    rectangle1.Width,
                    rectangle1.Height,
                    rectangle2.X,
                    rectangle2.Y,
                    rectangle2.Width,
                    rectangle2.Height
                )
            );
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.RectanglesCollide), MemberType = typeof(CollisionCheckingContext))]
        public void Rectangles_Collide(Rectangle rectangle1, Rectangle rectangle2)
        {
            Assert.True(
                CollisionChecking.Rectangles(
                    rectangle1.X,
                    rectangle1.Y,
                    rectangle1.Width,
                    rectangle1.Height,
                    rectangle2.X,
                    rectangle2.Y,
                    rectangle2.Width,
                    rectangle2.Height
                )
            );
        }
        #endregion

        #region Rectangle / Circle | Circle / Rectangle
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.RectangleToCircleSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void RectangleToCircle_Seperate(Rectangle rectangle, Circle circle)
        {
            Assert.False(CollisionChecking.RectangleToCircle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, circle.X, circle.Y, circle.Radius));
            Assert.False(CollisionChecking.CircleToRectangle(circle.X, circle.Y, circle.Radius, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.RectangleToCircleCollide), MemberType = typeof(CollisionCheckingContext))]
        public void RectangleToCircle_Collide(Rectangle rectangle, Circle circle)
        {
            Assert.True(CollisionChecking.RectangleToCircle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, circle.X, circle.Y, circle.Radius));
            Assert.True(CollisionChecking.CircleToRectangle(circle.X, circle.Y, circle.Radius, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
        }
        #endregion

        #region Rectangle / Line | Line / Rectangle
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.RectangleToLineSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void RectangleToLine_Seperate(Rectangle rectangle, Line line)
        {
            Assert.False(CollisionChecking.RectangleToLine(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, line.BeginX, line.BeginY, line.EndX, line.EndY));
            Assert.False(CollisionChecking.LineToRectangle(line.BeginX, line.BeginY, line.EndX, line.EndY, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.RectangleToLineCollide), MemberType = typeof(CollisionCheckingContext))]
        public void RectangleToLine_Collide(Rectangle rectangle, Line line)
        {
            Assert.True(CollisionChecking.RectangleToLine(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, line.BeginX, line.BeginY, line.EndX, line.EndY));
            Assert.True(CollisionChecking.LineToRectangle(line.BeginX, line.BeginY, line.EndX, line.EndY, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
        }
        #endregion

        #region Rectangle / Point | Point / Rectangle
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.RectangleToPointSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void RectangleToPoint_Seperate(Rectangle rectangle, Vector2 point)
        {
            Assert.False(CollisionChecking.RectangleToPoint(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, point.X, point.Y));
            Assert.False(CollisionChecking.PointToRectangle(point.X, point.Y, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.RectangleToPointCollide), MemberType = typeof(CollisionCheckingContext))]
        public void RectangleToPoint_Collide(Rectangle rectangle, Vector2 point)
        {
            Assert.True(CollisionChecking.RectangleToPoint(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, point.X, point.Y));
            Assert.True(CollisionChecking.PointToRectangle(point.X, point.Y, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
        }
        #endregion

        #region Circle / Circle
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.CirclesSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void Circles_Seperate(Circle circle1, Circle circle2)
        {
            Assert.False(
                CollisionChecking.Circles(
                    circle1.X,
                    circle1.Y,
                    circle1.Radius,
                    circle2.X,
                    circle2.Y,
                    circle2.Radius
                )
            );
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.CirclesCollide), MemberType = typeof(CollisionCheckingContext))]
        public void Circles_Collide(Circle circle1, Circle circle2)
        {
            Assert.True(
                CollisionChecking.Circles(
                    circle1.X,
                    circle1.Y,
                    circle1.Radius,
                    circle2.X,
                    circle2.Y,
                    circle2.Radius
                )
            );
        }
        #endregion

        #region Circle / Line | Line / Circle
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.CircleToLineSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void CircleToLine_Seperate(Circle circle, Line line)
        {
            Assert.False(CollisionChecking.CircleToLine(circle.X, circle.Y, circle.Radius, line.BeginX, line.BeginY, line.EndX, line.EndY));
            Assert.False(CollisionChecking.LineToCircle(line.BeginX, line.BeginY, line.EndX, line.EndY, circle.X, circle.Y, circle.Radius));
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.CircleToLineCollide), MemberType = typeof(CollisionCheckingContext))]
        public void CircleToLine_Collide(Circle circle, Line line)
        {
            Assert.True(CollisionChecking.CircleToLine(circle.X, circle.Y, circle.Radius, line.BeginX, line.BeginY, line.EndX, line.EndY));
            Assert.True(CollisionChecking.LineToCircle(line.BeginX, line.BeginY, line.EndX, line.EndY, circle.X, circle.Y, circle.Radius));
        }
        #endregion

        #region Circle / Point | Point / Circle
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.CircleToPointSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void CircleToPoint_Seperate(Circle circle, Vector2 point)
        {
            Assert.False(CollisionChecking.CircleToPoint(circle.X, circle.Y, circle.Radius, point.X, point.Y));
            Assert.False(CollisionChecking.PointToCircle(point.X, point.Y, circle.X, circle.Y, circle.Radius));
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.CircleToPointCollide), MemberType = typeof(CollisionCheckingContext))]
        public void CircleToPoint_Collide(Circle circle, Vector2 point)
        {
            Assert.True(CollisionChecking.CircleToPoint(circle.X, circle.Y, circle.Radius, point.X, point.Y));
            Assert.True(CollisionChecking.PointToCircle(point.X, point.Y, circle.X, circle.Y, circle.Radius));
        }
        #endregion

        #region Line / Line
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.LinesSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void Lines_Seperate(Line line1, Line line2)
        {
            Assert.False(CollisionChecking.Lines(line1.BeginX, line1.BeginY, line1.EndX, line1.EndY, line2.BeginX, line2.BeginY, line2.EndX, line2.EndY));
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.LinesCollide), MemberType = typeof(CollisionCheckingContext))]
        public void Lines_Collide(Line line1, Line line2)
        {
            Assert.True(CollisionChecking.Lines(line1.BeginX, line1.BeginY, line1.EndX, line1.EndY, line2.BeginX, line2.BeginY, line2.EndX, line2.EndY));
        }
        #endregion

        #region Line / Point | Point / Line
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.LineToPointSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void LineToPoint_Seperate(Line line, Vector2 point)
        {
            Assert.False(CollisionChecking.LineToPoint(line.BeginX, line.BeginY, line.EndX, line.EndY, point.X, point.Y));
            Assert.False(CollisionChecking.PointToLine(point.X, point.Y, line.BeginX, line.BeginY, line.EndX, line.EndY));
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.LineToPointCollide), MemberType = typeof(CollisionCheckingContext))]
        public void LineToPoint_Collide(Line line, Vector2 point)
        {
            Assert.True(CollisionChecking.LineToPoint(line.BeginX, line.BeginY, line.EndX, line.EndY, point.X, point.Y));
            Assert.True(CollisionChecking.PointToLine(point.X, point.Y, line.BeginX, line.BeginY, line.EndX, line.EndY));
        }
        #endregion

        #region Point / Point
        [Theory]
        [MemberData(nameof(CollisionCheckingContext.PointsSeperate), MemberType = typeof(CollisionCheckingContext))]
        public void PointsSeperate(Vector2 point1, Vector2 point2)
        {
            Assert.False(CollisionChecking.Points(point1.X, point1.Y, point2.X, point2.Y));
        }

        [Theory]
        [MemberData(nameof(CollisionCheckingContext.PointsCollide), MemberType = typeof(CollisionCheckingContext))]
        public void PointsCollide(Vector2 point1, Vector2 point2)
        {
            Assert.True(CollisionChecking.Points(point1.X, point1.Y, point2.X, point2.Y));
        }
        #endregion

    }
}
