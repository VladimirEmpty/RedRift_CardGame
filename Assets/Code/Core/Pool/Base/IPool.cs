using System;

using Code.Factory;

namespace Code.Pool
{
    /// <summary>
    /// Основной интерфейс пула объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPool<T> : IContainablePool
        where T : UnityEngine.Object
    {
        IFactory<T> Factory { get; }

        public T Spawn();
        public void Despawn(T despawnedObject);
    }
}
