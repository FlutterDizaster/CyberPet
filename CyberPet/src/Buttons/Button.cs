using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CyberPet
{
    public delegate void OnClickDlg();

    public class Button : Entity
    {
        enum ButtonStates { ACTIVE, INACTIVE}

        private ButtonStates buttonState;
        private bool previousClickState;

        protected string name;
        protected SpriteFont spriteFont;
        protected Vector2 textSize;
        protected Vector2 textPosition;
        protected Vector2 textScale;

        protected OnClickDlg clickDlg;

        public Button(Texture2D texture, SpriteFont spriteFont, string name) : base(texture, new Point(3, 1))
        {
            buttonState = ButtonStates.ACTIVE;
            previousClickState = false;

            this.name = name;
            this.spriteFont = spriteFont;
            textSize = spriteFont.MeasureString(name);
            textPosition.X = position.X - origin.X + ((size.X - textSize.X) / 2);
            textPosition.Y = position.Y - origin.Y + ((size.Y - textSize.Y) / 2);

            Scale = new Vector2(7);
            textScale = new Vector2(Scale.X / 2.5f);
        }

        public Button(ContentManager Content, string name) : this(Content.Load<Texture2D>("Buttons"), Content.Load<SpriteFont>("Font"), name) { }

        public Button(ContentManager Content, string name, OnClickDlg onClick) :this(Content,name)
        {
            clickDlg = onClick;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            spriteBatch.Draw(texture, position, frameRect, Color.White, 0f, Origin, Scale, SpriteEffects.None, 0f);
            //spriteBatch.DrawString(spriteFont, name, textPosition, Color.White);
            spriteBatch.DrawString(spriteFont, name, textPosition, Color.White, 0, Vector2.Zero, textScale, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if(buttonState == ButtonStates.ACTIVE)
            {
                CheckClick();
            }

            
        }

        protected virtual void OnClick()
        {
            if (clickDlg != null)
            {
                clickDlg();
            }
        }

        private bool CheckClick()
        {
            var mouseState = Mouse.GetState();
            var mousePosition = mouseState.Position;

            if (IsHovered(mousePosition))
            {
                if (!previousClickState && mouseState.LeftButton == ButtonState.Pressed)
                {
                    previousClickState = true;
                    ChangeFrame(1);
                }
                else if(previousClickState && mouseState.LeftButton == ButtonState.Released)
                {
                    previousClickState = false;
                    ChangeFrame(0);
                    OnClick();
                    return true;
                }
            }
            return false;
        }

        private bool IsHovered(Point mousePosition)
        {
            var topLeftButtonCorner = new Vector2(position.X - origin.X, position.Y - origin.Y);
            if((mousePosition.X >= topLeftButtonCorner.X) && (mousePosition.X <= topLeftButtonCorner.X + Width))
                if((mousePosition.Y >= topLeftButtonCorner.Y) && (mousePosition.Y <= topLeftButtonCorner.Y + Height))
                {
                    return true;
                }

            return false;
        }

        private void ChangeFrame(int frame)
        {
            if(frame > -1)
            {
                frameRect = new Rectangle(frameWidth * frame, 0, frameWidth, frameHeight);
            }
        }

        public void SetActive(bool active)
        {
            if(active && buttonState != ButtonStates.ACTIVE)
            {
                buttonState = ButtonStates.ACTIVE;
                ChangeFrame(0);
            }
            else if(!active && buttonState != ButtonStates.INACTIVE)
            {
                buttonState = ButtonStates.INACTIVE;
                ChangeFrame(2);
            }
        }

        protected override void UpdatePosition(Vector2 position)
        {
            base.UpdatePosition(position);
            UpdateTextPosition(position);
        }

        protected void UpdateTextPosition(Vector2 position)
        {
            textPosition.X = position.X - origin.X + ((size.X - textSize.X * textScale.X) / 2);
            textPosition.Y = position.Y - origin.Y + ((size.Y - textSize.Y * textScale.Y) / 2);
        }
    }
}
