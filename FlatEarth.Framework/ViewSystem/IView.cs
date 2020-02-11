using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.ViewSystem
{
    public interface IView
    {
        Rectangle GetBounds(int windowWidth, int windowHeight);
    }
}
