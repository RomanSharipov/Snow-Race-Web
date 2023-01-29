using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using Agava.YandexGames;
using Agava.YandexMetrica;

public class InterstitialAdOnstart : MonoBehaviour
{
    public void ShowInterstitialAd()
    {
        
#if !UNITY_EDITOR
    InterstitialAd.Show();
    YandexMetrica.Send("InterShow");
#endif
    }
}
