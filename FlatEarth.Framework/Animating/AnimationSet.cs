using FlatEarth;
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
    class AnimationSet
    {
        public string Name { get; set; }
        public string SpriteSheetName { get; set; }
        public Dictionary<string, List<Rectangle>> Animations { get; set; }
        public Animation GetAnimationByName(string name)
        {
            var spriteSheet = Content.Load<Texture2D>(SpriteSheetName);
            var frames = Animations[name].Select(x => new FETexture(spriteSheet, x)).ToList();
            return new Animation(frames);
        }
    }
}
