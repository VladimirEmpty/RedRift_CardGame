using UnityEngine;

namespace Code.Game.Setting
{
    [CreateAssetMenu(fileName = nameof(LoadImageSetting), menuName = "Setting/LoadImageSetting")]
    public sealed class LoadImageSetting : ScriptableObject
    {
        [SerializeField] Sprite _defaultImage;
        [SerializeField] private int _minLoadImageCount;
        [SerializeField] private int _maxLoadImageCount;

        public Sprite DefaultImage => _defaultImage;
        public int MinLoadImageCount => _minLoadImageCount;
        public int MaxLoadIamgeCount => _maxLoadImageCount;
    }
}
