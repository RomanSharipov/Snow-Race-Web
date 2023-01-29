using Agava.YandexGames;
using Agava.YandexMetrica;
using UnityEngine;

public class InterADCounter : MonoBehaviour
{
    [SerializeField] private float _interDeley;

    private float _counter;

    private void Update()
    {
        _counter += Time.deltaTime;

        if (_counter >= _interDeley)
            ShowInter();
    }

    private void ShowInter()
    {
        InterstitialAd.Show(onOpenCallback: OnOpen, OnClose, OnError);
        _counter = 0;
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
