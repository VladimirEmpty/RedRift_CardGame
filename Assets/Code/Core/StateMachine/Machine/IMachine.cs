using System;

using Code.StateMachine.State;

namespace Code.StateMachine
{
    public interface IMachine : IDisposable
    {
        public IState CurrentState { get; }

        public void ChangeState(IState state);
        public void Update();
    }
}
