using FlatEarth.Rendering;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth
{
    public static class Extensions
    {

        public static FETexture ToFETexture(this Texture2D texture)
        {
            return new FETexture(texture);
        }
    }
}
