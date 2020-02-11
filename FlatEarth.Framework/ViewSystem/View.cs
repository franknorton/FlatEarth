using FlatEarth.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.ViewSystem
{
    public abstract class View : IView
    {
        public Anchor Anchor { get; set; }

        public View(Anchor anchor)
        {
            Anchor = anchor;
        }

        public abstract Rectangle GetBounds(int windowWidth, int windowHeight);
    }
}
