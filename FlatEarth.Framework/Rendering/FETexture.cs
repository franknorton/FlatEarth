using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FlatEarth.Rendering
{
    public class FETexture
    {
        public Texture2D Source { get; set; }
        public Rectangle SourceRect { get; set; }

        public int Width { get => SourceRect.Width; }
        public int Height { get => SourceRect.Height; }
       
        
        public FETexture(Texture2D source)
        {
            this.Source = source;
        }
        public FETexture(Texture2D source, Rectangle sourceRectangle)
        {
            this.Source = source;
            this.SourceRect = sourceRectangle;
        }

        #region Drawing
        public void Draw(SpriteBatch sb, Vector2 position, Color color)
        {
            sb.Draw(Source, position, SourceRect, color);
        }

        public void Draw(SpriteBatch sb, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale)
        {
            sb.Draw(Source, position, SourceRect, color, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        public void Draw(SpriteBatch sb, Rectangle destinationRect, Color color)
        {
            sb.Draw(Source, destinationRect, SourceRect, color);
        }

        public void Draw(SpriteBatch sb, Rectangle destinationRect, Color color, float rotation, Vector2 origin)
        {
            sb.Draw(Source, destinationRect, SourceRect, color, rotation, origin, SpriteEffects.None, 0f);
        }
        #endregion


        #region Static Methods
        public static FETexture FromFile(string filePath)
        {
            var texture = Content.LoadTextureFromFile(filePath);
            return new FETexture(texture);
        }

        public static FETexture FromName(string textureName)
        {
            var texture = Content.LoadTexture(textureName);
            return new FETexture(texture);
        }

        public static FETexture FromTexture2D(Texture2D source)
        {
            return new FETexture(source);
        }
        #endregion
    }
}
