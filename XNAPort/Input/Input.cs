using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.IO;

namespace Input
{
    public class InputManager : GameComponent
    {
        static private InputManager mInstance;

        private InputManager(Game game) : base(game)
        {
        }

        static public InputManager Instance
        {
            get
            {
                return mInstance;
            }
        }

        static public void CreateInstance(Game game)
        {
            mInstance = new InputManager(game);
        }

        public bool IsButtonDown(Buttons button, PlayerIndex index)
        {
            GamePadState currentGamePadState = GamePad.GetState(index);
            return currentGamePadState.IsButtonDown(button);
        }

        public bool IsButtonUp(Buttons button, PlayerIndex index)
        {
            GamePadState currentGamePadState = GamePad.GetState(index);
            return currentGamePadState.IsButtonUp(button);
        }
        public bool IsKeyDown(Keys key)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            return currentKeyboardState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            return currentKeyboardState.IsKeyUp(key);
        }

        public MouseState GetMouseState()
        {
            return Mouse.GetState();
        }
    }
}
