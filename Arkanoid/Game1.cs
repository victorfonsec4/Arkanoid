using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;
using System.Diagnostics;

namespace Arkanoid
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KinectSensor kinect;
        Skeleton[] skeletonData;
        Skeleton skeleton;
        Texture2D colorVideo;
        Arkanoid arkanoid;
        Joint rightHand;
        Vector2 rightHandPos;
        int height = 768;
        int width = 1024;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            kinect = KinectSensor.KinectSensors[0];
            kinect.Start();
            kinect.SkeletonStream.Enable();
            kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            kinect.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(kinect_AllFramesReady);
            arkanoid = new Arkanoid(Content);
            arkanoid.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            colorVideo = Content.Load<Texture2D>("fundo");
            arkanoid.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //*Debug.WriteLine(rightHand.Position.X);
            rightHandPos = new Vector2( ( (2*rightHand.Position.X) + 0.1f)*640, ( (0.5f*rightHand.Position.Y) + 0.5f) * 480);
            arkanoid.Update(gameTime, rightHandPos);
            //Debug.WriteLine(rightHandPos.X + " " + rightHand.Position.X);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawSkeleton(spriteBatch, new Vector2(640, 480), colorVideo);
            arkanoid.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        void kinect_AllFramesReady(object sender, AllFramesReadyEventArgs imageFrames)
        {
            SkeletonFrame skeletonFrame = imageFrames.OpenSkeletonFrame();
            if (skeletonFrame != null)
            {
                if ((skeletonData == null) || (this.skeletonData.Length != skeletonFrame.SkeletonArrayLength))
                {
                    this.skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                }
                skeletonFrame.CopySkeletonDataTo(this.skeletonData);
            }
            if (skeletonData != null)
            {
                foreach (Skeleton skel in skeletonData)
                {
                    if (skel.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        rightHand = skel.Joints[JointType.HandRight];
                        Debug.WriteLine(rightHand.Position.X);
                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            skeleton = skel;
                        }
                    }
                }
            }

        }

        private void DrawSkeleton(SpriteBatch spriteBatch, Vector2 resolution, Texture2D img)
        {
            if (skeleton != null)
            {
                foreach (Joint joint in skeleton.Joints)
                {
                    Vector2 position = new Vector2((((0.5f * joint.Position.X) + 0.5f) * resolution.X),
                                                   (((-0.5f * joint.Position.Y) + 0.5f) * resolution.Y));
                    spriteBatch.Draw(img, new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), 10, 10), Color.Red);
                }
            }
        }
    }
}
