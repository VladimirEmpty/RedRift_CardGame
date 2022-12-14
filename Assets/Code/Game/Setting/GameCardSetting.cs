using UnityEngine;

using Code.Game.Card;

namespace Code.Game.Setting
{
    [CreateAssetMenu(fileName =nameof(GameCardSetting), menuName = "Setting/GameCardSetting")]
    public sealed class GameCardSetting : ScriptableObject
    {
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private float _animationSpeed;
        [SerializeField] private float _cardHitDelay;
        [SerializeField] private float _radius;
        [SerializeField] private int _minCardStartHealthValue;
        [SerializeField] private int _maxCardStartHealthValue;
        [SerializeField] private int _minCardHitValue;
        [SerializeField] private int _maxCardHitValue;
        [SerializeField] private int _minCardCountOnStart;
        [SerializeField] private int _maxCardCountOnStart;

        public CardView CardPrefab => _cardPrefab;
        public float AnimationSpeed => _animationSpeed;
        public float CardHitDelay => _cardHitDelay;
        public float Radius => _radius;
        public int MinCardStartHealthValue => _minCardStartHealthValue;
        public int MaxCardStartHealthValue => _maxCardStartHealthValue;
        public int MinCardHitValue => _minCardHitValue;
        public int MaxCardHitValue => _maxCardHitValue;
        public int MinCardCountOnStart => _minCardCountOnStart;
        public int MaxCardCountOnStart => _maxCardCountOnStart;
    }
}
