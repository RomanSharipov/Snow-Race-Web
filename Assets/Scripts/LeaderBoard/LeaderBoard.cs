using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    private const string LevelsLeaderBoard = "LevelsLeaderBoard";

    [SerializeField] private Transform _content;
    [SerializeField] private LeaderBoardPlayerInfo _playerInfoPrefab;
    [SerializeField] private Button _leaderBoardButton;
    [SerializeField] private GameObject _authorizationButton;

    private void OnEnable()
    {
        _authorizationButton.gameObject.SetActive(false);
        Show();
    }

    public void Show()
    {
        if (PlayerAccount.IsAuthorized && !PlayerAccount.HasPersonalProfileDataPermission)
            PlayerAccount.RequestPersonalProfileDataPermission();

        ClearContent();
       
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.GetEntries(LevelsLeaderBoard, (result) =>
            {
                foreach (var entrie in result.entries)
                {
                    LeaderBoardPlayerInfo playerInfo = Instantiate(_playerInfoPrefab, _content);
                    string name = entrie.player.publicName;
                    if (string.IsNullOrEmpty(name))
                        name = "Anonym";
                    playerInfo.SetInfo(name, entrie.score.ToString());
                }
            });
        }
        else
        {
            ClearContent();
            _authorizationButton.SetActive(true);
        }
    }

    public void SetScore()
    {
        int count = PlayerPrefs.GetInt("ComplitedLevels", 1);
        count++;
        PlayerPrefs.SetInt("ComplitedLevels", count);

#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            if (PlayerAccount.HasPersonalProfileDataPermission)
                Leaderboard.SetScore(LevelsLeaderBoard, count);
        }
#endif
    }

    public void OnClosetButtonClick()
    {
        _leaderBoardButton.interactable = true;
        gameObject.SetActive(false);
    }

    private void ClearContent()
    {
        if (_content.childCount > 0)
        {
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }
        }
    }
}