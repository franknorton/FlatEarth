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

    public class CollisionCheckingContext
    {
        #region Rectangle / Rectangle
        public static TheoryData<Rectangle, Rectangle> RectanglesSeperate =>
            new TheoryData<Rectangle, Rectangle>
            {
                {
                    new Rectangle { X = 10, Y = 10, Width = 20, Height = 10 },
                    new Rectangle{ X = 40, Y = 10, Width = 20, Height = 10 }
                }
            };

        public static TheoryData<Rectangle, Rectangle> RectanglesCollide =>
            new TheoryData<Rectangle, Rectangle>
            {
                {
                    new Rectangle { X = 10, Y = 10, Width = 20, Height = 10 },
                    new Rectangle { X = 25, Y = 15, Width = 20, Height = 10 }
                }
            };
        #endregion

        #region Rectangle / Circle

        public static TheoryData<Rectangle, Circle> RectangleToCircleSeperate =>
            new TheoryData<Rectangle, Circle>
            {
                {
                    new Rectangle { X = 10, Y = 10, Width = 20, Height = 10 },
                    new Circle { X = 15, Y = 40, Radius = 10 }
                }
            };

        public static TheoryData<Rectangle, Circle> RectangleToCircleCollide =>
            new TheoryData<Rectangle, Circle>
            {
                {
                    new Rectangle { X = 10, Y = 10, Width = 20, Height = 10 },
                    new Circle { X = 15, Y = 5, Radius = 10 }
                }
            };

        #endregion

        #region Rectangle / Line | Line / Rectangle
        public static TheoryData<Rectangle, Line> RectangleToLineSeperate =>
            new TheoryData<Rectangle, Line>
            {
                {
                    new Rectangle { X = 10, Y = 10, Width = 20, Height = 10 },
                    new Line { BeginX = 0, BeginY = 0, EndX = 0, EndY = 40 }
                }
            };

        public static TheoryData<Rectangle, Line> RectangleToLineCollide =>
            new TheoryData<Rectangle, Line>
            {
                {
                    new Rectangle { X = 10, Y = 10, Width = 20, Height = 10 },
                    new Line { BeginX = 20, BeginY = 0, EndX = 20, EndY = 40}
                }
            };
        #endregion

        #region Rectangle / Point | Point / Rectangle
        public static TheoryData<Rectangle, Vector2> RectangleToPointSeperate =>
            new TheoryData<Rectangle, Vector2>
            {
                {
                    new Rectangle { X = 10, Y = 10, Width = 20, Height = 10 },
                    new Vector2 { X = 5, Y = 15 }
                }
            };

        public static TheoryData<Rectangle, Vector2> RectangleToPointCollide =>
            new TheoryData<Rectangle, Vector2>
            {
                {
                    new Rectangle { X = 10, Y = 10, Width = 20, Height = 10 },
                    new Vector2 { X = 20, Y = 15 }
                }
            };
        #endregion

        #region Circle / Circle
        public static TheoryData<Circle, Circle> CirclesSeperate =>
            new TheoryData<Circle, Circle>
            {
                {
                    new Circle { X = 10, Y = 10, Radius = 10 },
                    new Circle { X = 40, Y = 40, Radius = 10 }
                }
            };

        public static TheoryData<Circle, Circle> CirclesCollide =>
            new TheoryData<Circle, Circle>
            {
                {
                    new Circle { X = 10, Y = 10, Radius = 10 },
                    new Circle { X = 25, Y = 10, Radius = 10 }
                }
            };
        #endregion

        #region Circle / Line | Line / Circle
        public static TheoryData<Circle, Line> CircleToLineSeperate =>
            new TheoryData<Circle, Line>
            {
                {
                    new Circle { X = 10, Y = 10, Radius = 10 },
                    new Line { BeginX = 0, BeginY = 50, EndX = 50, EndY = 50 }
                }
            };

        public static TheoryData<Circle, Line> CircleToLineCollide =>
            new TheoryData<Circle, Line>
            {
                {
                    new Circle { X = 10, Y = 10, Radius = 10 },
                    new Line { BeginX = -10, BeginY = 10, EndX = 40, EndY = 10}
                },
                {
                    new Circle { X = 10, Y = 10, Radius = 10 },
                    new Line { BeginX = 10, BeginY = -10, EndX = 10, EndY = 40}
                },
                {
                    new Circle { X = 20, Y = 20, Radius = 10 },
                    new Line { BeginX = 15, BeginY = 0, EndX = 15, EndY = 40}
                }
            };
        #endregion

        #region Circle / Point | Point / Circle
        public static TheoryData<Circle, Vector2> CircleToPointSeperate =>
            new TheoryData<Circle, Vector2>
            {
                {
                    new Circle { X = 20, Y = 20, Radius = 10 },
                    new Vector2 { X = 5, Y = 5}
                }
            };

        public static TheoryData<Circle, Vector2> CircleToPointCollide =>
            new TheoryData<Circle, Vector2>
            {
                {
                    new Circle { X = 20, Y = 20, Radius = 10 },
                    new Vector2 { X = 15, Y = 15 }
                }
            };
        #endregion

        #region Line / Line
        public static TheoryData<Line, Line> LinesSeperate =>
            new TheoryData<Line, Line>
            {
                {
                    new Line(0, 0, 40, 0),
                    new Line(10, 10, 30, 30)
                }
            };

        public static TheoryData<Line, Line> LinesCollide =>
            new TheoryData<Line, Line>
            {
                {
                    new Line(0, 0, 40, 40),
                    new Line(0, 40, 40, 0)
                }
            };
        #endregion

        #region Line / Point | Point / Line
        public static TheoryData<Line, Vector2> LineToPointSeperate =>
            new TheoryData<Line, Vector2>
            {
                {
                    new Line { BeginX = 10, BeginY = 10, EndX = 10, EndY = 40 },
                    new Vector2 { X = 5, Y = 5 }
                }
            };

        public static TheoryData<Line, Vector2> LineToPointCollide =>
            new TheoryData<Line, Vector2>
            {
                {
                    new Line { BeginX = 10, BeginY = 0, EndX = 10, EndY = 40 },
                    new Vector2 { X = 10, Y = 25 }
                }
            };
        #endregion

        #region Point / Point
        public static TheoryData<Vector2, Vector2> PointsSeperate =>
            new TheoryData<Vector2, Vector2>
            {
                {
                    new Vector2 { X = 5, Y = 5 },
                    new Vector2 { X = 10, Y = 10 }
                }
            };

        public static TheoryData<Vector2, Vector2> PointsCollide =>
            new TheoryData<Vector2, Vector2>
            {
                {
                    new Vector2 { X = 10, Y = 10 },
                    new Vector2 { X = 10, Y = 10 }
                }
            };
        #endregion

    }
}
