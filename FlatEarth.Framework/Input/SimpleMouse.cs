using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FlatEarth.Input
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MouseButtons
    {
        LeftButton,
        MiddleButton,
        RightButton
    }

    public class SimpleMouse
    {
        private MouseState lastMouseState;
        private MouseState currentMouseState;

        public bool LockMouseToWindow = false;
        public bool UseVirtualCursor = false;
        public bool Touched;
        private int virtualMouseX;
        private int virtualMouseY;
        private int lastVirtualMouseX;
        private int lastVirtualMouseY;

        public SimpleMouse()
        {
            lastMouseState = Mouse.GetState();
            currentMouseState = Mouse.GetState();
            //virtualMouseX = game.Window.ClientBounds.Center.X;
            //virtualMouseY = game.Window.ClientBounds.Center.Y;
        }

        public void Update()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Touched = false;

            MoveVirtualMouse();
            ConstrainMouse();
            CheckTouched();
        }

        private void MoveVirtualMouse()
        {
            var changeInX = currentMouseState.X - lastMouseState.X;
            var changeInY = currentMouseState.Y - lastMouseState.Y;
            if (changeInX != 0 || changeInY != 0) Touched = true;

            if (UseVirtualCursor)
            {
                lastVirtualMouseX = virtualMouseX;
                lastVirtualMouseY = virtualMouseY;
                virtualMouseX += changeInX;
                virtualMouseY += changeInY;

                if (virtualMouseX < 0) virtualMouseX = 0;
                if (virtualMouseX > Resolution.Width) virtualMouseX = Resolution.Width;
                if (virtualMouseY < 0) virtualMouseY = 0;
                if (virtualMouseY > Resolution.Height) virtualMouseY = Resolution.Height;
            }
        }
        private void ConstrainMouse()
        {
            if (LockMouseToWindow)
                Mouse.SetPosition(Window.Center.X, Window.Center.Y);
        }
        private void CheckTouched()
        {
            if (currentMouseState != lastMouseState) Touched = true;
        }

        public Vector2 PositionVector2 { get => currentMouseState.Position.ToVector2(); }
        public Point Position { get => currentMouseState.Position; }

        public int X
        {
            get
            {
                if (LockMouseToWindow || UseVirtualCursor)
                    return virtualMouseX;
                else
                    return currentMouseState.X;
            }
        }
        public int Y
        {
            get
            {
                if (LockMouseToWindow || UseVirtualCursor)
                    return virtualMouseY;
                else return currentMouseState.Y;
            }
        }

        public int XChange
        {
            get
            {
                if (LockMouseToWindow || UseVirtualCursor)
                    return virtualMouseX - lastVirtualMouseX;
                else
                    return currentMouseState.X - lastMouseState.X;
            }
        }

        public int YChange
        {
            get
            {
                if (LockMouseToWindow || UseVirtualCursor)
                    return virtualMouseY - lastVirtualMouseY;
                else
                    return currentMouseState.Y - lastMouseState.Y;
            }
        }

        public bool WasButtonPressed(MouseButtons button)
        {
            return lastMouseState.IsButtonUp(button) && currentMouseState.IsButtonDown(button);
        }
        public bool WasButtonReleased(MouseButtons button)
        {
            return lastMouseState.IsButtonDown(button) && currentMouseState.IsButtonUp(button);
        }
        public bool IsButtonDown(MouseButtons button)
        {
            return currentMouseState.IsButtonDown(button);
        }
        public bool IsButtonUp(MouseButtons button)
        {
            return currentMouseState.IsButtonUp(button);
        }
    }
}
