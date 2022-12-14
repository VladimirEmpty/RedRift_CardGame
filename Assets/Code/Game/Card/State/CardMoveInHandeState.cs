using UnityEngine;

using Code.StateMachine;
using Code.StateMachine.State;

namespace Code.Game.Card.State
{
    public sealed class CardMoveInHandeState : BaseState<CardView>
    {
        public Vector3 toPosition;
        public Vector3 handPivotPosition;
        public float animationSpeed ;
        public bool isNoRotated;

        public float CompleteDistance = 0.25f;
        public override bool IsUpdatable => true;

        public override void Update()
        {
            StateOwner.transform.position = Vector3.Slerp(StateOwner.transform.position, toPosition, Time.deltaTime * animationSpeed);            

            var distance = (toPosition - StateOwner.transform.position).magnitude;

            if(distance <= CompleteDistance)
            {
                StateOwner.transform.position = toPosition;
                StateOwner.Reset();
            }

            if (isNoRotated)
                StateOwner.transform.rotation = Quaternion.Euler(Vector3.zero);
            else
                StateOwner.transform.up = StateOwner.transform.position - handPivotPosition;
        }
    }
}
