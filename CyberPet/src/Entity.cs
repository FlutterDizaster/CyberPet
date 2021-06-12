using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CyberPet
{
    public class Entity
    {
        private int verticalSegments;
        private int horizontalSegments;

        protected Texture2D texture;

        protected int frameWidth;
        protected int frameHeight;

        protected Vector2 position;
        protected Vector2 origin;
        protected Vector2 scale;
        protected Vector2 size;
        protected Rectangle frameRect;

        public Vector2 Position { get => position; set => UpdatePosition(value); }
        public Vector2 Origin { get => origin; set => origin = value; }
        public Vector2 Scale { get => scale; set => SetScale(value); }
        public int Width { get => (int)size.X; set => SetWidth(value); }
        public int Height { get => (int)size.Y; set => SetHeight(value); }

        public Entity(Texture2D texture) :this(texture, Vector2.One, new Point(1)) { }

        public Entity(Texture2D texture, Vector2 scale) : this(texture, scale, new Point(1)) { }

        public Entity(Texture2D texture, Point segments) : this(texture, Vector2.One, segments) { }

        public Entity(Texture2D texture, Vector2 scale, Point segments)
        {
            this.texture = texture;
            this.scale = scale;
            size.X = scale.X * texture.Width;
            size.Y = scale.Y * texture.Height;
            horizontalSegments = segments.X;
            verticalSegments = segments.Y;

            frameWidth = texture.Width / segments.X;
            frameHeight = texture.Height / segments.Y;

            frameRect = new Rectangle(0, 0, frameWidth, frameHeight);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            spriteBatch.Draw(texture, position, frameRect, Color.White, 0f, Origin, Scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        private void SetWidth(int value)
        {
            size.X = value;
            scale.X = value / texture.Width / horizontalSegments;
        }

        private void SetHeight(int value)
        {
            size.Y = value;
            scale.Y = value / texture.Height / verticalSegments;
        }

        private void SetScale(Vector2 scale)
        {
            this.scale = scale;
            size.X = scale.X * texture.Width / horizontalSegments;
            size.Y = scale.Y * texture.Height / verticalSegments;
        }

        protected virtual void UpdatePosition(Vector2 position)
        {
            this.position = position;
        }
    }
}
