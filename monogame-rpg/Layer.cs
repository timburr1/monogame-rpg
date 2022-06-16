using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_rpg
{

    public class Layer
    {
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row;

            public TileMap()
            {
                Row = new List<string>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap tileMap;
        public Image Image;
        public string SolidTiles, OverlayTiles;

        List<Tile> underlayTiles, overlayTiles;
        string state;
        
        public Layer()
        {
            Image = new Image();
            underlayTiles = new List<Tile>();
            overlayTiles = new List<Tile>();
            SolidTiles = OverlayTiles = String.Empty;
        }

        public  void LoadContent(Vector2 tileDimensions)
        {
            Image.LoadContent();

            //position for current tile
            Vector2 position = -tileDimensions;

            foreach(string row in tileMap.Row)
            {
                string[] split = row.Split(']');

                position.X = -tileDimensions.X;
                position.Y += tileDimensions.Y;

                foreach(string s in split)
                {
                    if (s != String.Empty)
                    {
                        position.X += tileDimensions.X;

                        if (!s.Contains("X"))
                        {
                            state = "Passive";
                            Tile tile = new Tile();

                            string str = s.Replace("[", "");
                            int val1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int val2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            if (SolidTiles.Contains("[" + val1.ToString() + ":" + val2.ToString() + "]"))
                                state = "Solid";

                            tile.LoadContent(position, new Rectangle(val1 * (int)tileDimensions.X, val2 * (int)tileDimensions.Y,
                                (int)tileDimensions.X, (int)tileDimensions.Y), state);

                            if (OverlayTiles.Contains("[" + val1.ToString() + ":" + val2.ToString() + "]"))
                                overlayTiles.Add(tile);
                            else
                                underlayTiles.Add(tile);
                        }
                    }
                }
            }
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            foreach (Tile tile in underlayTiles)
                tile.Update(gameTime, ref player);

            foreach (Tile tile in overlayTiles)
                tile.Update(gameTime, ref player);
        }

        public void Draw(SpriteBatch spriteBatch, string drawType)
        {
            List<Tile> tiles;

            if (drawType == "Underlay")
                tiles = underlayTiles;
            else
                tiles = overlayTiles;

            foreach(Tile tile in tiles)
            {
                Image.Position = tile.Position;
                Image.SourceRect = tile.SourceRect;
                Image.Draw(spriteBatch);
            }
        }
    }
}
