using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace RuneWitch
{
    public class MainMenuHandler
    {
        private AnimationReader AnimationReader { get; set; }
        private AnimationBook Book { get; set; }

        private Texture2D BackTexture { get; set; }
        private Texture2D TitleTexture { get; set; }
        private Texture2D SelectorTexture { get; set; }
        private SpriteFont ItemFont { get; set; }
        private List<MainMenuItem> Items { get; set; }
        private Song MenuSong { get; set; }

        private int CurrentSelection { get; set; }
        private bool InputMoveAllowed { get; set; }
        private bool InputSelectAllowed { get; set; }
        private float InputMoveTimer { get; set; }
        private float InputSelectTimer { get; set; }

        public bool IsActive { get; private set; }

        public MainMenuHandler(ContentManager content)
        {
            BackTexture = content.Load<Texture2D>("MainMenuBackground");
            TitleTexture = content.Load<Texture2D>("MainMenuTitle");
            SelectorTexture = content.Load<Texture2D>("TriskSelector");
            ItemFont = content.Load<SpriteFont>("MainFont");
            MenuSong = content.Load<Song>("Brittle Rille");
            Items = new List<MainMenuItem>();

            CurrentSelection = 0;
            InputMoveAllowed = true;
            InputSelectAllowed = true;
            InputMoveTimer = 0.15f;
            InputSelectTimer = 0.15f;
        }

        public void Activate()
        {
            //MediaPlayer.Volume = 1f;
            //MediaPlayer.Stop();
            //MediaPlayer.Play(MenuSong);

            SetUpMenuItems();

            Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();
            Animation _selector = new Animation(SelectorTexture, 5, 64, 64, 0.1f);
            _animations.Add("Selector", _selector);
            Book = new AnimationBook("SelectorBook", _animations);
            AnimationReader = new AnimationReader(Book);

            AnimationReader.Play("Selector");

            IsActive = true;
        }
        private void SetUpMenuItems()
        {
            MainMenuItem _newGame = new MainMenuItem("New Game", ItemFont, new Vector2(640, 512), Toolbox.FontAlignment.Center);
            MainMenuItem _continue = new MainMenuItem("Continue", ItemFont, new Vector2(640, 558), Toolbox.FontAlignment.Center);
            MainMenuItem _options = new MainMenuItem("Options", ItemFont, new Vector2(640, 604), Toolbox.FontAlignment.Center);
            MainMenuItem _exit = new MainMenuItem("Exit", ItemFont, new Vector2(640, 650), Toolbox.FontAlignment.Center);

            Items.Add(_newGame);
            Items.Add(_continue);
            Items.Add(_options);
            Items.Add(_exit);
        }

        public void HandleInput(GameTime gameTime)
        {
            float _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            InputMoveTimer -= _deltaTime;
            InputSelectTimer -= _deltaTime;

            if(InputMoveTimer < 0)
            {
                InputMoveTimer = 0f;
                InputMoveAllowed = true;
            }

            if (InputSelectTimer < 0)
            {
                InputSelectTimer = 0f;
                InputSelectAllowed = true;
            }

            if (InputMoveAllowed == true)
            {
                GamePadState gpState = GamePad.GetState(PlayerIndex.One);
                KeyboardState keyState = Keyboard.GetState();

                if ((gpState.ThumbSticks.Left.Y >= 0.15f) || 
                    gpState.IsButtonDown(Buttons.DPadDown) == true ||
                    keyState.IsKeyDown(Keys.Down) == true)
                {
                    // Move Down Selection
                    InputMoveAllowed = false;
                    InputMoveTimer = 0.1f;
                    
                    if(CurrentSelection == Items.Count() - 1)
                    {
                        CurrentSelection = 0;
                    }
                    else
                    {
                        CurrentSelection += 1;
                    }
                }

                if ((gpState.ThumbSticks.Left.Y <= -0.15f) || 
                    gpState.IsButtonDown(Buttons.DPadUp) == true ||
                    keyState.IsKeyDown(Keys.Up) == true)
                {
                    // Move Up Selection
                    InputMoveAllowed = false;
                    InputMoveTimer = 0.1f;

                    if (CurrentSelection == 0)
                    {
                        CurrentSelection = Items.Count() - 1;
                    }
                    else
                    {
                        CurrentSelection -= 1;
                    }
                }
            }
            else
            {
                GamePadState gpState = GamePad.GetState(PlayerIndex.One);
                KeyboardState keyState = Keyboard.GetState();

                if ((gpState.ThumbSticks.Left.Y <= 0.15f) &&
                    (gpState.ThumbSticks.Left.Y >= -0.15f) &&
                    gpState.IsButtonUp(Buttons.DPadDown) == true &&
                    gpState.IsButtonUp(Buttons.DPadUp) == true &&
                    keyState.IsKeyUp(Keys.Up) == true &&
                    keyState.IsKeyUp(Keys.Down) == true)
                {
                    InputMoveAllowed = true;
                    InputMoveTimer = 0.25f;
                }
            }


        }
        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            AnimationReader.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackTexture, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.Draw(TitleTexture, new Rectangle(0, 0, 1280, TitleTexture.Height), Color.White);

            if(Items.Count() > 0)
            {
                MainMenuItem _currentItem = Items.ElementAt(CurrentSelection);
                Rectangle _leftTrisk, _rightTrisk;

                _leftTrisk = new Rectangle((int)_currentItem.Position.X - 42, 
                    (int)_currentItem.Position.Y + (int)_currentItem.GetMeasureString().Y / 4, 32, 32);
                _rightTrisk = new Rectangle((int)_currentItem.Position.X + (int)_currentItem.GetMeasureString().X + 8, 
                    (int)_currentItem.Position.Y + (int)_currentItem.GetMeasureString().Y / 4, 32, 32);

                AnimationReader.Draw(spriteBatch, _leftTrisk);
                AnimationReader.Draw(spriteBatch, _rightTrisk);

                foreach (MainMenuItem _item in Items)
                {
                    _item.Draw(spriteBatch);
                }
            }
        }
    }
}
