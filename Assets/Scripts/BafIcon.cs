using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

public class BafIcon : MonoBehaviour
{
   [SerializeField] private Image _imageForReference;
   [SerializeField] private CanvasGroup _canvasGroup;

   private Tweener twiner;

   public void StartReference(int duration)
   {
       if (_imageForReference.fillAmount!=1)
       {
           Reset();
            return;
       }
      _canvasGroup.enabled=false;
     twiner= _imageForReference.DOFillAmount(0, duration);
   }

    public void StopReference()
   {
      _canvasGroup.enabled = true;
        Reset();
   }

    private void Reset()
    {
        _imageForReference.fillAmount = 1;
        twiner.Restart();
    }
}
