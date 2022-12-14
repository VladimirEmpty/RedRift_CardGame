using System.Collections.Generic;
using UnityEngine;

using Code.Factory;
using Code.StateMachine;
using Code.Game.UI;
using Code.Game.Setting;
using Code.Game.Card.State;

namespace Code.Game.Card
{
    public class CardHandView : MonoBehaviour, IStateMachineOwner
    {
        [Space(10f)]
        [Header("Gizom Debug Value")]
        [SerializeField] private int _cardCountDebugGizmo;
        [SerializeField] private float _radius;

        public int Hash => GetHashCode();       

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
            Gizmos.color = Color.green;

            var cardTransformData = CardInHandPositionHandler.GetCardInHandPositions(this.transform, _cardCountDebugGizmo, _radius);
            var sphereRadius = 25f;

            for (var i = 0; i < cardTransformData.Length; ++i)
            {
                Gizmos.DrawSphere(cardTransformData[i], sphereRadius);
            }
        }
    }
}

