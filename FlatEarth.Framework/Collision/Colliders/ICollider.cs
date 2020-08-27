using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision.Colliders
{
    public interface ICollider
    {
        Vector2 Position { get; set; }
        bool IsTrigger { get; set; }
    }
}
