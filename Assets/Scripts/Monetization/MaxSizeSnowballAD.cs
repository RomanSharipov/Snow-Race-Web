using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Agava.YandexMetrica;
using UnityEngine.UI;

public class MaxSizeSnowballAD : MonoBehaviour
{
    [SerializeField] private Snowball _playerSnowball;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _soudButton;
    
    private float _normalTimeScale;
    private int _index;
    private bool _isStarted = false;

    public bool IsStarted => _isStarted;
    private void Start()
    {
        
        _normalTimeScale = Time.timeScale;
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _menuButton.gameObject.SetActive(false);
        _soudButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _soudButton.gameObject.SetActive(true);
        _menuButton.gameObject.SetActive(true);
    }

    private void Reward()
    {
        _playerSnowball.SetMaxSize();
        gameObject.SetActive(false);
    }

    private void OnClosedClick()
    {
        Time.timeScale = _normalTimeScale;
        _isStarted = true;
#if !UNITY_EDITOR
        AudioListener.volume = 1;
        Constants.IsWatchAdd = false;
        _index = PlayerPrefs.GetInt("MetricaIndex", 1);
        YandexMetrica.Send($"Start {_index} lvl");
#endif
    }

    private void OnClosedClick(string error)
    {
        Time.timeScale = _normalTimeScale;
        _isStarted = true;
#if !UNITY_EDITOR
        AudioListener.volume = 1;
 Constants.IsWatchAdd = false;
       
#endif
    }

    public void OnButtonClick()
    {
#if !UNITY_EDITOR
        AudioListener.volume = 0;
        Constants.IsWatchAdd = true;
        VideoAd.Show(onRewardedCallback: Reward, onCloseCallback: OnClosedClick, onErrorCallback: OnClosedClick);
        YandexMetrica.Send("SizeReward");
#endif
#if UNITY_EDITOR
        Reward();
        Time.timeScale = _normalTimeScale;
#endif
    }

    public void TapToStart()
    {
        gameObject.SetActive(false);
        Time.timeScale = _normalTimeScale;
        _isStarted = true;
#if !UNITY_EDITOR
        _index = PlayerPrefs.GetInt("MetricaIndex", 1);
        YandexMetrica.Send($"Start {_index} lvl");
#endif
    }
    
}