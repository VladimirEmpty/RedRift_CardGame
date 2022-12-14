using System;
using Code.StateMachine.State;

namespace Code.StateMachine
{
    /// <summary>
    /// Примитивная StateMachine для работы с объектами, реализующий интерфейс <see cref="IState"/>
    /// Все объекты State являются одноразовыми
    /// </summary>
    public sealed class Machine : IMachine
    {
        private bool _isDisposed;

        public Machine() { }

        public IState CurrentState { get; private set; }

        private event Action OnUpdate;

        public void ChangeState(IState state)
        {
            if (CurrentState != null)
            {
                OnUpdate -= CurrentState.Update;
                CurrentState.Exit();
                CurrentState.Dispose();
            }

            CurrentState = state;
            CurrentState.Enter();

            if (CurrentState.IsUpdatable)
            {
                OnUpdate += CurrentState.Update;
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            CurrentState.Dispose();
            CurrentState = null;
            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        public void Update() => OnUpdate?.Invoke();
    }
}
