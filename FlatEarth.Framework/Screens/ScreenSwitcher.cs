using FlatEarth.Extensions;
using FlatEarth.Rendering2;
using FlatEarth.Screens.Transitions;
using FlatEarth.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Screens
{
    public class ScreenSwitcher
    {
        protected Screen currentScreen;
        protected Screen transitionToScreen;
        protected ScreenTransition transition;
        protected RenderTarget2D currentScreenRenderTarget;
        protected RenderTarget2D nextScreenRenderTarget;
        protected RenderTarget2D transitionRenderTarget;

        public ScreenSwitcher()
        {
            currentScreenRenderTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, Resolution.VirtualWidth, Resolution.VirtualHeight);
            nextScreenRenderTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, Resolution.VirtualWidth, Resolution.VirtualHeight);

            Renderer.GraphicsDeviceCreated += Renderer_GraphicsDeviceCreated;
        }

        private void Renderer_GraphicsDeviceCreated(object sender, EventArgs e)
        {
            currentScreenRenderTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, Resolution.VirtualWidth, Resolution.VirtualHeight);
            nextScreenRenderTarget = new RenderTarget2D(Engine.Graphics.GraphicsDevice, Resolution.VirtualWidth, Resolution.VirtualHeight);
        }

        public void SetScreen(Screen screen)
        {
            currentScreen?.Destroy();
            screen.Initialize();
            currentScreen = screen;
        }

        #region Transitions
        public void ChangeScreenTransition(Screen screen, ScreenTransition transition)
        {
            screen.Initialize();
            transitionToScreen = screen;
            this.transition = transition;
        }
        public void ChangeScreenFade(Screen screen, int lengthInMilliseconds, Color fadeToColor)
        {
            screen.Initialize();
            transitionToScreen = screen;
            transition = new TransitionFade(lengthInMilliseconds, fadeToColor);
        }
        public void ChangeScreenSlide(Screen screen, int lengthInMilliseconds, Direction slideDirection)
        {
            screen.Initialize();
            transitionToScreen = screen;
            transition = new TransitionSlide(lengthInMilliseconds, slideDirection);
        }
        public void ChangeScreenPush(Screen screen, int lengthInMilliseconds, Direction pushDirection)
        {
            screen.Initialize();
            transitionToScreen = screen;
            transition = new TransitionPush(lengthInMilliseconds, pushDirection);
        }
        public void ChangeScreenGrow(Screen screen, int lengthInMilliseconds)
        {
            screen.Initialize();
            transitionToScreen = screen;
            transition = new TransitionGrow(lengthInMilliseconds);
        }
        public void ChangeScreenCrossFade(Screen screen, int lengthInMilliseconds)
        {
            screen.Initialize();
            transitionToScreen = screen;
            transition = new TransitionCrossFade(lengthInMilliseconds);
        }
        #endregion

        public void Update(GameTime gameTime)
        {
            if (transition != null)
                UpdateTransition(gameTime);
            else
                currentScreen?.Update(gameTime);
        }

        protected void UpdateTransition(GameTime gameTime)
        {
            transition.Update(gameTime);
            if(transition.Done)
            {
                currentScreen?.Destroy();
                currentScreen = transitionToScreen;
                transitionToScreen = null;
                transition = null;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            if (transition == null)
            {
                var renderTarget = currentScreen?.Draw(spriteBatch);
                Engine.Graphics.GraphicsDevice.Clear(Color.White);
                Resolution.SwitchToVirtualViewport();
                renderTarget.RenderToForeground(spriteBatch);
                Resolution.SwitchToFullViewport();
            }
            else
                DrawTransition(spriteBatch);             
        }

        protected void DrawTransition(SpriteBatch spriteBatch)
        {
            var currentTarget = currentScreen.Draw(spriteBatch);
            var nextTarget = transitionToScreen.Draw(spriteBatch);
            transition.Draw(transitionRenderTarget, spriteBatch, currentTarget, nextTarget);
            transitionRenderTarget.RenderToForeground(spriteBatch);
        }
    }
}
