using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Kinect;
using System.Diagnostics;

namespace Arkanoid
{
    class Arkanoid
    {
        private List<Block> blocks;
        private Ball ball;
        private Bar bar;
        private ContentManager c;
        private Vector2 barSize;
        private float ballRadius;
        private Vector2 blockSize;

        public Arkanoid(ContentManager c)
        {
            this.c = c;
        }

        public void Initialize()
        {
            ballRadius = 10;
            barSize = new Vector2(100, 10);
            blockSize = new Vector2(50, 10);
            ball=new Ball(ballRadius);
            bar=new Bar(barSize);
            blocks = new List<Block>();
            for(int i=0;i<20;i++)
            {
                blocks.Add(new Block(new Vector2(blockSize.X * i + 512-10*blockSize.X, 100),blockSize));
            }
        }

        public void LoadContent()
        {
            this.ball.LoadContent(c,"ball");
            foreach (Block b in blocks)
                b.LoadContent(c, "block");
            bar.LoadContent(c, "bar");
        }

        public void Update(GameTime gameTime, Vector2 rightHandPos)
        {
            bar.Position = new Vector2(rightHandPos.X, 500);
            if (ball.Position.X < ball.Radius || ball.Position.X>1024-ball.Radius)
            {
                ball.Velocity = new Vector2(-ball.Velocity.X,ball.Velocity.Y);
            }

            if (ball.Position.Y < ball.Radius || ball.Position.Y > 768 - ball.Radius)
            {
                ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
            }

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
            spriteBatch.Draw(ball.Texture, ball.Position, null, Color.White, 0,new Vector2(ball.Radius, ball.Radius),2*ball.Radius/ball.Texture.Width,SpriteEffects.None, 0);
            spriteBatch.Draw(bar.Texture, bar.Position, null, Color.White, 0,new Vector2(bar.Size.X / 2, bar.Size.Y / 2), new Vector2(bar.Size.X/bar.Texture.Width,bar.Size.Y/bar.Texture.Height), SpriteEffects.None, 0);
            foreach(Block b in blocks)
                spriteBatch.Draw(b.Texture, b.Position, null, Color.White, 0,new Vector2(b.Size.X / 2, b.Size.Y / 2), new Vector2(b.Size.X / b.Texture.Width, b.Size.Y / b.Texture.Height), SpriteEffects.None, 0);
        }
    }
}
