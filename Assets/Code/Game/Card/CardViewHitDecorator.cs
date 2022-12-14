using System;

using Code.StateMachine;

namespace Code.Game.Card
{
    public sealed class CardViewHitDecorator : IStateMachineOwner, IDisposable
    {
        private bool _isDisposed;

        public CardViewHitDecorator(CardView cardView)
        {
            CardView = cardView;
        }

        public CardView CardView { get; private set; }
        public int Hash => GetHashCode();

        public void Dispose()
        {
            if (_isDisposed) return;

            CardView = null;
            _isDisposed = true;

            this.RemoveStateMachine();

            GC.SuppressFinalize(this);
        }
    }
}
