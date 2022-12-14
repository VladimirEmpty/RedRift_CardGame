using System;
using UnityEngine;

using Code.StateMachine;
using Code.StateMachine.State;

namespace Code.Game.Card.State
{
    public sealed class CardHitState : BaseState<CardViewHitDecorator>
    {
        public int currentHealth;
        public int damage;
        public float timeTick;

        private float _currentTimeTick;

        public override bool IsUpdatable => true;

        public event Action OnComplete;
        public event Action<CardViewHitDecorator> OnDestroy;
        public event Action<CardViewHitDecorator, int> OnApplyDamage;

        public override void Enter()
        {
            StateOwner.CardView.CardHealthText.color = damage > 0 ? Color.red : Color.green;
            _currentTimeTick = Time.time + timeTick;
        }

        public override void Update()
        {
            
            if(_currentTimeTick <= Time.time)
            {
                if (damage == 0)
                {
                    StateOwner.Reset();
                    return;
                }
                var tickDamage = (damage > 0) ? 1 : -1;

                currentHealth -= tickDamage;
                damage -= tickDamage;

                OnApplyDamage?.Invoke(StateOwner, tickDamage);

                StateOwner.CardView.CardHealthText.text = currentHealth.ToString();

                if (currentHealth <= 0)
                {
                    OnComplete?.Invoke();
                    OnDestroy?.Invoke(StateOwner);
                }

                _currentTimeTick = Time.time + timeTick;
            }            
        }

        public override void Exit()
        {
            OnComplete?.Invoke();
            StateOwner.CardView.CardHealthText.color = Color.white;
        }

        protected override void OnDispose()
        {
            OnDestroy = null;
            OnComplete = null;
            OnApplyDamage = null;
        }
    }
}
