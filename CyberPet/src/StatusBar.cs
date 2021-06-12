using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CyberPet
{
    public class StatusBar : Entity
    {
        protected int factor;

        protected string name;
        protected SpriteFont spriteFont;

        protected Color baseColor;
        protected Color fillColor;

        protected Rectangle frameRect;

        public int Factor { get => factor; set => SetWidth(value); }

        public StatusBar(ContentManager Content ,Color color, string name) :base(Content.Load<Texture2D>("StatusBarTex"))
        {
            scale = new Vector2(20);
            baseColor = new Color(0, 0, 0);
            fillColor = color;
            factor = 10;
            position = Vector2.Zero;
            this.name = name;
            spriteFont = Content.Load<SpriteFont>("Font");
            frameRect = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            spriteBatch.Draw(texture, position, null, baseColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, position, frameRect, fillColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(spriteFont, name, position, Color.White);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {

        }

        protected void SetWidth(int value)
        {
            factor = value;

            if (value < 0)
                value = 0;
            if (value > 10)
                value = 10;

            frameRect.Width = (int)((float)texture.Width * value / 10);

        }
    }
}
