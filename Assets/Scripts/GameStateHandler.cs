using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    private const float WinPanelDellay = 1.5f;
    private const int ScoreForCompliteLevel = 10;

    [SerializeField] private Snowball _snowball;
    [SerializeField] private List<ScoreMultiplayer> _scoreMultiplayers;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private bool _gameIsOver = false;
    [SerializeField] private bool _playerIsWin = false;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private FinishColisionHandler _finishColisionHandler;


    private int _scoreMultiplayer = 1;

    public bool GameIsOver => _gameIsOver;
    public bool PlayerIsWin => _playerIsWin;

    private void OnEnable()
    {
        _gameIsOver = false;
        _snowball.Stoped += OnSnowballStoped;

        for (int i = 0; i < _scoreMultiplayers.Count; i++)
        {
            _scoreMultiplayers[i].SnowballEntered += OnSnowballEntered;
        }

        foreach (var enemy in _enemies)
        {
            enemy.Init(this);
            enemy.Won += SetGameOver;
        }

        _finishColisionHandler.Init(this);
        _finishColisionHandler.PlayerWon += SetGameOver;
    }

    public void SetGameOver()
    {
        _gameIsOver = true;
    }

    public void SetPlayerWin()
    {
        _playerIsWin = true;
    }

    private void OnDisable()
    {
        _snowball.Stoped -= OnSnowballStoped;

        for (int i = 0; i < _scoreMultiplayers.Count; i++)
        {
            _scoreMultiplayers[i].SnowballEntered -= OnSnowballEntered;
        }

        foreach (var enemy in _enemies)
        {
            enemy.Won -= SetGameOver;
        }

        _finishColisionHandler.PlayerWon -= SetGameOver;
    }

    private void OnSnowballStoped()
    {
        Invoke(nameof(OpenWinPanel), WinPanelDellay);
        var walletValue = PlayerPrefs.GetInt(Constants.WalletCoinsKey);
        // Debug.Log("walet value "+walletValue);
        // Debug.Log("ScoreForCompliteLevel "+ScoreForCompliteLevel);
        // Debug.Log("_scoreMultiplayer "+_scoreMultiplayer);
        Debug.Log(walletValue + ScoreForCompliteLevel * _scoreMultiplayer);
        PlayerPrefs.SetInt(Constants.WalletCoinsKey, walletValue + ScoreForCompliteLevel * _scoreMultiplayer);
    }

    private void OpenWinPanel()
    {
        _winPanel.gameObject.SetActive(true);
        _winPanel.ChangeTetx((ScoreForCompliteLevel * _scoreMultiplayer).ToString());
    }

    private void OnSnowballEntered(int scoreMultiplayer)
    {
        Debug.Log("scoreMultiplayer " + scoreMultiplayer);
        Debug.Log("_scoreMultiplayer " + _scoreMultiplayer);
        if (scoreMultiplayer > _scoreMultiplayer)
        {
            Debug.Log("true");
            _scoreMultiplayer = scoreMultiplayer;
        }
    }
}