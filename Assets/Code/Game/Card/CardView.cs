using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

using Code.StateMachine;

namespace Code.Game.Card
{
    public sealed class CardView : MonoBehaviour, IStateMachineOwner
    {
        [SerializeField] private Image _cardImage;
        [SerializeField] private GameObject _selectedBG;
        [SerializeField] private TextMeshProUGUI _cardHealthText;

        public Image CardImage => _cardImage;
        public GameObject SeletedBG => _selectedBG;
        public TextMeshProUGUI CardHealthText => _cardHealthText;
        public int Hash => GetHashCode();
    }
}
