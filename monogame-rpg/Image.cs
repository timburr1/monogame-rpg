using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_rpg
{
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path;
        public Vector2 Position, Scale;        
        public Rectangle SourceRect;
        public bool IsActive;
        
        Texture2D Texture;
        Vector2 origin;
        ContentManager contentManager;
        RenderTarget2D renderTarget;
        SpriteFont font;
        Dictionary<string, ImageEffect> effectMap;
        public string Effects;

        public FadeEffect FadeEffect;

        void SetEffect<T>(ref T effect) 
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }

            effectMap.Add(effect.GetType().ToString().Replace("monogame_rpg.", ""), (effect as ImageEffect));
        }

        public void ActivateEffect(string effect) 
        { 
            if(effectMap.ContainsKey(effect))
            {
                effectMap[effect].IsActive = true;
                var obj = this;
                effectMap[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect) 
        { 
            if(effectMap.ContainsKey(effect))
            {
                effectMap[effect].IsActive = false;
                effectMap[effect].UnloadContent();
            }
        }

        public Image()
        {
            Path = Text = String.Empty;
            FontName = "Fonts/Yoster Island";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            effectMap = new Dictionary<string, ImageEffect>();
        } 

        public void LoadContent() 
        {
            contentManager = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if(Path != String.Empty)
                Texture = contentManager.Load<Texture2D>(Path);         
             
            font = contentManager.Load<SpriteFont>(FontName); 

            Vector2 dimensions = Vector2.Zero;

            if (Texture != null) {
                dimensions.X += Texture.Width;
                dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            }
            else
                dimensions.Y = font.MeasureString(Text).Y;

            dimensions.X += font.MeasureString(Text).X;

            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            
            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)dimensions.X, (int)dimensions.Y);
            
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();

            if(Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);

            ScreenManager.Instance.SpriteBatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref FadeEffect);

            if(Effects != null && Effects != String.Empty)
            {
                string[] split = Effects.Split(':');
                foreach(string item in split)
                    ActivateEffect(item);
            }
            
        }

        public void UnloadContent() {
            contentManager.Unload();
            foreach (var effect in effectMap)
                DeactivateEffect(effect.Key);
        }

        public void Update(GameTime gameTime) 
        {
            foreach (var effect in effectMap)
            {
                if(effect.Value.IsActive)
                    effect.Value.Upate(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            spriteBatch.Draw(Texture, Position + origin, SourceRect, Color.White * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
