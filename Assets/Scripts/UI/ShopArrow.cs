using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopArrow : MonoBehaviour
{
   [SerializeField] private Scrollbar _scrollbar;
   [SerializeField] private CanvasGroup _canvasGroup;

   private const float _scrolbarValueToDisableArrow = 0.95f;

   private void OnEnable()
   {
      _scrollbar.onValueChanged.AddListener(OnScrollBarValueChanged);
   }
   
   private void OnDisable()
   {
      _scrollbar.onValueChanged.RemoveListener(OnScrollBarValueChanged);
   }

   private void OnScrollBarValueChanged(float value)
   {
      if (value<_scrolbarValueToDisableArrow&&_canvasGroup.enabled)
      {
         _canvasGroup.enabled = false;
      }
      else if (value>_scrolbarValueToDisableArrow&&_canvasGroup.enabled==false)
      {
         _canvasGroup.enabled = true;
      }
   }


}
