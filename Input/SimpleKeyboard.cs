using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Threading;
using System.Windows;

namespace FlatEarth.Input
{
    public interface IKeyboardSubscriber
    {
        void OnCharEntered(char character, Keys key);
        void OnControlEntered(ControlCode code);
    }
    public class SimpleKeyboard
    {
        private KeyboardState lastKeyboardState;
        private KeyboardState currentKeyboardState;

        public IKeyboardSubscriber CharInputSubscriber;
        public bool Touched;

        public SimpleKeyboard()
        {
            lastKeyboardState = Keyboard.GetState();
            currentKeyboardState = Keyboard.GetState();

            //Works with Windows, Windows Open GL, and Linux Monogame versions
            //game.Window.TextInput += Window_TextInput;
        }

        internal void Update()
        {
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();


            Touched = false;
            if (currentKeyboardState.GetPressedKeys().Length > 0)
                Touched = true;
        }

        public bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }
        public bool WasKeyReleased(Keys key)
        {
            return lastKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key);
        }
        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        //Works with Windows, Windows Open GL, and Linux Monogame versions
        //private void Window_TextInput(object sender, TextInputEventArgs e)
        //{
        //    if (!game.IsActive)
        //        return;

        //    if (Char.IsControl(e.Character))
        //    {
        //        try
        //        {
        //            var control = ControlCodeParser.Parse(e.Character);
        //            if (CharInputSubscriber != null)
        //                CharInputSubscriber.OnControlEntered(control);
        //        }
        //        catch { }
        //    }
        //    else
        //    {
        //        if(CharInputSubscriber != null)
        //            CharInputSubscriber.OnCharEntered(e.Character, e.Key);
        //    }
        //}
    }
}
