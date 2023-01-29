using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Agava.YandexMetrica;

public class SkiColisionHandler : MonoBehaviour
{
    [SerializeField] private AudioHandler _audioHandler;
    
    private float _normalTimeScale;
    private Player _player;

    private bool _isOpenSkiRewardAD = false;

    public bool IsOpenSkiRewardAD => _isOpenSkiRewardAD;

    private void Start()
    {
        _normalTimeScale = Time.timeScale;
    }

#if UNITY_EDITOR

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out Player player))
        {
            _player = player;
            _player.UseSki();
            gameObject.SetActive(false);
        }
    }

#endif
#if !UNITY_EDITOR

     private void OnTriggerEnter(Collider other)
    {
        _normalTimeScale = Time.timeScale;
        if (other.TryGetComponent(out Player player))
        {
            Time.timeScale = 0;
            _player = player;
            YandexMetrica.Send("SkiReward");
            VideoAd.Show(onOpenCallback:OnOpenCallback, onRewardedCallback: Reward, onCloseCallback: SetNormalTimeScale, onErrorCallback: SetNormalTimeScale);
            
        }
    }

     private void OnOpenCallback()
     {
        _isOpenSkiRewardAD = true;
         AudioListener.volume = 0;
     }

    private void Reward()
    {
        _player.UseSki();
        gameObject.SetActive(false);
    }

    private void SetNormalTimeScale()
    {
        _isOpenSkiRewardAD = false;
        Time.timeScale = _normalTimeScale;
        AudioListener.volume = 1;
    }

    private void SetNormalTimeScale(string error)
    {
        _isOpenSkiRewardAD = false;
        Time.timeScale = _normalTimeScale;
        AudioListener.volume = 1;
    }
#endif
}

