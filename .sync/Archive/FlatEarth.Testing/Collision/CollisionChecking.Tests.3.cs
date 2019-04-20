using FlatEarth.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FlatEarth.Testing.Collision
{
    class CollisionCheckingTests
    {
        public Rectangle rectangleBase;
        public static Rectangle rectangleSeperate;
        public static Rectangle rectangleCollide;

        public CollisionCheckingTests()
        {
            rectangleBase = new Rectangle(10, 10, 20, 10);
            rectangleSeperate = new Rectangle(40, 10, 20, 10);
            rectangleCollide = new Rectangle(25, 15, 20, 10);
        }

        [Fact]
        public void Circles()
        {
            Assert.True(CollisionChecking.Circles(10, 10, 5, 17, 10, 5));
            Assert.False(CollisionChecking.Circles(10, 10, 5, 20, 10, 5));
        }

        [Fact]
        public void Rectangles_Seperate()
        {
            Assert.False(CollisionChecking.Rectangles(rectangleBase.X, rectangleBase.Y, rectangleBase.Width, rectangleBase.Height, rectangleSeperate.X, rectangleSeperate.Y, rectangleSeperate.Width, rectangleSeperate.Height));
        }
        [Fact]
        public void Rectangles_Collide()
        {
            Assert.True(CollisionChecking.Rectangles(rectangleBase.X, rectangleBase.Y, rectangleBase.Width, rectangleBase.Height, rectangleCollide.X, rectangleCollide.Y, rectangleCollide.Width, rectangleCollide.Height));
        }
    }
}
