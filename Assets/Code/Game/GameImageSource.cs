using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

using Code.Game.Setting;

using Random = UnityEngine.Random;

namespace Code.Game
{
    public sealed class GameImageSource : MonoBehaviour
    {
        private Stack<Sprite> _sprites = new Stack<Sprite>();
        [SerializeField] private LoadImageSetting _loadImageSetting;
        private Sprite _defaultSprite;
        private int _loadedImagesCount;

        public static GameImageSource Instance { get; private set; }

        public Sprite CardSprite => _sprites.Count > 0 ? _sprites.Pop() : _defaultSprite;
        public int Hash => GetHashCode();

        public event Action OnImagesIsLoaded;

        public void Load(LoadImageSetting loadImageSetting)
        {
            _loadImageSetting = loadImageSetting;
            _defaultSprite = loadImageSetting.DefaultImage;
            _loadedImagesCount = Random.Range(_loadImageSetting.MinLoadImageCount, _loadImageSetting.MaxLoadIamgeCount);
            Instance = this;
                       
            StartCoroutine(LoadImages());

            DontDestroyOnLoad(this.gameObject);
        }

        private IEnumerator LoadImages()
        {
            using (var webRequest = UnityWebRequest.Get($"https://picsum.photos/v2/list?limit={_loadedImagesCount}"))
            {
                yield return webRequest.SendWebRequest();

                if(webRequest.result == UnityWebRequest.Result.Success)
                {
                    var imageDataArray = JArray.Parse(webRequest.downloadHandler.text);
                    var imageDownloadKey = "download_url";

                    foreach (var value in imageDataArray)
                    {
                        StartCoroutine(LoadTexture(value.Value<string>(imageDownloadKey)));
                    }

                    yield return new WaitWhile(() => _sprites.Count < _loadedImagesCount / 2);
                }
            }

            

            OnImagesIsLoaded?.Invoke();
        }

        private IEnumerator LoadTexture(string url)
        {
            if (string.IsNullOrEmpty(url)) yield return null;

            using(var textureRequest = UnityWebRequestTexture.GetTexture(url))
            {
                yield return textureRequest.SendWebRequest();

                if(textureRequest.result == UnityWebRequest.Result.Success)
                {
                    _sprites.Push(
                                ConvertTextureToSprite(
                                        DownloadHandlerTexture.GetContent(textureRequest)));

                    Debug.Log("Texture is loaded!");
                }
            }
        }

        private Sprite ConvertTextureToSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
        }
    }
}
