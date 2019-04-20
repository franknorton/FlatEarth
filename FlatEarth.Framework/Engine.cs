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

        public Engine() : base()
        {
            Instance = this;
            Graphics = new GraphicsDeviceManager(this);
            Graphics.HardwareModeSwitch = false;
        }

        protected override void Initialize()
        {

            Content = new ContentManager(this.Services, "Content");
            DefaultResourceContent = new ResourceContentManager(Services, DefaultResources.ResourceManager);
            base.Initialize();
            
            Defaults.Initialize();

            Input.Input.Initialize();
            DebugDraw.Initialize(Graphics.GraphicsDevice);
            FlatEarth.Window.Initialize(Window);
            Resolution.Set(1280, 1024, false);
            Resolution.SetVirtual(800, 800);
            Renderer = new Renderer();
            
            
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Input.Update(gameTime);
            GameTime = gameTime;
            RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTime = RawDeltaTime * TimeRate;

            base.Update(gameTime);
        }
    }
}
