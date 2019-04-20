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

        public override bool Collide(CircleCollider circle) {
            foreach(var c in colliders) {
                if (c.Collide(circle))
                    return true;
            }
            return false;
        }

        public override bool Collide(RectCollider rect) {
            foreach(var c in colliders) {
                if (c.Collide(rect))
                    return true;
            }
            return false;
        }

        public override bool Collide(GridCollider grid) {
            foreach(var c in colliders) {
                if (c.Collide(grid))
                    return true;
            }
            return false;
        }

        public override void DebugRender()
        {
            colliders.ForEach(x => x.DebugRender());
        }
    }
}
