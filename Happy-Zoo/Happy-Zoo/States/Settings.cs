using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Happy_Zoo.Controls;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Happy_Zoo.States
{
    class Settings : State
    {
        #region Fields
        private List<Component> _components;
        private int handleXStart = 312 + ((712 - 312) / 2);
        private int handleYStart = 280;
        private int handleXEnd;
        private int handleYEnd;
        private int sliderXStart = 312;
        private int sliderYStart = 285;
        private int sliderXEnd = 712;
        private int sliderYEnd;
        private int handleWidth = 16;
        private int handleHeight = 41;
        private int lastHandlePosition = 0;
        private int screenCenter = 1024 / 2;
        private Color brownColour = new Color(102, 71, 54);
        private GraphicsDeviceManager graphics;
        #endregion

        public Settings(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            #region Constructor fields
            #region Button images
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonZebraWide = _content.Load<Texture2D>("Controls/zebraText");
            var buttonGiraffeWide = _content.Load<Texture2D>("Controls/giraffeText");
            var buttonPantherWide = _content.Load<Texture2D>("Controls/panterText");
            var buttonZebraSmall = _content.Load<Texture2D>("Controls/knopZebra");
            var buttonGiraffeSmall = _content.Load<Texture2D>("Controls/knopGirraffe");
            var buttonPantherSmall = _content.Load<Texture2D>("Controls/knopPanter");
            var playButton = _content.Load<Texture2D>("Controls/PlayButton");
            var muteButton = _content.Load<Texture2D>("Controls/MuteButton");
            var backButton = _content.Load<Texture2D>("Controls/BackButton");
            var fullscreenButton = _content.Load<Texture2D>("Controls/FullScreenButton");
            var font = _content.Load<SpriteFont>("Fonts/blokletters");
            #endregion

            graphics = game.graphics;
            #endregion

            #region Calculations
            int bScreenCenter = screenCenter - (buttonPantherWide.Width / 2);
            #endregion

            // 'resume game' button
            var resumeGameButton = new Button(buttonPantherWide, font)
            {
                Position = new Vector2(bScreenCenter, 450),
                Text = "Resume Game",
            };

            // add a click to the 'resume game' button
            resumeGameButton.Click += ResumeGameButton_Click;

            // mute button
            var musicMuteButton = new Button(playButton, font)
            {
                Position = new Vector2(resumeGameButton.Rectangle.X, 350),
            };
            
            // add a click to the mute button
            musicMuteButton.Click += MuteButton_Click;

            // full screen button
            var fullScreenButton = new Button(fullscreenButton, font)
            {
                Position = new Vector2(screenCenter - (fullscreenButton.Width / 2), 350),
            };

            // add a click to the full screen button
            fullScreenButton.Click += FullScreenButton_Click;

            // back button
            var goBackButton = new Button(backButton, font)
            {
                Position = new Vector2(screenCenter + (resumeGameButton.Rectangle.Width / 2) - backButton.Width, 350),
            };

            // add a click to the back button
            goBackButton.Click += BackButton_Click;

            // add buttons to a list
            _components = new List<Component>()
            {
                musicMuteButton,
                resumeGameButton,
                fullScreenButton,
                goBackButton,
            };
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // go back to the last screen
            StateManager.showLastScreen();
        }

        private void FullScreenButton_Click(object sender, EventArgs e)
        {
            // toggle fullscreen
            graphics.ToggleFullScreen();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var font = _content.Load<SpriteFont>("Fonts/blokletters");                  // font
            float volumeWidth = font.MeasureString("Volume").X;                         // width of the volumeslider
            var background = _content.Load<Texture2D>("Images/settingsbg");             // backgroundimage
            var volumeSlider = _content.Load<Texture2D>("Controls/Volume");             // image of the volumeslider
            var handle = _content.Load<Texture2D>("Controls/slider");                   // image of the handle
            var currentMouseState = Mouse.GetState();                                   // current mouse state
            var mousePosition = new Point(currentMouseState.X, currentMouseState.Y);    // current mouse position

            // start the spritebatch
            spriteBatch.Begin();

            // draw background
            spriteBatch.Draw(background, new Rectangle(0, 0, 1024, 768), Color.White);  

            // slider
            handleXEnd = handleXStart + handleWidth;    // calculate the X of the end of the handle
            handleYEnd = handleYStart + handleHeight;   // calculate the Y of the end of the handle

            spriteBatch.DrawString(font, "Volume", new Vector2(screenCenter - (volumeWidth / 2), sliderYStart - 50), brownColour);  // draw the word 'Volume'

            spriteBatch.Draw(volumeSlider, new Vector2(sliderXStart, sliderYStart));    // draw the volumeslider
            spriteBatch.Draw(handle, new Vector2(handleXStart, handleYStart));          // draw the handle

            // if left mouse button is pressed and the mouse position is somewhere in between the dimensions of the handle, move the handle towards the position of the mouse
            if (currentMouseState.LeftButton == ButtonState.Pressed && mousePosition.X > handleXStart && mousePosition.X < handleXEnd && mousePosition.Y > handleYStart && mousePosition.Y < handleYEnd
                || currentMouseState.LeftButton == ButtonState.Pressed && mousePosition.X > sliderXStart && mousePosition.X < sliderXEnd && mousePosition.Y > handleYStart && mousePosition.Y < handleYEnd)
            {
                // if the X position of the mouse is in front of the slider
                if (currentMouseState.X < sliderXStart)
                {
                    // put the handle at the start of the slider
                    handleXStart = sliderXStart;
                }
                // if the X position of the mouse is behind of the slider
                else if (currentMouseState.X > sliderXEnd)
                {
                    // put the handle at the end of the slider
                    handleXStart = sliderXEnd - handleWidth;
                }
                // if the X position of the mouse is on the slider
                else
                {
                    // move the handle to the right position
                    handleXStart = currentMouseState.X - (handleWidth / 2);
                }
            };

            // calculate volume based on the position of the handle
            MediaPlayer.Volume = (handleXStart - sliderXStart - (handleWidth / 2)) * 0.0025f;

            // draw buttons
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            // end the spritebatch
            spriteBatch.End();
        }

        private void ResumeGameButton_Click(object sender, EventArgs e)
        {
            // load new state
            StateManager.showGame();
        }

        private void MuteButton_Click(object sender, EventArgs e)
        {
            var currentVolume = MediaPlayer.Volume; // current volume
            var lastVolume = 0.0f;                  // initializing the variable 'lastVolume'

            // if the volume is not 0, mute volume
            if (MediaPlayer.Volume > 0.0f)
            {
                lastVolume = currentVolume;         // set 'lastVolume' to current volume
                lastHandlePosition = handleXStart;  // set the last handle position current handle position
                MediaPlayer.Volume = 0.0f;          // mute the sound
                handleXStart = sliderXStart;        // place handle at the start of the slider
            }
            // if the volume is 0
            else if (MediaPlayer.Volume == 0.0f)
            {
                handleXStart = lastHandlePosition;  // place handle back to last known postion
            }
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