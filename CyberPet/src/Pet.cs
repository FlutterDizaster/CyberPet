using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CyberPet
{
    enum PetState { ALIVE, DEAD}

    public class Pet : Entity
    {
        private Point segments;
        private int currentFrame;
        private int frameTime;

        private float speed;
        private int health;
        private int hunger;
        private int stamina;
        private PetState petState;

        private float _timer = 0;

        public int Health { get => health; }
        public int Hunger { get => hunger; }
        public int Stamina { get => stamina; }

        public Pet(Texture2D texture, Point segments, int frameTime) :base(texture, segments)
        {
            this.segments = segments;
            this.frameTime = frameTime;

            currentFrame = 0;
            position = new Vector2(0, 0);

            scale = new Vector2(7);

            origin = new Vector2(frameWidth / 2, frameHeight);

            speed = 0.5f;
            health = 10;
            hunger = 0;
            stamina = 10;
            petState = PetState.ALIVE;
        }

        public override void Update(GameTime time)
        {
            if(petState == PetState.ALIVE)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Z))
                    Feed();
                if (Keyboard.GetState().IsKeyDown(Keys.X))
                    Play();
                if (Keyboard.GetState().IsKeyDown(Keys.C))
                    Sleep();

                CheckStatus();

                Animate(time);

                Move(time);
            }

        }

        public void Feed()
        {
            if(petState == PetState.ALIVE)
            {
                if (hunger == 0)
                {
                    health--;
                }
                else
                {
                    hunger--;
                }
            }
        }

        public void Play()
        {
            if(petState == PetState.ALIVE)
            {
                if (stamina == 0)
                {
                    health--;
                    hunger++;
                }
                else
                {
                    stamina--;
                    hunger++;
                }
            }
        }

        public void Sleep()
        {
            if (petState == PetState.ALIVE)
            {
                stamina = 10;
                if (hunger < 10)
                {
                    health++;
                    hunger++;
                }
                else
                    health--;
            }
        }

        private void CheckStatus()
        {
            if(hunger > 10)
            {
                health -= 1;
                hunger = 10;
            }

            if(health < 0)
            {
                health = 0;
                petState = PetState.DEAD;
            }

            if (stamina > 10)
            {
                stamina = 10;
                health--;
            }
        }

        private void Move(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= speed * time.ElapsedGameTime.Milliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += speed * time.ElapsedGameTime.Milliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= speed * time.ElapsedGameTime.Milliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += speed * time.ElapsedGameTime.Milliseconds;
            }
        }

        private void Animate(GameTime time)
        {
            _timer += time.ElapsedGameTime.Milliseconds;
            if (_timer >= frameTime)
            {
                _timer -= frameTime;
                if (currentFrame == segments.X - 1)
                {
                    currentFrame = 0;
                }
                else
                {
                    currentFrame++;
                }

                frameRect = new Rectangle(frameWidth * currentFrame, 0, frameWidth, frameHeight);
            }
        }
    }
}
