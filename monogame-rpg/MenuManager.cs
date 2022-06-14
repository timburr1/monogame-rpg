using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_rpg
{
    public class MenuManager
    {
        Menu menu;
        public MenuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += menu_OnMenuChange;
        }

        void menu_OnMenuChange(object sender, EventArgs e)
        {
            XmlManager<Menu> xmlMenuManager = new XmlManager<Menu>();
            menu.UnloadContent();
            //TODO: add transition effect here
            menu = xmlMenuManager.Load(menu.ID);
            menu.LoadContent();
        }

        public void LoadContent(string menuPath)
        {
            if (menuPath != String.Empty)
                menu.ID = menuPath;
        }

        public void UnloadContent() 
        {
            menu.UnloadContent();
        }

        public void Update(GameTime gameTime) 
        {
            menu.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            menu.Draw(spriteBatch);
        }
    }
}
