using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace RuneWitch
{
    public class SplashscreenHandler
    {
        private List<Splashscreen> Splashscreens { get; set; }
        private float SplashTimer { get; set; }
        private int CurrentSplashscreen { get; set; }
        private float SplashAlpha { get; set; }

        private bool InputAllowed { get; set; }
        private float InputTimer { get; set; }

        public bool IsComplete { get; private set; }

        private SplashState State { get; set; }
        private enum SplashState
        {
            Begin,
            FadeIn,
            Hold,
            FadeOut,
            Idle,
            Complete,
            End
        }

        public SplashscreenHandler()
        {
            State = SplashState.Begin;
            Splashscreens = new List<Splashscreen>();
            SplashTimer = 0f;
            SplashAlpha = 0f;
            CurrentSplashscreen = 0;
            IsComplete = false;

            InputAllowed = true;
            InputTimer = 0.15f;
        }

        public void AddSplashscreen(Splashscreen splashscreen)
        {
            Splashscreens.Add(splashscreen.Clone());
        }
        private void CheckInput(GameTime gameTime)
        {
            GamePadState gpState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyState = Keyboard.GetState();

            if(gpState.IsButtonUp(Buttons.A) == true &&
                gpState.IsButtonUp(Buttons.Start) == true &&
                keyState.IsKeyUp(Keys.Enter) == true &&
                keyState.IsKeyUp(Keys.Space) == true)
            {
                InputTimer = 0.15f;
                InputAllowed = true;
            }

            if (InputAllowed == true && 
                (gpState.IsButtonDown(Buttons.A) == true ||
                gpState.IsButtonDown(Buttons.Start) == true ||
                keyState.IsKeyDown(Keys.Enter) == true ||
                keyState.IsKeyDown(Keys.Space) == true))
            {
                InputTimer = 0.15f;
                InputAllowed = false;
                
                SplashAlpha = 0f;
                SplashTimer = 0f;
                State = SplashState.Complete;
            }

            if(InputAllowed == false)
            {
                InputTimer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public void Update(GameTime gameTime)
        {
            CheckInput(gameTime);

            float _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Splashscreen _currentSplashscreen = null;

            if (CurrentSplashscreen != Splashscreens.Count())
            {
                _currentSplashscreen = Splashscreens.ElementAt(CurrentSplashscreen);
            }

            switch (State)
            {
                case SplashState.Begin:
                    {
                        foreach(SplashscreenAsset _asset in _currentSplashscreen.Assets)
                        {
                            if(_asset.Type == "SoundAsset")
                            {
                                SoundEffectInstance _playSound = _asset.Sound.CreateInstance();
                                _playSound.Volume = _asset.Volume;
                                _playSound.Play();
                            }

                            if(_asset.Type == "SongAsset")
                            {
                                MediaPlayer.Volume = _asset.Volume;
                                MediaPlayer.Play(_asset.Song);
                            }                        
                        }

                        State = SplashState.FadeIn;
                        break;
                    }
                case SplashState.FadeIn:
                    {
                        SplashTimer += _deltaTime;

                        if (SplashTimer < 1.0f)
                        {
                            SplashAlpha = SplashTimer;
                        }
                        else
                        {
                            SplashTimer = 2.5f;
                            SplashAlpha = 1.0f;
                            State = SplashState.Hold;
                        }

                        break;
                    }
                case SplashState.Hold:
                    {
                        SplashTimer -= _deltaTime;

                        if (SplashTimer <= 0f)
                        {
                            SplashTimer = 1f;
                            SplashAlpha = 1f;
                            State = SplashState.FadeOut;
                        }
                        else
                        {
                            SplashAlpha = 1.0f;
                        }

                        break;
                    }
                case SplashState.FadeOut:
                    {
                        SplashTimer -= _deltaTime;

                        if (SplashTimer > 0f)
                        {
                            SplashAlpha = SplashTimer;
                        }
                        else
                        {
                            SplashTimer = 1f;
                            SplashAlpha = 0f;
                            State = SplashState.Idle;
                        }

                        break;
                    }
                case SplashState.Idle:
                    {
                        SplashTimer -= _deltaTime;

                        if (SplashTimer > 0f)
                        {
                            SplashAlpha = 0f;
                        }
                        else
                        {
                            State = SplashState.Complete;
                        }

                        break;
                    }
                case SplashState.Complete:
                    {
                        CurrentSplashscreen += 1;

                        if(CurrentSplashscreen == Splashscreens.Count())
                        {
                            State = SplashState.End;
                        }
                        else
                        {
                            State = SplashState.Begin;
                        }

                        //MediaPlayer.Stop();

                        break;
                    }
                case SplashState.End:
                    {
                        IsComplete = true;
                        break;
                    }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(State != SplashState.End)
            {
                Splashscreen _currentSplashscreen = Splashscreens.ElementAt(CurrentSplashscreen);

                _currentSplashscreen.Draw(spriteBatch, SplashAlpha);
            }
        }
    }
}
