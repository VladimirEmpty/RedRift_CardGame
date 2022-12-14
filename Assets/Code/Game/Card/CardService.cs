using System.Collections.Generic;
using UnityEngine;

using Code.Factory;
using Code.StateMachine;

using Code.Game.UI;
using Code.Game.Setting;
using Code.Game.Card.State;

namespace Code.Game.Card
{
    public sealed class CardService
    {

        private IDictionary<CardView, int> _cardInHandHealthTable;
        private List<CardView> _cardsInHand;

        public CardService(
            CardUI cardUI, 
            CardHandView cardHand,
            GameCardSetting gameCardSetting)
        {
            CardUI = cardUI;
            CardHandView = cardHand;
            GameCardSetting = gameCardSetting;

            _cardInHandHealthTable = new Dictionary<CardView, int>();
            _cardsInHand = new List<CardView>();

            cardUI.AddCardButton.onClick.AddListener(AddCardInHand);
            cardUI.RemoveCardButton.onClick.AddListener(RemoveRandomCardInHand);
            cardUI.FireCardButton.onClick.AddListener(ExecuteCardFire);

            StartCreateCardInHand();
        }

        public CardUI CardUI { get; }
        public CardHandView CardHandView { get; }
        public GameCardSetting GameCardSetting { get; }

        public int GetCardHealth(CardView card) => _cardInHandHealthTable[card];
        public void ApplayCardDamage(CardView card, int damage) => _cardInHandHealthTable[card] -= damage;
        public void RemoveCard(CardView card) => RemoveCardInHand(card);
        public void StartCreateCardInHand()
        {
            var count = Random.Range(GameCardSetting.MinCardCountOnStart, GameCardSetting.MaxCardCountOnStart);

            using (var factory = new GameObjectFactory<CardView>(GameCardSetting.CardPrefab))
            {
                var cardInHandPositions = CardInHandPositionHandler.GetCardInHandPositions(CardHandView.transform, count, GameCardSetting.Radius);

                for (var i = 0; i < cardInHandPositions.Length; ++i)
                {
                    CreateNewCard(factory, cardInHandPositions[i]);
                }
            }
        }

        private void AddCardInHand()
        {
            using (var factory = new GameObjectFactory<CardView>(GameCardSetting.CardPrefab))
            {
                var cardInHandPositions = CardInHandPositionHandler.GetCardInHandPositions(CardHandView.transform, _cardsInHand.Count + 1, GameCardSetting.Radius);

                for (var i = 0; i < _cardsInHand.Count; ++i)
                {
                    _cardsInHand[i].ChangeState<CardView, CardMoveInHandeState>(x =>
                    {
                        x.toPosition = cardInHandPositions[i];
                        x.handPivotPosition = CardHandView.transform.position;
                        x.animationSpeed = GameCardSetting.AnimationSpeed;
                    });
                }

                CreateNewCard(factory, cardInHandPositions[cardInHandPositions.Length - 1], _cardsInHand.Count + 1 > 2);
            }
        }

        private void RemoveRandomCardInHand() => RemoveCardInHand(_cardsInHand[Random.Range(default, _cardsInHand.Count)]);

        private void RemoveCardInHand(CardView removedCard)
        {
            var cardInHandPosiitons = CardInHandPositionHandler.GetCardInHandPositions(CardHandView.transform, _cardsInHand.Count - 1, GameCardSetting.Radius);

            removedCard.RemoveStateMachine();
            _cardsInHand.Remove(removedCard);
            _cardInHandHealthTable.Remove(removedCard);
            Object.Destroy(removedCard.gameObject);

            for (var i = 0; i < _cardsInHand.Count; ++i)
            {
                _cardsInHand[i].ChangeState<CardView, CardMoveInHandeState>(x =>
                {
                    x.toPosition = cardInHandPosiitons[i];
                    x.handPivotPosition = CardHandView.transform.position;
                    x.animationSpeed = GameCardSetting.AnimationSpeed;
                    x.isNoRotated = _cardsInHand.Count <= 2;
                });
            }
        }

        private void ExecuteCardFire()
        {
            CardHandView.Reset();
            CardHandView.ChangeState<CardHandView, CardHandFireState>(x =>
            {
                x._cardQueue = new Queue<CardView>(_cardsInHand);
                x.cardService = this;
            });
        }

        private void CreateNewCard(IFactory<CardView> cardFactory, Vector3 position, bool isRotaded = true)
        {
            var card = cardFactory.Create();
            var cardHealth = Random.Range(GameCardSetting.MinCardStartHealthValue, GameCardSetting.MaxCardStartHealthValue);

            card.ChangeState<CardView, CardCreateSetupState>(x =>
            {
                x.createPosition = position;
                x.handPivotPosition = CardHandView.transform.position;
                x.health = cardHealth;
                x.isRotated = isRotaded;
            });

            card.transform.SetParent(CardHandView.transform);
            _cardsInHand.Add(card);
            _cardInHandHealthTable.Add(card, cardHealth);
        }
    }
}
