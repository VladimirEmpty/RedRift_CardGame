using System;
using UnityEngine;

namespace Code
{
    /// <summary>
    /// Класс по обновлению подключенных объектов
    /// </summary>
    public sealed class GameUpdater : MonoBehaviour
    {
        public event Action OnUpdate;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}
