using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Agava.YandexMetrica;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
   [SerializeField] private TemplatesData _templatesDataIsSkinsForBuy;
   [SerializeField] private Wallet _wallet;
   [SerializeField] private SkinView _temlate;
   [SerializeField] private GameObject _skinContainer;


   [SerializeField] private List<SkinView> _skinViews = new List<SkinView>();

    private int _countNewSkins;
    private Skin _skinForAdvertising;
    private SkinView _viewSkinForAdvertising;

    
    public SkinView LastSkinView => _skinViews[_skinViews.Count - 1];
    public event UnityAction SkinSelected;

    private void OnEnable()
    {
        //_gameStopper.Stop();
        //_gameStopper.NotifyAboutOpenedPanel();
#if UNITY_EDITOR
        return;
#endif
        //YandexMetrica.Send("openRewardedSkinShop");
    }

    private void OnDisable()
    {
        //_gameStopper.TryPlay();
        //_gameStopper.NotifyAboutClosePanel();
    }
    
    private void OnValidate()
    {
#if UNITY_EDITOR

       // _gameStopper = UnityEditor.SceneManagement.StageUtility.GetCurrentStageHandle().FindComponentOfType<GameStopper>();
       // _advertisingImpression = UnityEditor.SceneManagement.StageUtility.GetCurrentStageHandle().FindComponentOfType<AdvertisingImpressionСounter>();
        //_newSkinTeaser = UnityEditor.SceneManagement.StageUtility.GetCurrentStageHandle().FindComponentOfType<NewSkinTeaser>();
        
#endif
    }

    private void Start()
    {
        Init();
    }
    
    
    private void Init()
    {
        var inventory = Inventory.Load();

       // Debug.Log("inited");
        for (int i = 0; i < _templatesDataIsSkinsForBuy.SkinTemplates.Count; i++)
        {
            Debug.Log("count"+_templatesDataIsSkinsForBuy.SkinTemplates[i]);
            if (inventory.Contains(_templatesDataIsSkinsForBuy.SkinTemplates[i]))
            {
                //Debug.Log(_templatesDataIsSkinsForBuy.SkinTemplates[i].name);
                _templatesDataIsSkinsForBuy.SkinTemplates[i].Buy();
            }
            AddSkin(_templatesDataIsSkinsForBuy.SkinTemplates[i]);
        }
        _skinViews.RemoveAt(0);
       // _advertisingImpression.Init();
        RefreshCurrentSkinViewButton();
    }

    public void RefreshCurrentSkinViewButton()
    {
        var inventory = Inventory.Load();

//        Debug.Log($"_skinViews.Count = {_skinViews.Count} inventory.SelectedGuid = {inventory.SelectedGuid}");
        Skin SelectedSkinView = _skinViews[inventory.SelectedGuid].Skin;
        SelectedSkinView.Buy();
        inventory.SelectSkin(SelectedSkinView);
        inventory.Save();
        SkinSelected?.Invoke();

        foreach (var skinView in _skinViews)
        {
            Debug.Log(skinView);
            skinView.ChangeButtonText();
        }
    }

    public void AddSkin(Skin skin)
   {
        SkinView view = Instantiate(_temlate, _skinContainer.transform);
        _skinViews.Add(view);
       view.SellButonClick += OnSellButonClick;
       view.Selected += OnSelected;
       view.Render(skin);
       
   }


   /* public void AddSkinsForAdvertisingToShop()
    {
        _countNewSkins = PlayerPrefs.GetInt(Constants.CurrentSkinIndexForAdd, 0);

        if (_countNewSkins > _templatesDataIsSkinsForAdvertising.SkinTemplates.Count)
        {
            _countNewSkins = _templatesDataIsSkinsForAdvertising.SkinTemplates.Count;
           // _advertisingImpression.CancelOfferSkins();
        }

        var inventory = Inventory.Load();
        for (int i = 0; i < _countNewSkins; i++)
        {
            Skin newSkin = _templatesDataIsSkinsForAdvertising.SkinTemplates[i];

            if (inventory.Contains(newSkin))
            {
                newSkin.Buy();
            }
            AddSkin(newSkin);
        }

        if (_advertisingImpression.IsNeedShowTeaser)
        {
            _newSkinTeaser.Init(LastSkinView);
            _newSkinTeaser.Open();
        }
    }*/

    private void OnSellButonClick(Skin skin,SkinView view)
   {
       TrySellSkin(skin,view);
   }

   private void TrySellSkin(Skin skin,SkinView view)
   {
        if (skin.IsSkinForAdvesting)
        {
            _skinForAdvertising = skin;
            _viewSkinForAdvertising = view;
            
            

#if UNITY_EDITOR
            SellSkin(_skinForAdvertising, _viewSkinForAdvertising);
            return;
#endif
            //YandexMetrica.Send("skinWasReceived", $"{{\"{skin.Name}\": \" Viewing ads \"}}");
            VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnClosed, OnErrorCallback);
            YandexMetrica.Send("SkinReward");
            
            //CrazyAds.Instance.beginAdBreakRewarded(OnRewardedCallback);
            //CrazySDK.Instance.GameplayStop();
        }
        
        
        if (skin.Price <= _wallet.Value)
        {
            Debug.Log("waletwalue");
            _wallet.Take( skin.Price);
            SellSkin(skin,view);
            
#if UNITY_EDITOR
            return;
#endif
           // YandexMetrica.Send("softSpend", $"{{\"{skin.Name}\": \" {skin.Price}\"}}");
        }
        else
        {
            Debug.Log("not walue");
        }
    }


    

    private void SellSkin(Skin skin,SkinView view)
   {
       skin.Buy();
       var inventory = Inventory.Load();
       inventory.AddSkin(skin);
       inventory.Save();
       view.SellButonClick -= OnSellButonClick;

       for (int i = 0; i < _skinViews.Count; i++)
       {
           _skinViews[i].Render();
       }
    }
   
   private void OnSelected()
   {
       SkinSelected?.Invoke();
       
       for (int i = 0; i < _skinViews.Count; i++)
       {
           _skinViews[i].Render();
       }
   }

    public void Unsubscribe()
   {
       for (int i = 0; i < _skinViews.Count; i++)
       {
           _skinViews[i].Selected -= OnSelected;
           _skinViews[i].Unsubscribe();
       }
       
   }
    

    private void OnRewardedCallback()
    {
        SellSkin(_skinForAdvertising, _viewSkinForAdvertising);
       // CrazySDK.Instance.GameplayStart();
        
    }

   
    private void OnOpenCallback()
    {
        AudioListener.volume = 0;
        Constants.IsWatchAdd = true;
    }

    private void OnClosed()
    {

        AudioListener.volume = 1;
        Constants.IsWatchAdd = false;
    }

    private void OnErrorCallback(string errorText)
    {
        AudioListener.volume = 1;
        Constants.IsWatchAdd = false;
        Debug.Log($"OnErrorCallback = {errorText}");
        OnRewardedCallback();
    }

}


