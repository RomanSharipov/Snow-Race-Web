using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TextTranslator : MonoBehaviour
{
    [SerializeField] private  string _rusText;
    [SerializeField] private  string _trText;
    [SerializeField] private  string _enText;
    [SerializeField] private TextMeshProUGUI _text;

    private string targetLang;
    
    private const string RusLang = "ru";
    private const string TrLang = "tr";
    private const string EnsLang = "en";

    private void Awake()
    {
    #if YANDEX_GAMES
        targetLang = LocalizationStringsConstants._lang;
    #endif

    #if VK_GAMES
        targetLang = RusLang;
    #endif
        return;
    }
    
    private void OnEnable()
    {
        if (_text!=null)
        {
            TranslateText();
        }
    }

    public string GetTranslated()
    {

        string targetText;
        
        if (Equals(LocalizationStringsConstants._lang,RusLang))
        {
            targetText = _rusText;
        }
        
        else if (Equals(LocalizationStringsConstants._lang,TrLang))
        {
            targetText = _trText;
        }

        else
        {
            targetText = _enText;
        }

        return targetText;
    }
    
    private void TranslateText()
    {
        if (Equals(LocalizationStringsConstants._lang,RusLang))
        {
            _text.text = _rusText;
        }
        
        else if (Equals(LocalizationStringsConstants._lang,TrLang))
        {
            _text.text = _trText;
        }

        else
        {
            _text.text = _enText;
        }
    }

}