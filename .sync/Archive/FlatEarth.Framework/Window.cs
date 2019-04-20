using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth
{
    public class WindowSizeChangeEventArgs
    {
        public int Width { get; set; }
        public int Height { get; set; }

    }
    public static class Window
    {
        private static GameWindow window;

        public static event EventHandler<WindowSizeChangeEventArgs> OnWindowSizeChanged;

        public static int Width { get => window.ClientBounds.Width; }
        public static int Height { get => window.ClientBounds.Height; }
        public static int X { get => window.Position.X; }
        public static int Y { get => window.Position.Y; }
        public static Point Center { get => window.ClientBounds.Center; }
        public static bool AllowUserResizing { get => window.AllowUserResizing; set => window.AllowUserResizing = value; }
        


        public static void Initialize(GameWindow window)
        {
            Window.window = window;
            window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private static void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            OnWindowSizeChanged?.Invoke(null, new WindowSizeChangeEventArgs { Width = Width, Height = Height });
        }

        public static void SetPosition(int x, int y)
        {
            window.Position = new Point(x, y);
        }

        public static void CenterOnScreen()
        {
            var position = new Point();
            position.X = (Engine.Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2) - (window.ClientBounds.Width / 2);
            position.Y = (Engine.Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2) - (window.ClientBounds.Height / 2);
            window.Position = position;
        }
    }
}
