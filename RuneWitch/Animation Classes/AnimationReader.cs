using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RuneWitch
{
    public class AnimationNotFoundException : Exception
    {
        public AnimationNotFoundException(string referenceName) : base(String.Format("Invalid Animation Reference Name: {0}", referenceName))
        {

        }
    }

    public class AnimationReader
    {
        private Animation Animation { get; set; }
        private AnimationBook Book { get; set; }
        private float FrameTimer { get; set; }

        public AnimationReader(Animation animation)
        {
            Animation = animation;
        }

        public AnimationReader(AnimationBook animationBook)
        {
            Book = new AnimationBook(animationBook);
        }

        public void Play(string animationReference)
        {
            try
            {
                Animation _animation = Book.GetAnimation(animationReference);

                if (_animation == null)
                {
                    throw new AnimationNotFoundException(animationReference);
                }

                if (Animation == _animation)
                {
                    return;
                }

                Animation = _animation;
                Animation.CurrentFrame = 0;
                FrameTimer = 0;
            }
            catch (AnimationNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Stop()
        {
            FrameTimer = 0;
            Animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            FrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (FrameTimer >= Animation.FrameSpeed)
            {
                FrameTimer = 0f;
                Animation.GotoNextFrame();
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Animation.Texture, position,
                new Rectangle((int)Animation.FrameStartPosition.X + (Animation.CurrentFrame * Animation.FrameWidth),
                (int)Animation.FrameStartPosition.Y, Animation.FrameWidth, Animation.FrameHeight),
                Color.White);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            spriteBatch.Draw(Animation.Texture, rectangle,
                new Rectangle((int)Animation.FrameStartPosition.X + (Animation.CurrentFrame * Animation.FrameWidth),
                (int)Animation.FrameStartPosition.Y, Animation.FrameWidth, Animation.FrameHeight),
                Color.White);
        }
    }
}
