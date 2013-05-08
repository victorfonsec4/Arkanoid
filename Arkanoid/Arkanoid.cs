using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Arkanoid
{
    class Arkanoid
    {
        private List<Block> blocks;
        private Ball ball;
        private Bar bar;
        private ContentManager c;

        public Arkanoid(ContentManager c)
        {
            this.c = c;
        }

        public void Initialize()
        {
            ball=new Ball();
            bar=new Bar();
            for(int i=0;i<20;i++)
            {
                blocks.Add(new Block(new Vector2(100,20*i+322)));
            }
        }

        public void LoadContent()
        {
            ball.LoadContent(c,"ball");
            foreach (Block b in blocks)
                b.LoadContent(c, "block");
            bar.LoadContent(c, "bar");
        }

        public void Update(GameTime gameTime)
        {
            foreach (Block b in blocks)
            {
                if (Math.Abs(ball.Position.X - b.Position.X) <= ball.Radius + b.Size.X / 2 && Math.Abs(ball.Position.Y - b.Position.Y) <= ball.Radius + b.Size.Y / 2)
                {
                    ball.BlockImpact(b.Position, b.Size);
                    blocks.Remove(b);
                    break;
                }
            }

            if (Math.Abs(ball.Position.X - bar.Position.X) <= ball.Radius + bar.Size.X / 2 && Math.Abs(ball.Position.Y - bar.Position.Y) <= ball.Radius + bar.Size.Y / 2)
            {
                ball.BarImpact(bar.Position, bar.Size);
            }

            ball.Position = ball.Position + (float)gameTime.ElapsedGameTime.TotalSeconds * ball.Velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ball.Texture, ball.Position, null, Color.White, 0, ball.Position - new Vector2(ball.Radius, ball.Radius),2*ball.Radius/ball.Texture.Width,SpriteEffects.None, 0);
            spriteBatch.Draw(bar.Texture, bar.Position, null, Color.White, 0, bar.Position - new Vector2(bar.Size.X / 2, bar.Size.Y / 2), new Vector2(bar.Size.X/bar.Texture.Width,bar.Size.Y/bar.Texture.Height), SpriteEffects.None, 0);
            foreach(Block b in blocks)
                spriteBatch.Draw(b.Texture, b.Position, null, Color.White, 0, b.Position - new Vector2(b.Size.X / 2, b.Size.Y / 2), new Vector2(b.Size.X / b.Texture.Width, b.Size.Y / b.Texture.Height), SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
