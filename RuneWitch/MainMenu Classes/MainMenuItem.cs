using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneWitch
{
    public class MainMenuItem
    {
        public string Text { get; private set; }
        private SpriteFont Font { get; set; }

        public Vector2 Position { get; private set; }

        private Texture2D Texture { get; set; }
        private Rectangle TextureRectangle { get; set; }

        public MainMenuItem(string text, SpriteFont font, Vector2 position, Toolbox.FontAlignment alignment)
        {
            Text = text;
            Font = font;

            Position = Toolbox.AdjustTextPosition(position, Text, Font, alignment);
        }
        public MainMenuItem(Texture2D texture, Vector2 position, int width, int height)
        {
            Texture = texture;
            Position = position;


        }

        public Vector2 GetMeasureString()
        {
            return Font.MeasureString(Text);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Toolbox.DrawTextOutlined(spriteBatch, Font, Text, Color.Black, Color.Yellow, 1f, Position);
        }
    }
}
