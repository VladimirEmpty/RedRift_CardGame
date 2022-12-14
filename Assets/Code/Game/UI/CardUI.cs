using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Code.Game.UI
{
    public sealed class CardUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageValueText;
        [SerializeField] private Button _addCardButton;
        [SerializeField] private Button _removeCardButton;
        [SerializeField] private Button _fireCardButton;

        public TextMeshProUGUI DamageValueText => _damageValueText;
        public Button AddCardButton => _addCardButton;
        public Button RemoveCardButton => _removeCardButton;
        public Button FireCardButton => _fireCardButton;
    }
}
