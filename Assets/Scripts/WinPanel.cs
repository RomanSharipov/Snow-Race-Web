using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;
using Agava.YandexMetrica;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private LeaderBoard _leaderBoard;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _noThanksShowDelay;
    [SerializeField] private Button _noThanks;

    public event Action LevelComplite;

    private void Start()
    {
        _noThanks.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _leaderBoard.SetScore();
        _joystick.gameObject.SetActive(false);
        StartCoroutine(NoThanksShow());
#if UNITY_EDITOR
        return;
#endif
        InterstitialAd.Show(OnOpen,OnClose,OnError);
    }

    private void MetricaSend(bool isClose)
    {
        YandexMetrica.Send("InterShow");
    }

    private IEnumerator NoThanksShow()
    {
        yield return new WaitForSeconds(_noThanksShowDelay);
        _noThanks.gameObject.SetActive(true);
    }

    public void ChangeTetx(string text)
    {
        _text.text = text;
    }
    
    private void OnOpen()
    {
        Constants.IsWatchAdd = true;
        AudioListener.volume = 0;
        YandexMetrica.Send("InterShow");
    }

    private void OnClose(bool bol)
    {
        AudioListener.volume = 1;
        Constants.IsWatchAdd = false;
        
    }

    private void OnError(string error)
    {
        AudioListener.volume = 1;
        Constants.IsWatchAdd = false;
    }
}