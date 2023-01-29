using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public  class VideoButton : ShopButton
{
    [SerializeField] private Image icon;
    [SerializeField] private int _targetWidth;
    [SerializeField] private RectTransform _rectTransform;

    public override void ChangeButtonText(string text,bool isByed)
    {
        if (isByed)
        {
            icon.gameObject.SetActive(false);
            _text.GetComponent<RectTransform>().sizeDelta = new Vector2(_targetWidth, _rectTransform.sizeDelta.y);
        }
        
        _text.text = text;
    }
    
}
