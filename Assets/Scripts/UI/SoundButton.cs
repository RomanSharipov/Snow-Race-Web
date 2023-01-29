using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class SoundButton : MonoBehaviour
{
   [SerializeField] private AudioHandler _audioHandler;
   [SerializeField] private Button _button;
   [SerializeField] private Image _image;
   [SerializeField] private Sprite _enabledIcon;
   [SerializeField] private Sprite _disabledIcon;

   private void Awake()
   {
      ChangeButtonImage();
   }
   
   private void OnEnable()
   {
      _button.onClick.AddListener(OnButtonClick);
   }

   private void OnDisable()
   {
      _button.onClick.RemoveListener(OnButtonClick);
   }

   private void ChangeButtonImage()
   {
      if (Convert.ToBoolean(PlayerPrefs.GetInt(Constants.AudioListenerKey,1)))
      {
         _image.sprite = _enabledIcon;
      }
      else
      {
         _image.sprite = _disabledIcon;
      }
   }
   
   private void OnButtonClick()
   {
      _audioHandler.ChangeAudioListenerState();
      ChangeButtonImage();
   }
}
