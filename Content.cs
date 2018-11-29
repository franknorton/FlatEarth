using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FlatEarth
{
    public static class Content
    {

        public static string AssemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string ContentDirectory
        {
#if PS4
            get { return Path.Combine("/app0/", Engine.Instance.Content.RootDirectory); }
#elif NSWITCH
            get { return Path.Combine("rom:/", Engine.Instance.Content.RootDirectory); }
#elif XBOXONE
            get  {return Engine.Instance.Content.RootDirectory; }
#else
            get { return Path.Combine(AssemblyDirectory, Engine.Instance.Content.RootDirectory); }
#endif
        }

        public static Texture2D LoadTextureFromFile(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var texture = Texture2D.FromStream(Engine.Instance.GraphicsDevice, fileStream);
                return texture;
            }
        }

        public static Effect LoadEffect(string effectName)
        {
            return Load<Effect>(effectName);
        }

        public static SpriteFont LoadFont(string fontName)
        {
            return Load<SpriteFont>(fontName);
        }

        public static Texture2D LoadTexture(string textureName)
        {
            return Load<Texture2D>(textureName);
        }

        public static T Load<T>(string fileName)
        {
            return Engine.Instance.Content.Load<T>(fileName);
        }
    }
}
