using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGame___Topic_3___Loops__Lists____Inputs
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        KeyboardState keyboardState;

        Rectangle window;

        Texture2D grassTexture;
        Texture2D mowerTexture;
        Rectangle mowerRect;

        SoundEffect mowerSound;
        SoundEffectInstance mowerSoundInstance;


        List<Rectangle> grassTiles;


        Vector2 mowerSpeed;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 600, 500);

            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();


            mowerRect = new Rectangle(100, 100, 30, 30);


            grassTiles = new List<Rectangle>();
            for (int x = 0; x < window.Width; x+= 5)
            {
                for (int y = 0; y < window.Height; y += 5) 
                {

                    grassTiles.Add(new Rectangle(x, y, 5, 5));
                
                }

            }


            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            mowerTexture = Content.Load<Texture2D>("Images/mower");
            grassTexture = Content.Load<Texture2D>("Images/long_grass");
            mowerSound = Content.Load<SoundEffect>("Audio/mower_sound");
            mowerSoundInstance = mowerSound.CreateInstance();
            mowerSoundInstance.IsLooped = true;


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();

            mowerSpeed = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W))
            {
                mowerSpeed.Y += -1;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                mowerSpeed.Y += 1;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                mowerSpeed.X += -1;
            }


            if (keyboardState.IsKeyDown(Keys.D))
            {
                mowerSpeed.X += 1;
            }

            mowerRect.Offset(mowerSpeed);


            if (mowerSpeed == Vector2.Zero)
            {
                mowerSoundInstance.Stop();

            }
            else
            {
                mowerSoundInstance.Play();
            }


            for (int i = 0; i < grassTiles.Count; i++)
            {
                if (mowerRect.Contains(grassTiles[i]))
                {

                    grassTiles.RemoveAt(i);
                    i--;

                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LawnGreen);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            foreach(Rectangle grass in grassTiles)
            {
                _spriteBatch.Draw(grassTexture, grass, Color.White);
            }

            _spriteBatch.Draw(mowerTexture, mowerRect, Color.White);



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
