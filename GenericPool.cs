using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GenericPool<T> where T : IPoolable
    {
        private readonly Queue<T> _pool = new Queue<T>();
        private readonly Func<T> _factoryMethod;
        private readonly Action<T> _resetAction;

        public GenericPool(Func<T> factoryMethod, Action<T> resetAction, int initialSize = 10)
        {
            _factoryMethod = factoryMethod ?? throw new ArgumentNullException(nameof(factoryMethod));
            _resetAction = resetAction;

            for (int i = 0; i < initialSize; i++)
            {
                _pool.Enqueue(_factoryMethod());
            }
        }

        public T Get()
        {
            T item;
            if (_pool.Count > 0)
            {
                item = _pool.Dequeue();
            }
            else
            {
                item = _factoryMethod();
            }

            _resetAction?.Invoke(item);
            return item;
        }

        public void Return(T item)
        {
            if (item == null) return;

            item.Reset();
            _pool.Enqueue(item);
        }
    }
}