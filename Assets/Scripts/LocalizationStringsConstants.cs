using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocalizationStringsConstants : MonoBehaviour
{
   
    
    public event UnityAction<Launge> LaungeSeted;

    private const string LangKey = "LangKey";

    
    
    public static string _lang
    {
        get { return PlayerPrefs.GetString(LangKey,null); }
        private set { PlayerPrefs.SetString(LangKey, value); }
    }
    public static void SetLaunge(string lang)
    {
        _lang = lang;
    }
}

public enum  Launge{ru,tr,en}