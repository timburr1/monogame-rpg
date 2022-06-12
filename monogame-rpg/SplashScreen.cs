using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_rpg
{
    public class SplashScreen : GameScreen
    {
        Texture2D image;
        public string Path;
        public Vector2 Position;
        public override void LoadContent()
        {
            base.LoadContent();
            image = content.Load<Texture2D>(Path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, Color.White);
        }
    }
}
