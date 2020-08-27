using FlatEarth.Collision;
using FlatEarth.Collision.Colliders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.ECS
{
    public class Scene
    {
        public List<Entity> Entities;
        private List<Entity> RemoveEntities;
        private List<Entity> AddedEntities;

        public Scene()
        {
            Entities = new List<Entity>();
            RemoveEntities = new List<Entity>();
            AddedEntities = new List<Entity>();
        }

        public void Add(Entity entity)
        {
            AddedEntities.Add(entity);
            entity.OnAdded(this);
        }

        public void Remove(Entity entity)
        {
            RemoveEntities.Add(entity);
        }

        public void Update()
        {
            AddedEntities.ForEach(e => Entities.Add(e));
            AddedEntities.Clear();
            Entities.ForEach(e => e.Update());
            RemoveEntities.ForEach(e => Entities.Remove(e));
            RemoveEntities.Clear();
        }

        public void Render()
        {
            Entities.ForEach(e => e.Render());
        }

        public void DebugRender()
        {
            Entities.ForEach(e => e.DebugRender());
        }
    }
}
