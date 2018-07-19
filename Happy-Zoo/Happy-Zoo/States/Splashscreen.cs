using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Happy_Zoo.States
{
    class Splashscreen : State
    {
        #region Fields
        private Texture2D splashscreen;
        #endregion

        public Splashscreen(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            //
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // load splashscreen image
            var splashscreen = _content.Load<Texture2D>("Images/Splashscreen-geenlaadbalk-resized");
            var fullscreen = new Rectangle(0, 0, 1024, 768);    // window set to 1024x768

            // start the spritebatch
            spriteBatch.Begin();

            // draw background
            spriteBatch.Draw(splashscreen, fullscreen, Color.White);

            // end the spritebatch
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            //
        }
    }
}
