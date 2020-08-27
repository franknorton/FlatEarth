using FlatEarth.Collision;
using FlatEarth.Collision.Colliders;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlatEarth.ECS
{
    public class Entity
    {
        private List<Component> components = new List<Component>();
        private List<Component> addComponents = new List<Component>();
        private List<Component> removeComponents = new List<Component>();

        public bool destroy;

        private Collider collider;
        public Collider Collider
        {
            get => collider; set
            {
                collider = value;
            }
        }
        public Scene Scene;

        public Vector2 Position;
        public Vector2 DisplayPosition;

        public Entity() { }

        public virtual void Move(float x, float y)
        {
            Position.X += x;
            Position.Y += y;
            DisplayPosition.X += x;
            DisplayPosition.Y += y;
        }

        public virtual void SetPosition(Vector2 position)
        {
            Position = position;
            DisplayPosition = position;
        }
        public virtual void Move(Vector2 movement)
        {
            Position += movement;
            DisplayPosition += movement;
        }

        public virtual void Update()
        {
            if (destroy)
            {
                components.Clear();
                addComponents.Clear();
                removeComponents.Clear();

                collider = null;
                Scene.Remove(this);
                return;
            }

            addComponents.ForEach(c => components.Add(c));
            addComponents.Clear();
            removeComponents.ForEach(c => components.Remove(c));
            removeComponents.Clear();
            components.ForEach(c => c.Update());
        }
        public virtual void Render()
        {
            components.ForEach(c => c.Render());
        }
        public virtual void DebugRender()
        {
            DebugDraw.Circle(Position, 1, Color.Black);
            components.ForEach(c => c.DebugRender());

        }
        public virtual void Destroy()
        {
            destroy = true;

        }

        public virtual void OnAdded(Scene scene)
        {
            Scene = scene;
        }
        public virtual void OnCollision(Entity entity)
        {
            components.ForEach(c => c.OnCollision(entity));
        }

        #region Components

        public void AddComponent(Component component)
        {
            addComponents.Add(component);
            component.OnAdded(this);
        }

        public void RemoveComponent(Component component)
        {
            removeComponents.Add(component);
        }

        public void RemoveComponent(Type componentType)
        {
            var component = components.FirstOrDefault(c => c.GetType() == componentType);
        }

        public void RemoveComponent<T>()
        {
            var component = components.FirstOrDefault(c => c is T);
            if (component != null)
                removeComponents.Add(component);
        }

        public T GetComponent<T>() where T : Component
        {
            var component = components.FirstOrDefault(c => c is T) as T;
            if (component == null)
                component = addComponents.FirstOrDefault(x => x is T) as T;
            return component;
        }

        public T GetComponent<T>(string tag) where T : Component
        {
            return components.FirstOrDefault(c => c is T && c.Tag.Equals(tag)) as T;
        }

        public IEnumerable<T> GetAllComponents<T>() where T : Component
        {
            return components.Where(c => c is T).Select(c => c as T);
        }
        #endregion

    }
}
