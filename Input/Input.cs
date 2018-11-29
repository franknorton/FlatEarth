using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FlatEarth.Input
{
    public static class Input
    {
        private static SimpleKeyboard keyboard;
        public static SimpleKeyboard Keyboard { get { return keyboard; } }

        private static SimpleMouse mouse;
        public static SimpleMouse Mouse { get { return mouse; } }

        private static SimpleGamePad[] gamepads;
        public static SimpleGamePad GamePad(PlayerIndex playerNumber) { return gamepads[(int)playerNumber]; }

        public static GamePadDeadZone GamePadDeadZoneMode;

        static Input() { }

        public static void Initialize()
        {
            keyboard = new SimpleKeyboard();
            mouse = new SimpleMouse();
            gamepads = new SimpleGamePad[4] 
            {
                new SimpleGamePad(PlayerIndex.One),
                new SimpleGamePad(PlayerIndex.Two),
                new SimpleGamePad(PlayerIndex.Three),
                new SimpleGamePad(PlayerIndex.Four)
            };
            GamePadDeadZoneMode = GamePadDeadZone.IndependentAxes;
        }

        public static bool AnyGamePadTouched()
        {
            foreach(var gamepad in gamepads)
            {
                if (gamepad.Touched)
                    return true;
            }

            return false;
        }
        public static bool MouseTouched()
        {
            return Mouse.Touched;
        }
        public static bool KeyboardTouched()
        {
            return Keyboard.Touched;
        }

        public static void Update(GameTime gameTime)
        {
            keyboard.Update();
            mouse.Update();
            foreach (var gamepad in gamepads)
                gamepad.Update(gameTime, GamePadDeadZoneMode);
        }
    }
}
