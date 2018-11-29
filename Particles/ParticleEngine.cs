using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Particles
{
    public static class ParticleEngine
    {
        private static Pool<Particle> availableParticles;
        private static List<Particle> liveParticles;
        private static List<Particle> deadParticles;

        static ParticleEngine()
        {
            availableParticles = new Pool<Particle>(250, 150);
            liveParticles = new List<Particle>();
            deadParticles = new List<Particle>();
        }

        public static void AddParticle(Particle particle)
        {
            liveParticles.Add(particle);
        }

        public static Particle New()
        {
            var particle =  availableParticles.Get();
            AddParticle(particle);
            return particle;
        }

        public static void Update()
        {
            foreach(var particle in liveParticles)
            {
                particle.Update();
                if (particle.IsDead)
                    deadParticles.Add(particle);
            }

            foreach(var particle in deadParticles)
            {
                liveParticles.Remove(particle);
                availableParticles.Put(particle);
            }

            deadParticles.Clear();
        }

        public static void Render(SpriteBatch sb)
        {
            foreach(var particle in liveParticles)
            {
                particle.Render(sb);
            }
        }
    }
}
