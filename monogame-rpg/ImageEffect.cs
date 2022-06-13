using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace monogame_rpg
{
    public class ImageEffect
    {
        protected Image Image;
        public bool IsActive;

        public ImageEffect()
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Image Image)
        {
            this.Image = Image;
        }

        public virtual void UnloadContent() { }

        public virtual void Upate(GameTime gameTime) { }
        //public virtual void Draw() { }
    }
}
