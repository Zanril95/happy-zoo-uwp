using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Happy_Zoo.Controls;

namespace Happy_Zoo.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            #region Button images
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonZebraWide = _content.Load<Texture2D>("Controls/zebraText");
            var buttonGiraffeWide = _content.Load<Texture2D>("Controls/giraffeText");
            var buttonPantherWide = _content.Load<Texture2D>("Controls/panterText");
            var buttonFont = _content.Load<SpriteFont>("Fonts/blokletters");
            #endregion

            #region Calculations
            int screenCenter = 1024 / 2;
            int bScreenCenter = screenCenter - (buttonPantherWide.Width / 2);
            #endregion

            // 'new game' button
            var newGameButton = new Button(buttonPantherWide, buttonFont)
            {
                Position = new Vector2(bScreenCenter, 200),
                Text = "New Game",
            };

            // add a click to the 'new game' button
            newGameButton.Click += NewGameButton_Click;

            // settings button
            var settingsButton = new Button(buttonGiraffeWide, buttonFont)
            {
                Position = new Vector2(bScreenCenter, newGameButton.Position.Y + 100),
                Text = "Settings",
            };

            // add a click to the settings button
            settingsButton.Click += SettingsButton_Click;

            // help button
            var helpButton = new Button(buttonZebraWide, buttonFont)
            {
                Position = new Vector2(bScreenCenter, settingsButton.Position.Y + 100),
                Text = "Help",
            };

            // add a click to the 'load game' button
            helpButton.Click += HelpButton_Click;

            // 'quit game' button
            var quitGameButton = new Button(buttonPantherWide, buttonFont)
            {
                Position = new Vector2(bScreenCenter, helpButton.Position.Y + 100),
                Text = "Quit Game",
            };

            // add a click to the 'quit game' button
            quitGameButton.Click += QuitGameButton_Click;

            // add buttons to a list
            _components = new List<Component>()
            {
                newGameButton,
                helpButton,
                settingsButton,
                quitGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var background = _content.Load<Texture2D>("Images/newgamebg");  // backgroundimage

            // start the spritebatch
            spriteBatch.Begin();

            // draw background
            spriteBatch.Draw(background, new Rectangle(0, 0, 1024, 768), Color.White);

            // draw buttons
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            // end the spritebatch
            spriteBatch.End();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            StateManager.showGame();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            // exit game
            _game.Exit();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            // load settings
            StateManager.showSettings();
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            // help page
            StateManager.showHelp();
        }

        public override void Update(GameTime gameTime)
        {
            // update components
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
