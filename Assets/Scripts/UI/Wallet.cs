using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;
//using UnityEditor.SceneManagement;

public class Wallet : MonoBehaviour
    {
        [SerializeField] private Transform _icon = null;
        [SerializeField] private TextMeshProUGUI _text = null;
        [SerializeField] private Transform _coinPrefab = null;
        [SerializeField] private Transform _spawnedCoinsContainer = null;
        [SerializeField] private int _maxSpawnedCoins = 10;
        [SerializeField] private Vector2 _spawnSpreadMultiplier = Vector2.one;
        [SerializeField] private PlayerCollisionHandler _player;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private int _valueForLeaderboard;

        private bool _forbiddenToCollectCoins;
        private int _moneyOnLevelStart;
        private int _collectedForLevelMoney;

        private readonly Queue<Transform> _availableCoins = new Queue<Transform>();
        private float _minDuration = 0.9f;
        private float _maxDuration = 1.4f;
        private int _currentValue = 0;
        private int _targetValue = 0;

        private bool _isAdedForLevelPassing;
        
        private const int MoneyForPassing = 50;
        
        public int Value => _targetValue;
        public int ValueForLeaderboard => _valueForLeaderboard;

        public int CurrentValue => _currentValue;

        public event UnityAction<int> MoneyReceived;

        public event UnityAction ValueChanged;

        private void Awake()
        {
            _targetValue = PlayerPrefs.GetInt(Constants.WalletCoinsKey,0);
            _moneyOnLevelStart = _targetValue;
            if (_targetValue > 0)
            {
                UpdateValue(_targetValue);
            }
        }

        private void OnDisable()
        {
            
            PlayerPrefs.SetInt(Constants.WalletCoinsKey, _targetValue);
        }
        
        
        public IEnumerator Add(int count=MoneyForPassing)
        {
            _isAdedForLevelPassing = true;
            
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (_targetValue + count < 0)
            {
                return null;
            }

            
            PlayerPrefs.SetInt(Constants.WalletCoinsKey, _targetValue);
            MoneyReceived?.Invoke(_targetValue);
             Debug.Log($"MoneyReceived?.Invoke({count});");
            UpdateValue(_targetValue);

            return null;
        }

        public void Hide()
        {
            _canvasGroup.enabled = true;
            _spawnedCoinsContainer.GetComponent<CanvasGroup>().enabled = true;
        }

        public void Show()
        {
            _canvasGroup.enabled = false;
            _spawnedCoinsContainer.GetComponent<CanvasGroup>().enabled = false;
        }

        public void ProhibitCollectingCoins()
        {
            _forbiddenToCollectCoins = true;
        }

        public YieldInstruction AddAndAnimate(Transform to, int count = MoneyForPassing)
        {
            
            
            if (!_forbiddenToCollectCoins)
            {
                if (count <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(count));
                }

                return AnimateCoin(to, _icon, count);
            }

            return null;
            
            
        }

        public YieldInstruction Take(Transform to, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return AnimateCoin(_icon, to, -count);
        }

        public void MultiplyCollectedMoneyForLevel()
        {
            _targetValue += MoneyForPassing;
            PlayerPrefs.SetInt(Constants.WalletCoinsKey, _targetValue);
        }
        
        public int GetMoneyForPassing()
        {
            return MoneyForPassing;
        }
        
        public YieldInstruction Take(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (_targetValue + count < 0)
            {
                return null;
            }
            
            _targetValue -= count;
            PlayerPrefs.SetInt(Constants.WalletCoinsKey, _targetValue);
            UpdateValue(_targetValue);

            return null;
        }
        public bool HaveCoins(int count)
        {
            return _targetValue >= count;
        }

        private void OnAllEnemiesDied()
        {
            PlayerPrefs.SetInt(Constants.WalletCoinsKey, _targetValue);
        }
        
        private void OnTouchedCoin(Transform transform)
        {
            AddAndAnimate(transform,2);
        }
        
        private void OnRewarded(int reward)
        {
            Add(reward);
        }
        
         private YieldInstruction AnimateCoin(Transform from, Transform to, int count)
        {
            if (count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (_targetValue + count < 0)
            {
                return null;
            }

            var coinsToSpawn = _maxSpawnedCoins / 4;
            _targetValue += count;
            _valueForLeaderboard += count;
            
            var rootAnimation = DOTween.Sequence().OnComplete(() =>
            {
                UpdateValue(_targetValue);
            });

            for (int i = 0; i < coinsToSpawn; i++)
            {
                if (_availableCoins.Count <= 0)
                {
                    break;
                }

                var coin = _availableCoins.Dequeue();

                var targetPosition = from.position;
                if (!from.TryGetComponent(out RectTransform _))
                {
                    targetPosition = Camera.main.WorldToScreenPoint(targetPosition);
                }
                coin.position = targetPosition;

                coin.gameObject.SetActive(true);

                var spreadOffset = (Vector3)(URandom.insideUnitCircle * _spawnSpreadMultiplier);

                var duration = URandom.Range(_minDuration, _maxDuration);
                var animation = DOTween.Sequence();
                
                if (count > 0)
                {
                    animation.OnComplete(() =>
                    {
                        coin.gameObject.SetActive(false);
                        _availableCoins.Enqueue(coin);
                        if (!_isAdedForLevelPassing)
                        {
                            UpdateValue(_targetValue + count / coinsToSpawn);
                        }
                      
                    });
                }
                else
                {
                    animation.OnStart(() =>
                    {
                        UpdateValue(_currentValue + count / coinsToSpawn);
                    }).OnComplete(() =>
                    {
                        coin.gameObject.SetActive(false);
                        _availableCoins.Enqueue(coin);
                    });
                }

                animation.Append(coin.DOMove(spreadOffset, duration / 4).SetEase(Ease.InCubic).SetRelative());

                animation.Append(coin.DOMove(to.position, duration / 4 * 3).SetEase(Ease.InOutBack));

                rootAnimation.Join(animation);
            }

            return rootAnimation.WaitForCompletion();
        }


        private void UpdateValue(int newValue)
        {
            if (newValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newValue));
            }

            _currentValue = newValue;
            _text.text = _currentValue.ToString();
            ValueChanged?.Invoke();
        }
        

/*#if UNITY_EDITOR
    public void FindPlayer()
    {
        //_player = StageUtility.GetCurrentStageHandle().FindComponentOfType<Player>();
        //Save();
    }

    private void Save() => UnityEditor.EditorUtility.SetDirty(this);
#endif*/
}

