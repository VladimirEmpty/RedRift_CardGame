using UnityEngine;
using UnityEngine.SceneManagement;

using Code.Game.Setting;

namespace Code.Game
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LoadImageSetting _loadImageSetting;
        private GameImageSource _gameImageSource;

        private void Start()
        {
            _gameImageSource = new GameObject(nameof(GameImageSource)).AddComponent<GameImageSource>();
            _gameImageSource.Load(_loadImageSetting);
            _gameImageSource.OnImagesIsLoaded += StartGame;
        }

        private void StartGame()
        {
            _gameImageSource.OnImagesIsLoaded -= StartGame;
            SceneManager.LoadScene("Game");
        }
    }
}

