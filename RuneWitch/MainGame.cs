using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RuneWitch
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SplashscreenHandler SplashHandler;
        MainMenuHandler MainMenuHandler;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SplashHandler = new SplashscreenHandler();
            MainMenuHandler = new MainMenuHandler(Content);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MediaPlayer.Play(Content.Load<Song>("Brittle Rille"));

            Texture2D _logo1 = Content.Load<Texture2D>("Max");
            Texture2D _logo2 = Content.Load<Texture2D>("Athena");

            //Song _logo2sfx = Content.Load<Song>("Logo2SFX");

            Splashscreen _screen1 = new Splashscreen();
            Splashscreen _screen2 = new Splashscreen();

            _screen1.AddAsset(_logo1, new Vector2(graphics.PreferredBackBufferWidth / 2 - _logo1.Width / 2, 64), _logo1.Width, _logo1.Height);
            _screen1.AddAsset("Programming by Max Courneya", Content.Load<SpriteFont>("MainFont"),
                new Vector2(graphics.PreferredBackBufferWidth / 2, 96 + _logo1.Height), Toolbox.FontAlignment.Center);

            _screen2.AddAsset(_logo2, new Vector2(graphics.PreferredBackBufferWidth / 2 - _logo2.Width / 2, 64), _logo2.Width, _logo2.Height);
            _screen2.AddAsset("Art by Athena Pitkin", Content.Load<SpriteFont>("MainFont"),
                new Vector2(graphics.PreferredBackBufferWidth / 2, 96 + _logo1.Height), Toolbox.FontAlignment.Center);

            SplashHandler.AddSplashscreen(_screen1);
            SplashHandler.AddSplashscreen(_screen2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(SplashHandler.IsComplete == false)
            {
                SplashHandler.Update(gameTime);
            }
            else
            {
                if(MainMenuHandler.IsActive == false)
                {
                    MainMenuHandler.Activate();
                }
                else
                {
                    MainMenuHandler.Update(gameTime);
                }
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (SplashHandler.IsComplete == false)
            {
                SplashHandler.Draw(spriteBatch);
            }
            else
            {
                if (MainMenuHandler.IsActive == true)
                {
                    MainMenuHandler.Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
