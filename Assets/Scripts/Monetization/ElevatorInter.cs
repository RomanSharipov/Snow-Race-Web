using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexMetrica;
using UnityEngine;

public class ElevatorInter : MonoBehaviour
{
    [SerializeField] private GameStoper _gameStoper;

    private bool _elevatorInterIsOpen = false;

    private float _delayBeforeInterstitialAdShow = 0.5f;

    public bool ElevatorInterIsOpen => _elevatorInterIsOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Invoke(nameof(InterstitialAdShow), _delayBeforeInterstitialAdShow);
        }
    }

    public void OnOpen()
    {
        AudioListener.volume = 0;
        _gameStoper.Stop();
        _elevatorInterIsOpen = true;
        YandexMetrica.Send("InterShow");
    }

    public void OnClose(bool isClose)
    {
        AudioListener.volume = 1;
        _gameStoper.Resume();
        _elevatorInterIsOpen = false;
    }

    public void OnError(string error)
    {
        AudioListener.volume = 1;
        _gameStoper.Resume();
        _elevatorInterIsOpen = false;
    }

    public void InterstitialAdShow()
    {
#if UNITY_EDITOR
        return;
#endif
        InterstitialAd.Show(OnOpen, OnClose, OnError);
    }
}
