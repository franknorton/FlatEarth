using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatEarth
{
    public class Resolution
    {
        public static int VirtualWidth { get; private set; }
        public static int VirtualHeight { get; private set; }
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static bool Fullscreen { get; private set; }
        public static Matrix ScaleMatrix { get; private set; }

        private static Viewport virtualViewport;
        private static Viewport fullViewport;

        public delegate void ResolutionChangeHandler();
        public static event ResolutionChangeHandler OnResolutionChange;

        static Resolution()
        {
            virtualViewport = new Viewport(0, 0, 0, 0, 0, 1);
            fullViewport = new Viewport(0, 0, 0, 0, 0, 1);
        }
        
        public static void Set(int width, int height, bool fullscreen)
        {
            Width = width;
            Height = height;
            Fullscreen = fullscreen;
            UpdateViewport();
            ApplyChanges();
        }
        public static void SetVirtual(int virtualWidth, int virtualHeight)
        {
            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;
            UpdateVirtualViewport();
            ApplyChanges();
        }
        public static void SwitchToVirtualViewport()
        {
            Engine.Graphics.GraphicsDevice.Viewport = fullViewport;
        }
        public static void SwitchToFullViewport()
        {
            Engine.Graphics.GraphicsDevice.Viewport = virtualViewport;
        }

        private static void UpdateViewport()
        {
            fullViewport.Width = Width;
            fullViewport.Height = Height;
        }
        private static void UpdateVirtualViewport()
        {
            if(Width / VirtualWidth > Height / VirtualHeight) //Letterbox
            {
                virtualViewport.Width = Height / VirtualHeight * VirtualWidth;
                virtualViewport.Height = Height;
            }
            else //Pillarbox
            {
                virtualViewport.Width = Width;
                virtualViewport.Height = Width / VirtualWidth * VirtualHeight;
            }

            virtualViewport.X = Width / 2 - virtualViewport.Width / 2;
            virtualViewport.Y = Height / 2 - virtualViewport.Height / 2;
            ScaleMatrix = Matrix.CreateScale(virtualViewport.Width / VirtualWidth);
        }

        private static void ApplyChanges()
        {
            if(!Fullscreen)
            {
                if(Width <= Engine.Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width
                    && Height <= Engine.Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height)
                {
                    Engine.Graphics.PreferredBackBufferWidth = Width;
                    Engine.Graphics.PreferredBackBufferHeight = Height;
                    Engine.Graphics.IsFullScreen = false;
                    Engine.Graphics.ApplyChanges();
                    OnResolutionChange?.Invoke();
                }
                else
                    throw new InvalidOperationException("Cannot set resolution larger than the current display resolution.");
            }
            else
            {
                if (Engine.Graphics.GraphicsDevice.Adapter.SupportedDisplayModes.Any(x => x.Width == Width && x.Height == Height))
                {
                    Engine.Graphics.PreferredBackBufferWidth = Width;
                    Engine.Graphics.PreferredBackBufferHeight = Height;
                    Engine.Graphics.IsFullScreen = true;
                    Engine.Graphics.ApplyChanges();
                    OnResolutionChange?.Invoke();
                }
                else
                    throw new InvalidOperationException("Display does not support this full-screen resolution.");
            }
        }
    }
}
