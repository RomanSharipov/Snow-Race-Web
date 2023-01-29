using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class LoadImageChanger : MonoBehaviour
{
    [SerializeField] private Sprite _16X9Sprite;
    [SerializeField] private Sprite _9x16Sprite;
    [SerializeField] private Image _image;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;

        if (_camera.aspect>=1.7)
        {
            _image.sprite = _16X9Sprite;
        }
        else
        {
            _image.sprite = _9x16Sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
