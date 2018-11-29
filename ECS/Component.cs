using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.ECS
{
    public abstract class Component
    {
        public Entity Entity { get; set; }
        public string Tag { get; set; }

        public Component() { }
        public virtual void OnAdded(Entity entity)
        {
            Entity = entity;
        }
        public virtual void OnRemoved(Entity entity)
        {
            Entity = null;
        }
        public virtual void OnCollision(Entity entity) { }
        public virtual void Update() { }
        public virtual void Render() { }
        public virtual void DebugRender() { }
    }
}
