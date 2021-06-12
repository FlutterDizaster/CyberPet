using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CyberPet
{
    public class Button : Entity
    {
        enum ButtonStates { ACTIVE, PUSHED, INACTIVE}

        private ButtonStates buttonState;
        private bool previousClickState;

        protected string name;

        public Button(Texture2D texture, string name) : base(texture)
        {
            this.name = name;
            buttonState = ButtonStates.ACTIVE;
            previousClickState = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if(buttonState != ButtonStates.INACTIVE)
            {

            }
        }

        protected void OnClick()
        {

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
                }
                else if(previousClickState && mouseState.LeftButton == ButtonState.Released)
                {
                    previousClickState = false;
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
    }
}
