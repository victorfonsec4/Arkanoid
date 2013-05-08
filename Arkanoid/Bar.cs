using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Arkanoid
{
    class Bar
    {
        private Vector2 position;
        private Vector2 size;
        private Texture2D texture;

        public Bar(Vector2 size)
        {
            this.size = size;
            this.position = new Vector2(512, 700);
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Vector2 Size
        {
            get
            {
                return size;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }

        public void LoadContent(ContentManager c,string s)
        {
            this.texture = c.Load<Texture2D>(s);
        }
    }
}
