﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_rpg
{
    public class Map
    {
        [XmlElement("Layers")]
        public List<Layer> Layers;
        public Vector2 TileDimensions;

        public Map()
        {
            Layers = new List<Layer>();
            TileDimensions = Vector2.Zero;
        }

        public void LoadContent()
        {
            foreach (Layer l in Layers)
                l.LoadContent(TileDimensions);
        }

        public void UnloadContent()
        {
            foreach (Layer l in Layers)
                l.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Layer l in Layers)
                l.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Layer l in Layers)
                l.Draw(spriteBatch);
        }
    }
}
