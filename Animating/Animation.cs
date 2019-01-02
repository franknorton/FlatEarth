using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Animating
{
    class Animation
    {
        public string Name { get; set; }

        public float incrementer = 0;
        public int msPerFrame = 120;
        public int CurrentFrameNumber = 0;
        public List<FETexture> Frames;
        public FETexture CurrentFrame { get { return Frames[CurrentFrameNumber]; } }
        public bool Loop;
        public bool Running;


        public Animation() { }
        public Animation(List<FETexture> frames)
        {
            Frames = frames;
        }

        public void Update(GameTime gameTime)
        {
            if (Running)
            {
                incrementer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (incrementer >= msPerFrame)
                {
                    CurrentFrameNumber++;
                    incrementer = 0;
                }

                if (CurrentFrameNumber >= Frames.Count)
                {
                    if (Loop)
                        CurrentFrameNumber = 0;
                    else
                        Running = false;
                }
            }
        }
    }
}
