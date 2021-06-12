using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CyberPet
{
    public class HealthBar : StatusBar
    {
        private Pet _target;

        public HealthBar(ContentManager Content, Pet pet) : base(Content, new Color(114, 24, 14), "Health")
        {
            _target = pet;
        }

        public override void Update(GameTime gameTime)
        {
            SetWidth(_target.Health);
        }
    }
}
