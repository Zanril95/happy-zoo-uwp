using Happy_Zoo.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Happy_Zoo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    public class Game1 : Game
    {
        #region Properties
        public GraphicsDeviceManager graphics { get; private set; }
        #endregion

        #region Fields
        SpriteBatch spriteBatch;
        
        private SpriteFont font;

        private MouseState oldSate;
        private int mouseX, mouseY;

        private State _currentState;
        private State _nextState;


        TimeSpan timeSpan = TimeSpan.FromMilliseconds(4000);

        bool nextScene = false;
        #endregion

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;

            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            StateManager.init(this, graphics.GraphicsDevice, Content);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load font
            font = Content.Load<SpriteFont>("Fonts/default");

            // load song
            //song = Content.Load<Song>("Music/Pixel-Puppies");
            MediaPlayer.Play(song);

            // loop the song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            StateManager.showSplashScreen();
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(song);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            #region keyboardControl
            /*
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 moveVelocity = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                moveVelocity += new Vector2(-1, 0);
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                moveVelocity += new Vector2(1, 0);
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                moveVelocity += new Vector2(0, 1);
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                moveVelocity += new Vector2(0, -1);
            }

            moveVelocity *= 5;

           // myMap.Camera.Move(moveVelocity);
           */
            #endregion

            if (_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            // Toggle Fullscreen ON/OFF with F11
            if (Keyboard.GetState().IsKeyDown(Keys.F11))
            {
                graphics.ToggleFullScreen();
            }

            // toggle pausemenu with esc
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                StateManager.showPauseMenu();
            }

            // base implementation of Update() from MonoGame
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(212, 231, 200));

            _currentState.Draw(gameTime, spriteBatch);

            timeSpan -= gameTime.ElapsedGameTime;

            if (timeSpan < TimeSpan.Zero && !nextScene)
            {
                nextScene = true;
                StateManager.showMenu();
            }

            base.Draw(gameTime);
        }
    }
}