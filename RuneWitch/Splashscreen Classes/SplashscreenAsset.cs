using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace RuneWitch
{
    public class SplashscreenAsset
    {
        public string Type { get; private set; }
        private string SplashText { get; set; }
        private SpriteFont Font { get; set; }

        private Texture2D Texture { get; set; }
        private Rectangle TextureRectangle { get; set; }

        private Vector2 Position { get; set; }

        public SoundEffect Sound { get; private set; }
        public Song Song { get; private set; }
        public float Volume { get; private set; }

        public SplashscreenAsset(string text, SpriteFont font, Vector2 position, Toolbox.FontAlignment alignment)
        {
            Type = "TextAsset";

            SplashText = text;
            Font = font;

            Position = Toolbox.AdjustTextPosition(position, SplashText, Font, alignment);
        }
        public SplashscreenAsset(Texture2D texture, Vector2 position, int width, int height)
        {
            Type = "TextureAsset";

            Texture = texture;
            Position = position;

            TextureRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);
        }
        public SplashscreenAsset(SoundEffect soundeffect, float volume)
        {
            Type = "SoundAsset";

            Sound = soundeffect;
            Volume = volume;
        }
        public SplashscreenAsset(Song song, float volume)
        {
            Type = "SongAsset";

            Song = song;
            Volume = volume;
        }

        public void Draw(SpriteBatch spriteBatch, float alpha)
        {
            switch (Type)
            {
                case "TextAsset":
                    {
                        spriteBatch.DrawString(Font, SplashText, Position, Color.White * alpha);
                        break;
                    }
                case "TextureAsset":
                    {
                        spriteBatch.Draw(Texture, TextureRectangle, Color.White * alpha);
                        break;
                    }
            }
        }

        public SplashscreenAsset Clone()
        {
            switch (Type)
            {
                case "TextAsset":
                    {
                        return new SplashscreenAsset(SplashText, Font, Position, Toolbox.FontAlignment.Left);
                    }
                case "TextureAsset":
                    {
                        return new SplashscreenAsset(Texture, new Vector2(TextureRectangle.X, TextureRectangle.Y), TextureRectangle.Width, TextureRectangle.Height);
                    }
                case "SoundAsset":
                    {
                        return new SplashscreenAsset(Sound, Volume);
                    }
                case "SongAsset":
                    {
                        return new SplashscreenAsset(Song, Volume);
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
