using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_rpg
{
    public class Tile
    {
        Vector2 position;
        Rectangle sourceRect;

        public Vector2 Position { get { return position;  } }
        public Rectangle SourceRect { get { return sourceRect; } }
        
        public void LoadContent(Vector2 position, Rectangle sourceRect) 
        {
            this.position = position;
            this.sourceRect = sourceRect;
        }

        public void UnloadContent() { }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch) { }
    }
}
