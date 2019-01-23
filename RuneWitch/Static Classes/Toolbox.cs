using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneWitch
{
    public static class Toolbox
    {
        public enum FontAlignment
        {
            Left,
            Right,
            Center
        }

        public static Vector2 AdjustTextPosition(Vector2 originalPosition, string text, SpriteFont font, FontAlignment alignment)
        {
            float _adjustedX, _adjustedY;
            Vector2 _measuredString = font.MeasureString(text);

            switch (alignment)
            {
                case FontAlignment.Center:
                    {
                        _adjustedX = originalPosition.X - (0.5f * _measuredString.X);
                        break;
                    }
                case FontAlignment.Left:
                    {
                        _adjustedX = originalPosition.X;
                        break;
                    }
                case FontAlignment.Right:
                    {
                        _adjustedX = originalPosition.X - (_measuredString.X);
                        break;
                    }
                default:
                    {
                        _adjustedX = 0;
                        break;
                    }
            }

            _adjustedY = originalPosition.Y - (0.5f * _measuredString.Y);

            return new Vector2(_adjustedX, _adjustedY);
        }
        public static void DrawTextOutlined(SpriteBatch spriteBatch, SpriteFont font, string text, Color backColor, Color frontColor, float scale, Vector2 position)
        {
            Vector2 origin = Vector2.Zero;

            spriteBatch.DrawString(font, text, position + new Vector2(1 * scale, 1 * scale), backColor, 0, origin, scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, position + new Vector2(-1 * scale, 1 * scale), backColor, 0, origin, scale, SpriteEffects.None, 1f);
            //spriteBatch.DrawString(font, text, position + new Vector2(-1 * scale, -1 * scale), backColor, 0, origin, scale, SpriteEffects.None, 1f);
            //spriteBatch.DrawString(font, text, position + new Vector2(1 * scale, -1 * scale), backColor, 0, origin, scale, SpriteEffects.None, 1f);          

            spriteBatch.DrawString(font, text, position, frontColor, 0, origin, scale, SpriteEffects.None, 0f);
        }
    }
}
