using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CyberPet
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private List<Entity> entities;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            entities = new List<Entity>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Pet pet = new Pet(Content.Load<Texture2D>("Sit_19x15x8"), new Point(8, 1), 100);
            pet.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            HealthBar healthBar = new HealthBar(Content, pet);
            healthBar.Position = Vector2.Zero;

            StatusBar hungerBar = new HungerBar(Content, pet);
            hungerBar.Position = new Vector2(0, 22);

            StatusBar staminaBar = new StaminaBar(Content, pet);
            staminaBar.Position = new Vector2(0, 44);

            Button buttonFeed = new Button(Content, "Feed", pet.Feed);
            buttonFeed.Position = new Vector2(0, _graphics.PreferredBackBufferHeight - buttonFeed.Height);

            Button buttonPlay = new Button(Content, "Play", pet.Play);
            buttonPlay.Position = new Vector2((_graphics.PreferredBackBufferWidth - buttonPlay.Width) / 2, _graphics.PreferredBackBufferHeight - buttonPlay.Height);

            Button buttonSleep = new Button(Content, "Sleep", pet.Sleep);
            buttonSleep.Position = new Vector2(_graphics.PreferredBackBufferWidth - buttonSleep.Width, _graphics.PreferredBackBufferHeight - buttonSleep.Height);

            entities.Add(pet);
            entities.Add(healthBar);
            entities.Add(hungerBar);
            entities.Add(staminaBar);
            entities.Add(buttonFeed);
            entities.Add(buttonPlay);
            entities.Add(buttonSleep);
        }

        protected override void Update(GameTime gameTime)
        {
            ProceedInput(gameTime);

            entities.ForEach(delegate (Entity entity)
            {
                entity.Update(gameTime);

            });

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(41, 54, 66));

            entities.ForEach(delegate (Entity entity)
            {
                entity.Draw(_spriteBatch);

            });

            base.Draw(gameTime);
        }

        private void ProceedInput(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }
    }
}
