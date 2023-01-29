
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BafsHandler : MonoBehaviour
{
   [SerializeField] private Player _player;
   [SerializeField] private BafIcon _skiIcon;
   [SerializeField] private int _bafDuration;
   [SerializeField] private List<GameObject> _ski = new List<GameObject>();
   [SerializeField] private InputOnPlane _inputOnPlane;
   [SerializeField] private InputOnBridge _inputOnBridge;
   [SerializeField] private InputOnElevator _inputOnElevator;
   [SerializeField] private float _speedOnSki;
    [SerializeField] private ParticleSystem _skiParticle;
    [SerializeField] private Animator _playerAnimator;

   private Coroutine _skiCoroutine;


   private bool _skiUsed = false;
   private bool _superFastRunUsed;

    public bool SkiUsed => _skiUsed;

   public void UseSki()
   {
       if ( _skiUsed)
       {
          StopCoroutine(_skiCoroutine);
       }
        _skiUsed = true;
        _skiIcon.gameObject.SetActive(true);
        _skiCoroutine = StartCoroutine(StopSki());
        _skiIcon.StartReference(_bafDuration);
        TurnSki(true);
        ChangeSpeed(_speedOnSki);
   }

   public void ChangeAnimator(Animator animator)
   {
       _playerAnimator = animator;
   }

   private IEnumerator StopSki()
   {
        _playerAnimator.Play("SkiRide");
        _skiParticle.Play();
       yield return new  WaitForSeconds(_bafDuration);
        _playerAnimator.Play("Blend Tree");
        _skiParticle.Stop();
       _skiUsed = false;
       _skiIcon.StopReference();
       TurnSki(false);
       ChangeSpeed();
       
    }

   private void TurnSki(bool activeSelf)
   {
       for (int i = 0; i < _ski.Count; i++)
       {
           _ski[i].SetActive(activeSelf);
       }
   }

   private void ChangeSpeed(float speed=0)
   {
       if (speed==0)
       {
           _inputOnBridge.ReturnStartSpeed();
           _inputOnElevator.ReturnStartSpeed();
           _inputOnPlane.ReturnStartSpeed();
       }
       else
       {
           _inputOnBridge.ChangeSpeed(speed);
           _inputOnElevator.ChangeSpeed(speed);
           _inputOnPlane.ChangeSpeed(speed);
       }
   }

   public void ChangeSki(List<GameObject> ski)
   {
       _ski = ski;
   }
}
