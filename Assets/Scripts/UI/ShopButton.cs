using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopButton : MonoBehaviour
{
    [SerializeField] protected  TextMeshProUGUI _text;
    [SerializeField] protected Button _button;

    public Button Button => _button;

    public virtual void ChangeButtonText(string text,bool isByed)
    {
        
    }
}
