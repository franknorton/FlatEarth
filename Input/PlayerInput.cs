using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Input
{
    public static class PlayerInput
    {
        private static InputMap[] playerInputMaps;

        static PlayerInput()
        {
            playerInputMaps = new InputMap[4]
            {
                new InputMap(PlayerIndex.One),
                new InputMap(PlayerIndex.Two),
                new InputMap(PlayerIndex.Three),
                new InputMap(PlayerIndex.Four)
            };
        }

        public static InputMap Player_One{
            get
            {
                return playerInputMaps[0];
            }
        }
        public static InputMap Player(PlayerIndex playerNumber)
        {
            return playerInputMaps[(int)playerNumber];
        }

        public static bool WasActionPressed(string actionName)
        {
            return playerInputMaps[0].WasActionPressed(actionName);
        }
        public static bool WasActionPressed(string actionName, PlayerIndex playerNumber)
        {
            return playerInputMaps[(int)playerNumber].WasActionPressed(actionName);
        }
        public static bool WasActionReleased(string actionName)
        {
            return playerInputMaps[0].WasActionReleased(actionName);
        }
        public static bool WasActionReleased(string actionName, PlayerIndex playerNumber)
        {
            return playerInputMaps[(int)playerNumber].WasActionReleased(actionName);
        }
        public static bool IsActionDown(string actionName)
        {
            return playerInputMaps[0].IsActionDown(actionName);
        }
        public static bool IsActionDown(string actionName, PlayerIndex playerNumber)
        {
            return playerInputMaps[(int)playerNumber].IsActionDown(actionName);
        }
        public static bool IsActionUp(string actionName)
        {
            return playerInputMaps[0].IsActionUp(actionName);
        }
        public static bool IsActionUp(string actionName, PlayerIndex playerNumber)
        {
            return playerInputMaps[(int)playerNumber].IsActionUp(actionName);
        }

        public static void Save(string relativeFilePath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFilePath);
            var inputJSON = JsonConvert.SerializeObject(playerInputMaps, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            File.WriteAllText(fullPath, inputJSON);
        }

        public static void Load(string relativeFilePath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFilePath);
            var fileJSON = File.ReadAllText(fullPath);
            playerInputMaps = JsonConvert.DeserializeObject<InputMap[]>(fileJSON, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }
    }
}
