using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class SDKInitiolize : MonoBehaviour
{
    [SerializeField] private ImageTranslator _imageTranslator;
    [SerializeField] private InterstitialAdOnstart _interstitialAdOnstart;

    private string _language;
    private LeanLocalization _lean;

    public string Language => _language;
    
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();
        
        LocalizationStringsConstants.SetLaunge(YandexGamesSdk.Environment.i18n.lang);
         _language = YandexGamesSdk.Environment.i18n.lang;
         _imageTranslator.TranslateText(_language);
         PlayerPrefs.SetString(Constants.LangKey, _language);
        
        _lean = GetComponent<LeanLocalization>();
        switch (_language)
        {
            case "ru":
                _lean.SetCurrentLanguage("Russian");
                break;
            case "en":
                _lean.SetCurrentLanguage("English");
                break;
            case "tr":
                _lean.SetCurrentLanguage("Turkish");
                break;
        }
        _interstitialAdOnstart?.ShowInterstitialAd();
    }
}
