using UnityEngine;

using Code.Game.UI;
using Code.Game.Card;
using Code.Game.Setting;

namespace Code.Game
{
    public sealed class SceneInstaller : MonoBehaviour
    {
        [SerializeField] private CardUI _cardUI;
        [SerializeField] private CardHandView _cardHandView;
        [SerializeField] private GameCardSetting _gameCardSetting;

        private void Reset()
        {
            name = nameof(SceneInstaller);
        }

        private void Start()
        {
            var cardService = new CardService(_cardUI, _cardHandView, _gameCardSetting);
            //cardService.StartCreateCardInHand();
        }

    }
}
