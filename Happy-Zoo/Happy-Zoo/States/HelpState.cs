using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Happy_Zoo.Controls;

namespace Happy_Zoo.States
{
    class HelpState : State
    {
        private List<Component> _components;
        private Color brownColour = new Color(102, 71, 54);
        private int screenCenter = 1024 / 2;

        public HelpState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            #region Button images
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonZebraWide = _content.Load<Texture2D>("Controls/zebraText");
            var buttonGiraffeWide = _content.Load<Texture2D>("Controls/giraffeText");
            var buttonPantherWide = _content.Load<Texture2D>("Controls/panterText");
            var backButton = _content.Load<Texture2D>("Controls/BackButton");
            var buttonFont = _content.Load<SpriteFont>("Fonts/blokletters");
            #endregion

            #region Calculations
            int screenCenter = 1024 / 2;
            int bScreenCenter = screenCenter - (buttonPantherWide.Width / 2);
            #endregion

            // back button
            var goBackButton = new Button(backButton, buttonFont)
            {
                Position = new Vector2(screenCenter - 265, 490),
            };

            // add a click to the back button
            goBackButton.Click += BackButton_Click;

            // 'resume game' button
            var resumeGameButton = new Button(buttonZebraWide, buttonFont)
            {
                Position = new Vector2(goBackButton.Position.X + 125, 480),
                Text = "Resume Game",
            };

            // add a click to the 'resume game' button
            resumeGameButton.Click += ResumeGameButton_Click;
            
            // add buttons to a list
            _components = new List<Component>()
            {
                goBackButton,
                resumeGameButton,
            };
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            StateManager.showLastScreen();
        }

        private void ResumeGameButton_Click(object sender, EventArgs e)
        {
            StateManager.showGame();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var font = _content.Load<SpriteFont>("Fonts/blokletters");                          // font
            var background = _content.Load<Texture2D>("Images/newgamebg");                      // backgroundimage
            float lineA = font.MeasureString("To place a facility or animal").X;                // width of first line
            float lineB = font.MeasureString("house, click on the designated").X;               // width of second line
            float lineC = font.MeasureString("button and press 'E' on the map").X;              // width of third line
            float lineD = font.MeasureString("to place it.").X;                                 // width of the fourth line

            float lineE = font.MeasureString("To enter the menu, press").X;                     // width of fifth line
            float lineF = font.MeasureString("'Escape'.").X;                                    // width of sixth line
            // start the spritebatch
            spriteBatch.Begin();

            // draw background
            spriteBatch.Draw(background, new Rectangle(0, 0, 1024, 768), Color.White);

            // draw the help text
            spriteBatch.DrawString(font, "To place a facility or animal", new Vector2(screenCenter - (lineA / 2), 220), brownColour);
            spriteBatch.DrawString(font, "house, click on the designated", new Vector2(screenCenter - (lineB / 2), 260), brownColour);
            spriteBatch.DrawString(font, "button and press 'E' on the map", new Vector2(screenCenter - (lineC / 2), 300), brownColour);
            spriteBatch.DrawString(font, "to place it.", new Vector2(screenCenter - (lineD / 2), 340), brownColour);
            spriteBatch.DrawString(font, "To enter the menu, press", new Vector2(screenCenter - (lineE / 2), 400), brownColour);
            spriteBatch.DrawString(font, "'Escape'.", new Vector2(screenCenter - (lineF / 2), 440), brownColour);

            // draw buttons
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            // end the spritebatch
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }     

        public override void Update(GameTime gameTime)
        {
            // update components
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
