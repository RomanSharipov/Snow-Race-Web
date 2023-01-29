
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkinView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private SellButon _sellButton;
    [SerializeField] private VideoButton _videoButton;
    [SerializeField] private List<SkinBackGround> _skinBackGrounds;
    [SerializeField] private GameObject _chekMark;
    [SerializeField] private TextTranslator _selectTranslate;
    [SerializeField] private TextTranslator _selectedTranslate;
    [SerializeField] private TextTranslator _watchTranslate;

    private  string _buttonTextOnPurchased;
    private  string _buttonTextOnSelected;
    private  string _videoButtonText;


    public Image Image => _icon;
    

    private const int DefaulstSkinId = 0;

    private Skin _skin;

    public Skin Skin => _skin;

    public event UnityAction<Skin,SkinView> SellButonClick;
    public event UnityAction Selected;
    
    private void Awake()
    {
    #if YANDEX_GAMES 
        _buttonTextOnPurchased = _selectTranslate.GetTranslated();
        Debug.Log("translated buttonTextOnPurchased " +_selectTranslate.GetTranslated());
        _buttonTextOnSelected = _selectedTranslate.GetTranslated();
        Debug.Log("translated _buttonTextOnSelected " + _selectedTranslate.GetTranslated());
        _videoButtonText=_watchTranslate.GetTranslated();
        Debug.Log("translated _videoButtonText " +_watchTranslate.GetTranslated());
        return;
    #endif
       
        Debug.Log("#endif");
        _buttonTextOnPurchased = "Purchased";
        _buttonTextOnSelected = "Selected";
        _videoButtonText = "Watch";

    }
    
    public void OnButtonClick()
    {

        if (_skin.IsBuyed)
        {
            var inventory = Inventory.Load();
            inventory.SelectSkin(_skin);
            inventory.Save();
            Selected?.Invoke();
        }
        else
        {
            SellButonClick?.Invoke(_skin,this);
        }
         ChangeButtonText(); 
         Debug.Log("change");
    }
    
    public void Unsubscribe()
    {
        if (_skin.IsSkinForAdvesting)
        {
            _videoButton.Button.onClick.RemoveListener(OnButtonClick);
        }
        else
        {
            _sellButton.Button.onClick.RemoveListener(OnButtonClick);
        }

    }
    
    public void Render(Skin skin)
    {
        _skin = skin;
        
        if (_skin.IsSkinForAdvesting)
        {
            _videoButton.gameObject.SetActive(true);
            _videoButton.Button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            _sellButton.gameObject.SetActive(true);
            _sellButton.Button.onClick.AddListener(OnButtonClick);
        }

        _icon.sprite = skin.Icon;

        TurnOnBackground(skin);
        ChangeButtonText();
        TruTurnChekMark();
    }
    
    public void Render()
    {

        ChangeButtonText();
       TruTurnChekMark();
    }

    private void TruTurnChekMark()
    {
        var inventory = Inventory.Load();

        if (_chekMark.activeSelf&&inventory.SelectedGuid!=_skin.Id)
        {
            _chekMark.SetActive(false);
        }
        else if(inventory.SelectedGuid==_skin.Id)
        {
            _chekMark.SetActive(true);
        }
    }
    
    private void TurnOnBackground(Skin skin)
    {
        var selectedBackground = _skinBackGrounds.FirstOrDefault(p => p.SkinRarity == skin.SkinRarity);
        selectedBackground.gameObject.SetActive(true);
    }
    
    public void ChangeButtonText()
    {

        if (_skin.IsSkinForAdvesting)
        {
            _videoButton.ChangeButtonText(GetButtonTetxt(),_skin.IsBuyed);
        }
        else
        {
            _sellButton.ChangeButtonText(GetButtonTetxt(),_skin.IsBuyed);
        }

    }

    private string GetButtonTetxt()
    {
        if (_skin.IsSkinForAdvesting&&!_skin.IsBuyed)
        {
            return _videoButtonText;
        }

        var inventory = Inventory.Load();
        
        if (_skin.Id==inventory.SelectedGuid)
        {
            return _buttonTextOnSelected;
        }
        else if (_skin.IsBuyed)
        {
            return _buttonTextOnPurchased;
            
            if (PlayerPrefs.GetInt(Constants.SelectedSkinKey,0)==_skin.Id)
            {
                return  _buttonTextOnSelected;
            }
        }
        else
        {
            return _skin.Price.ToString();
        }
    }


    private void SetLanguage(string currentLanguage)
    {
        switch (currentLanguage)
        {
            case "English":
                _buttonTextOnPurchased = "Select ?";
                _buttonTextOnSelected = "Selected";
                _videoButtonText = "Watch";
                break;

            case "Russian":
                _buttonTextOnPurchased = "Выбрать ?";
                _buttonTextOnSelected = "Выбрано";
                _videoButtonText = "Смотреть";
                break;

            case "Turkish":
                _buttonTextOnPurchased = "Seçmek ?";
                _buttonTextOnSelected = "Seçildi";
                _videoButtonText = "seçilmiş";
                break;
        }
    }
    
}
