using FlatEarth.Collision.Colliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision
{
    public class ColliderList : Collider
    {
        public List<Collider> colliders;

        public ColliderList() { }

        public void Add(params Collider[] colliders) {
            this.colliders.AddRange(colliders);
        }

        public void Remove(params Collider[] colliders) {
            foreach(var collider in colliders) {
                this.colliders.Remove(collider);
            }
        }
    }
}
