using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class TurnOnTap : MonoBehaviour
{ 
    [SerializeField] private GameObject _panelWithControlElements;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
    
    private void OnButtonClick()
    {
        if (_panelWithControlElements.activeSelf)
        {
            _panelWithControlElements.SetActive(false);
        }
        else
        {
            _panelWithControlElements.SetActive(true);
        }
    }
}