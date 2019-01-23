using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace RuneWitch
{
    public class Splashscreen
    {
        public List<SplashscreenAsset> Assets { get; private set; }
        private float SplashTimer { get; set; }
        private int CurrentSplashscreen { get; set; }

        public Splashscreen()
        {
            Assets = new List<SplashscreenAsset>();
        }
        public Splashscreen(List<SplashscreenAsset> assets)
        {
            Assets = new List<SplashscreenAsset>();

            foreach(SplashscreenAsset _asset in assets)
            {
                Assets.Add(_asset.Clone());
            }
        }

        public void AddAsset(Texture2D texture, Vector2 position, int width, int height)
        {
            SplashscreenAsset _asset = new SplashscreenAsset(texture, position, width, height);
            Assets.Add(_asset);
        }
        public void AddAsset(string text, SpriteFont font, Vector2 position, Toolbox.FontAlignment alignment)
        {
            SplashscreenAsset _asset = new SplashscreenAsset(text, font, position, alignment);
            Assets.Add(_asset);
        }
        public void AddAsset(Song song, float volume)
        {
            SplashscreenAsset _asset = new SplashscreenAsset(song, volume);
            Assets.Add(_asset);
        }

        public void Draw(SpriteBatch spriteBatch, float alpha)
        {
            foreach(SplashscreenAsset _asset in Assets)
            {
                _asset.Draw(spriteBatch, alpha);
            }
        }

        public Splashscreen Clone()
        {
            return new Splashscreen(Assets);
        }
    }
}
