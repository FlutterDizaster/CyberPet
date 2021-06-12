using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CyberPet
{
    public class StaminaBar : StatusBar
    {
        private Pet _target;

        public StaminaBar(ContentManager Content, Pet pet) :base(Content, new Color(242, 191, 144), "Stamina")
        {
            _target = pet;
        }

        public override void Update(GameTime gameTime)
        {
            SetWidth(_target.Stamina);
        }
    }
}
