using System.Collections;
using System.Collections.Generic;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ImageTranslator : MonoBehaviour
{
    [SerializeField] private  Sprite _rusText;
    [SerializeField] private  Sprite _trText;
    [SerializeField] private  Sprite _enText;
    [SerializeField] private Image _image;



    private const string RusLang = "ru";
    private const string TrLang = "tr";
    private const string EnsLang = "en";
    
   
    
    public void TranslateText(string language)
    {
        Debug.Log("пришедший язык "+language);
        
        if (Equals(language,RusLang))
        {
            Debug.Log("выбрало рус");
            _image.sprite = _rusText;
        }
        
        else if (Equals(language,TrLang))
        {
            Debug.Log("выбрало тр");
            _image.sprite  = _trText;
        }

        else
        {
            Debug.Log("выбрало en");
            _image.sprite = _enText;
        }
    }
    
   

}