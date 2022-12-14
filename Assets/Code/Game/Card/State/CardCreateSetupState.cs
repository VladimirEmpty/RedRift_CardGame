using UnityEngine;

using Code.StateMachine;
using Code.StateMachine.State;

namespace Code.Game.Card.State
{
    public sealed class CardCreateSetupState : BaseState<CardView>
    {
        public Vector3 createPosition;
        public Vector3 handPivotPosition;
        public int health;
        public bool isRotated;

        public override bool IsUpdatable => false;

        public override void Enter()
        {
            StateOwner.transform.position = createPosition;

            if(isRotated) 
                StateOwner.transform.up = createPosition - handPivotPosition;

            StateOwner.CardImage.sprite = GameImageSource.Instance.CardSprite;
            StateOwner.CardHealthText.text = health.ToString();

            StateOwner.Reset();
        }
    }
}
