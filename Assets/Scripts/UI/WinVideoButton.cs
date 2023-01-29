using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Agava.YandexMetrica;
using MPUIKIT;
using UnityEngine.UI;

public class WinVideoButton : MonoBehaviour
{
    [SerializeField] private TemplatesData _templatesData;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Image _newSkinSprite;
    [SerializeField] private Image _skinSprite;
    [SerializeField] private Image _spriteBackGround;
    [SerializeField] private MPImage _newSkinVideo;
    [SerializeField] private Button _noThanks;

    private Skin _newSkin;
    private bool _allSkinsBuyed;

    private void OnEnable()
    {
        var inventory = Inventory.Load();
        
        foreach (var skin in _templatesData.SkinTemplates)
        {
            if(inventory.Contains(skin))
                skin.Buy();
            
            if (skin.IsBuyed == false)
            {
                _allSkinsBuyed = false;
                _newSkin = skin;
                break;
            }
            else
            {
                _allSkinsBuyed = true;
            }
        }

        if (_allSkinsBuyed == false)
        {
            _newSkinSprite.sprite = _newSkin.Icon;
            _noThanks.gameObject.SetActive(true);
            _newSkinVideo.gameObject.SetActive(true);
            _spriteBackGround.gameObject.SetActive(true);
            _skinSprite.gameObject.SetActive(true);
        }
        else
        {
            _sceneLoader.LoadNewScene();
        }
    }

    private void Reward()
    {
        _newSkin.Buy();
        var inventory = Inventory.Load();
        inventory.AddSkin(_newSkin);
        inventory.SelectSkin(_newSkin);
        inventory.Save();
    }

    private void OnCloseCallback()
    {
        _sceneLoader.LoadNewScene();
        AudioListener.volume = 1;
        Constants.IsWatchAdd = false;
    }

    private void OnCloseCallback(string error)
    {
        _sceneLoader.LoadNewScene();
        AudioListener.volume = 1;
        Constants.IsWatchAdd = false;
    }

    public void OnButtonClick()
    {
        AudioListener.volume = 0;
        Constants.IsWatchAdd = true;
#if !UNITY_EDITOR
        VideoAd.Show(onRewardedCallback: Reward, onCloseCallback:OnCloseCallback, onErrorCallback: OnCloseCallback);
        YandexMetrica.Send("SkinReward");
#endif
#if UNITY_EDITOR
        var inventory = Inventory.Load();
        _newSkin.Buy();
        inventory.AddSkin(_newSkin);
        inventory.SelectSkin(_newSkin);
        inventory.Save();
        _sceneLoader.LoadNewScene();
#endif
    }
}