using FlatEarth.ViewSystem;
using FlatEarth.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FlatEarth.Testing.ViewSystem
{
    public class FixedAspectViewTests
    {
        private FixedAspectView CenteredSquareView;
        private FixedAspectView CenteredTallView;
        private FixedAspectView CenteredWideView;

        public FixedAspectViewTests()
        {
            CenteredSquareView = new FixedAspectView(Anchor.Center, new AspectRatio(1, 1));
        }
        
        [Fact]
        public void IsPillarbox()
        {
            var bounds = CenteredSquareView.GetBounds(1600, 1024);
            Assert.True(bounds.Width <= 1600 && bounds.Height == 1024);
        }

        public void IsLetterbox()
        {
            var view = new FixedAspectView(Anchor.Center, new AspectRatio(1, 1));
            var windowWidth = 1024;
            var windowHeight = 1600;
            var bounds = view.GetBounds(windowWidth, windowHeight);

            Assert.Equal(0, bounds.X);
            Assert.Equal(windowWidth, bounds.Width);
            Assert.Equal(windowWidth, bounds.Height);
            Assert.Equal((windowHeight - windowWidth) / 2, bounds.Y);
        }
    }
}
