using FlatEarth.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Particles
{
    public class Particle
    {
        public FETexture Texture;
        public bool IsDead = false;
        private float ElapsedLifetime;
        public float Lifetime;

        public Vector2 Position;
        public Color Color;

        public void Update()
        {
            ElapsedLifetime += Engine.DeltaTime;
            if(ElapsedLifetime >= Lifetime)
                IsDead = true;

            if(!IsDead)
            {
                var percentDone = ElapsedLifetime / Lifetime;
            }
        }

        public void Render(SpriteBatch sb)
        {
            Texture.Draw(sb, Position, Color);
        }
    }
}
