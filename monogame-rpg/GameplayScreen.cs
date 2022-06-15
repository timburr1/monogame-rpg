﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_rpg
{
    public class GameplayScreen : GameScreen
    {
        Player player;
        Map map;

        public override void LoadContent()
        {
            base.LoadContent();

            XmlManager<Player> playerLoader = new XmlManager<Player>();
            player = playerLoader.Load("Load/Gameplay/Player.xml");
            player.LoadContent();

            XmlManager<Map> mapLoader = new XmlManager<Map>();
            map = mapLoader.Load("Load/Gameplay/Maps/Map1.xml");
            map.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.Update(gameTime);
            map.Update(gameTime, ref player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }
    }
}
