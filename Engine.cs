using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FlatEarth
{
    public class Engine : Game
    {

        public static Engine Instance { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static Renderer Renderer { get; private set; }

        public ResourceContentManager DefaultResourceContent { get; private set; }

        //Size
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static int ViewWidth { get; private set; }
        public static int ViewHeight { get; private set; }

        //Timing
        public static float DeltaTime { get; private set; }
        public static float RawDeltaTime { get; private set; }
        public static GameTime GameTime { get; private set; }
        public static float TimeRate = 1f;

        protected override void Initialize()
        {
            base.Initialize();

            FlatEarth.Window.Initialize(Window);
            Renderer = new Renderer();
            DefaultResourceContent = new ResourceContentManager(Services, DefaultResources.ResourceManager);
        }

        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTime = RawDeltaTime * TimeRate;

            base.Update(gameTime);
        }

    }
}
