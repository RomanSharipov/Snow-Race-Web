using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellButon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;
    [SerializeField] private int _targetWidth;

    [SerializeField] private RectTransform _rectTransform;
    public Button Button=>_button;


    public  void ChangeButtonText(string text,bool isByed)
    {
        if (isByed)
        {
            _text.GetComponent<RectTransform>().sizeDelta = new Vector2(_targetWidth, _rectTransform.sizeDelta.y);
        }
        _text.text = text;
    }
    
}
