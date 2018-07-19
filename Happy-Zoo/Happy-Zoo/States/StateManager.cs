using Happy_Zoo.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;


namespace Happy_Zoo.States
{
   static class StateManager
    {
        private static Game1 game;
        private static GraphicsDevice graphicsDevice;
        private static ContentManager content;

        private static State gameS, menuS, pauseMenuS, settingsS, helpS, splashscreenS, currentState = null , lastState = null;

        private static void updateLastCurrent(State s)
        {
            if (currentState != null && lastState != null)
            {
                lastState = currentState;
                currentState = s;
            } else {
                currentState = s;
                lastState = s;
            }
     
        }

        #region Public methods
        public static void init(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            StateManager.game = game;
            StateManager.graphicsDevice = graphicsDevice;
            StateManager.content = content;
         
            StateManager.gameS = new GameState(game, graphicsDevice, content);
            StateManager.menuS = new MenuState(game, graphicsDevice, content);
            StateManager.pauseMenuS = new PauseMenuState(game, graphicsDevice, content);
            StateManager.settingsS = new Settings(game, graphicsDevice, content);
            StateManager.helpS = new HelpState(game, graphicsDevice, content);
            StateManager.splashscreenS = new Splashscreen(game, graphicsDevice, content);
        }

        // show the game
        public static void showGame()
        {
            game.ChangeState(gameS);
            updateLastCurrent(gameS);
        }

        //overload for loading in a saved game
        public static void showGame(GameState s)
        {
            StateManager.gameS = s;
            game.ChangeState(s);
            updateLastCurrent(s);
        }

        // show the menu
        public static void showMenu()
        {
            game.ChangeState(menuS);
            updateLastCurrent(menuS);
        }

        // show the paused menu
        public static void showPauseMenu()
        {
            game.ChangeState(pauseMenuS);
            updateLastCurrent(pauseMenuS);
        }

        // show the settings
        public static void showSettings()
        {
            game.ChangeState(settingsS);
            updateLastCurrent(settingsS);
        }

        // show the settings
        public static void showHelp()
        {
            game.ChangeState(helpS);
            updateLastCurrent(helpS);
        }

        // show the splashscreen
        public static void showSplashScreen()
        {
            game.ChangeState(splashscreenS);
            updateLastCurrent(splashscreenS);
        }

        // show the last screen
        public static void showLastScreen()
        {
            game.ChangeState(lastState);
            updateLastCurrent(lastState);
        }
        
        //returns the GameState object (the currenctly played game state)
        public static GameState getGame()
        {
            return (GameState)gameS;
        }
        #endregion
    }
}
