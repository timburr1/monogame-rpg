
using Microsoft.Xna.Framework;

namespace monogame_rpg
{
    public class Tile
    {
        Vector2 position;
        Rectangle sourceRect;
        string state;

        public Vector2 Position { get { return position;  } }
        public Rectangle SourceRect { get { return sourceRect; } }
        
        public void LoadContent(Vector2 position, Rectangle sourceRect, string state) 
        {
            this.position = position;
            this.sourceRect = sourceRect;
            this.state = state;
        }

        public void UnloadContent() { }

        public void Update(GameTime gameTime, ref Player player) 
        { 
            if(state == "Solid")
            {
                Rectangle tileRect = new Rectangle((int)position.X, (int) position.Y, sourceRect.Width, sourceRect.Height);
                Rectangle playerRect = new Rectangle((int)player.Image.Position.X, (int)player.Image.Position.Y, player.Image.SourceRect.Width, player.Image.SourceRect.Height);
                
                if(playerRect.Intersects(tileRect))
                {
                    if (player.Velocity.X < 0)
                        player.Image.Position.X = tileRect.Right;
                    else if(player.Velocity.X > 0)
                        player.Image.Position.X = tileRect.Left - player.Image.SourceRect.Width;
                    else if (player.Velocity.Y < 0)
                        player.Image.Position.Y = tileRect.Bottom;
                    else if (player.Velocity.Y > 0)
                        player.Image.Position.Y = tileRect.Top - player.Image.SourceRect.Height;
                    
                    player.Velocity = Vector2.Zero;
                }
            }
        }

        //public void Draw(SpriteBatch spriteBatch) { }
    }
}
