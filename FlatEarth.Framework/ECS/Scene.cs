using FlatEarth.Collision;
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

        /// <summary>
        /// Checks if an entity collides with any other object in the scene.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public bool CollisionCheck(Entity a, out List<Entity> b)
        {
            b = new List<Entity>();
            foreach (var entity in Entities)
            {
                if (!entity.destroy && a != entity && a.Collider.Active && entity.Collider.Active && a.Collider.Collide(entity))
                {
                    b.Add(entity);
                }
            }
            return b.Count > 0;
        }

        /// <summary>
        /// Checks if an entity collides with another specific entity.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool CollisionCheck(Entity a, Entity b)
        {
            return a.Collider.Collide(b);
        }
        public bool CollisionCheck(RectCollider area, out List<Entity> e)
        {
            var collidedWith = new List<Entity>();
            e = collidedWith;
            foreach (var entity in Entities)
            {
                if (!entity.destroy && entity.Collider.Active && entity.Collider.Collide(area))
                {
                    collidedWith.Add(entity);
                }
            }

            return collidedWith.Count > 0;
        }
        public bool CollisionCheck(CircleCollider area, out List<Entity> e)
        {
            var collidedWith = new List<Entity>();
            e = collidedWith;
            foreach (var entity in Entities)
            {
                if (!entity.destroy && entity.Collider.Active && entity.Collider.Collide(area))
                {
                    collidedWith.Add(entity);
                }
            }
            return collidedWith.Count > 0;
        }
    }
}
