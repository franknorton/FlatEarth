using System;
using System.Collections.Generic;

namespace FlatEarth
{
    public class Pool<T>
    {
        private Queue<T> pool = new Queue<T>();
        private int increaseSize;
        private object[] parameters;

        public Pool(int initialSize, int increaseSize, params object[] parameters)
        {
            this.parameters = parameters;
            this.increaseSize = increaseSize;
            for (int i = 0; i < initialSize; i++)
            {
                pool.Enqueue((T)Activator.CreateInstance(typeof(T), parameters));
            }
        }

        private void Increase()
        {
            for (int i = 0; i < increaseSize; i++)
            {
                pool.Enqueue((T)Activator.CreateInstance(typeof(T), parameters));
            }
        }

        public T Get()
        {
            if (pool.Count < 1)
                Increase();

            return pool.Dequeue();
        }

        public void Put(T obj)
        {
            pool.Enqueue(obj);
        }
    }
}
