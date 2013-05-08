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
            this.velocity = new Vector2(0, -150);
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
                velocity.Y = -velocity.Y;
            else
                velocity.X = -velocity.X;
        }

        public void BarImpact(Vector2 barPos, Vector2 barSize)
        {
            if (position.X >= barPos.X - barSize.X / 2 && position.X <= barPos.X + barSize.X / 2)
            {
                velocity.Y = -velocity.Y;
                velocity.X = (position.X - barPos.X) / 10;
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
