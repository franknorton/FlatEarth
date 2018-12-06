using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth
{
    public static class Defaults
    {
        public static Texture2D PixelTexture { get; private set; }

        public static FETexture FEPixelTexture { get; private set; }
        public static SpriteFont Font { get; private set; }

        static Defaults()
        {
            
        }

        public static void Initialize()
        {
            PixelTexture = new Texture2D(Engine.Graphics.GraphicsDevice, 1, 1);
            PixelTexture.SetData<Color>(new Color[] { Color.White });
            FEPixelTexture = new FETexture(PixelTexture);
            Font = Engine.Instance.DefaultResourceContent.Load<SpriteFont>("defaultFont");
        }
    }
}
