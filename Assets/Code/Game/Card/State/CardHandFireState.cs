using System.Collections.Generic;
using UnityEngine;

using Code.StateMachine;
using Code.StateMachine.State;
using Code.Game.UI;
using Code.Game.Setting;

using Random = UnityEngine.Random;

namespace Code.Game.Card.State
{
    public sealed partial class CardHandFireState : BaseState<CardHandView>
    {
        public Queue<CardView> _cardQueue;
        public CardService cardService;

        private CardViewHitDecorator _currentCardViewDecorator;
        private int _damage;
        private int _currentAwaitCount;

        public override bool IsUpdatable => true;

        public override void Enter()
        {
            _damage = Random.Range(
                                cardService.GameCardSetting.MinCardHitValue,
                                cardService.GameCardSetting.MaxCardHitValue);

            cardService.CardUI.DamageValueText.text = _damage.ToString();
            cardService.CardUI.DamageValueText.color = (_damage > 0) ? Color.red : Color.green;

            _currentAwaitCount = _cardQueue.Count + 1;
            HitNextCard();
        }

        public override void Update()
        {
            if(_currentAwaitCount <= 0)
            {
                StateOwner.Reset();
            }
        }

        protected override void OnDispose()
        {
            _cardQueue = null;
            _currentCardViewDecorator?.Reset();
            _currentCardViewDecorator?.Dispose();
        }

        private void HitNextCard()
        {
            _currentAwaitCount--;
            if(_cardQueue == null | _currentAwaitCount <= 0) return;            

            var card = _cardQueue.Dequeue();
            _currentCardViewDecorator = new CardViewHitDecorator(card);

            _currentCardViewDecorator.ChangeState<CardViewHitDecorator, CardHitState>(x =>
            {
                x.currentHealth = cardService.GetCardHealth(card);
                x.damage = _damage;
                x.timeTick = cardService.GameCardSetting.CardHitDelay;
                x.OnDestroy += RemoveCard;
                x.OnComplete += HitNextCard;
                x.OnApplyDamage += ApplayDamage;
            });
        }

        private void ApplayDamage(CardViewHitDecorator decorator, int damage)
        {
            cardService.ApplayCardDamage(decorator.CardView, damage);
        }

        private void RemoveCard(CardViewHitDecorator decorator)
        {
            cardService.RemoveCard(decorator.CardView);
            decorator.RemoveStateMachine();
            decorator.Dispose();
        }
    }
}
