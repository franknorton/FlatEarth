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
        public static int VirtualWidth { get; private set; } = 800;
        public static int VirtualHeight { get; private set; } = 600;
        public static int Width { get; private set; } = 800;
        public static int Height { get; private set; } = 600;
        public static bool Fullscreen { get; private set; } = false;
        public static Matrix ScaleMatrix { get; private set; }

        private static Viewport virtualViewport;
        private static Viewport fullViewport;

        public delegate void ResolutionChangeHandler();
        public static event ResolutionChangeHandler OnResolutionChange;

        static Resolution()
        {
            virtualViewport = new Viewport(0, 0, VirtualWidth, VirtualHeight, 0, 1);
            fullViewport = new Viewport(0, 0, Width, Height, 0, 1);

            Window.OnWindowSizeChanged += Window_OnWindowSizeChanged;
        }

        private static void Window_OnWindowSizeChanged(object sender, WindowSizeChangeEventArgs e)
        {
            //Reset size based on actual resolution, virtual stays the same. 
            Set(Window.Width, Window.Height, false);
        }

        public static void Set(int width, int height, bool fullscreen)
        {
            if (width == Width && height == Height && fullscreen == Fullscreen)
                return;

            Width = width;
            Height = height;
            Fullscreen = fullscreen;
            UpdateViewports();
            ApplyChanges();
        }
        public static void SetVirtual(int virtualWidth, int virtualHeight)
        {
            if (virtualWidth == VirtualWidth && virtualHeight == VirtualHeight)
                return;

            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;
            UpdateVirtualViewport();
            ApplyChanges();
        }
        public static void SwitchToVirtualViewport()
        {
            Engine.Graphics.GraphicsDevice.Viewport = virtualViewport;
        }
        public static void SwitchToFullViewport()
        {
            Engine.Graphics.GraphicsDevice.Viewport = fullViewport;
        }

        private static void UpdateViewports()
        {
            UpdateViewport();
            UpdateVirtualViewport();
        }

        private static void UpdateViewport()
        {
            fullViewport.Width = Width;
            fullViewport.Height = Height;
        }
        private static void UpdateVirtualViewport()
        {
            if((float)Width / VirtualWidth > (float)Height / VirtualHeight) //Letterbox
            {
                virtualViewport.Width = (int)((float)Height / VirtualHeight * VirtualWidth);
                virtualViewport.Height = Height;
            }
            else //Pillarbox
            {
                virtualViewport.Width = Width;
                virtualViewport.Height = (int)((float)Width / VirtualWidth * VirtualHeight);
            }

            virtualViewport.X = (int)((float)Width / 2f - virtualViewport.Width / 2f);
            virtualViewport.Y = (int)((float)Height / 2f - virtualViewport.Height / 2f);
            ScaleMatrix = Matrix.CreateScale((float)virtualViewport.Width / VirtualWidth);
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
