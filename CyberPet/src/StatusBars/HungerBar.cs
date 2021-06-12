using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CyberPet
{
    public class HungerBar : StatusBar
    {
        private Pet _target;

        public HungerBar(ContentManager Content, Pet pet) :base(Content, new Color(56, 109, 115), "Hunger")
        {
            _target = pet;
        }

        public override void Update(GameTime gameTime)
        {
            SetWidth(_target.Hunger);
        }
    }
}
