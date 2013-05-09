using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Arkanoid
{
    class Ball
    {
        private Vector2 position;
        private float radius;
        private Vector2 velocity;
        private Texture2D texture;

        public Ball(float radius)
        {
            this.radius = radius;
            this.position = new Vector2(512,500);
            this.velocity = new Vector2(0, -200);
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                this.position = value;
            }
        }

        public float Radius
        {
            get
            {
                return radius;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }

        public void BlockImpact(Vector2 blockPos, Vector2 blockSize)
        {
            if (position.X >= blockPos.X - blockSize.X / 2 && position.X <= blockPos.X + blockSize.X / 2)
            {
                velocity.Y = -velocity.Y;
                if (position.Y > blockPos.Y)
                    position.Y = blockPos.Y + blockSize.Y / 2 + radius;
                else
                    position.Y = blockPos.Y - blockSize.Y / 2 - radius;
            }
            else
                velocity.X = -velocity.X;
        }

        public void BarImpact(Vector2 barPos, Vector2 barSize)
        {
            if (position.X >= barPos.X - barSize.X / 2 && position.X <= barPos.X + barSize.X / 2)
            {
                float modulo = (velocity.X*velocity.X + velocity.Y* velocity.Y);
                velocity.X = (position.X - barPos.X) * 10;
                velocity.Y = -(float)Math.Sqrt(Math.Abs(modulo - velocity.X * velocity.X) );
            }
            else
                velocity.X = -velocity.X;
        }

        public void LoadContent(ContentManager c, string s)
        {
            this.texture = c.Load<Texture2D>(s);
        }

    }
}
