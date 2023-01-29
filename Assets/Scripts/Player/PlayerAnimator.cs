using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IAnimatorHoldSnowball
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    public event Action Idled;
    
    private void OnEnable()
    {
        _player.PlayerStopped += Idle;
        _player.Walked += Walk;
        _player.WasTakenDamage += Fall;
        //_player.EnteredOnLawaColdier+=RunOnTheBall;
        //_player.ExitOnLawaColdier+=StopRunOnTheBall;
        _player.EnteredOnWaterColdier += OnEnteredOnWaterColdier;
        _player.ExitOnWaterColdier += OnExitOnWaterColdier;
        _player.SettedOnSpringBoard += OnSetOnSpringBoard;
        _player.Landed += Stand;
        _player.JumpedTuCart += OnJumpedTuCart;
        _player.StopedFolowCart += OnStopedFolowCart;
        _player.Wined += Idle;

    }

    private void Walk(float height)
    {
        _animator.SetFloat(AnimatorConstants.Height, height);
        _animator.SetBool(AnimatorConstants.Walk, true);
    }

    public void HoldSnowBallInHand(float height)
    {
        _animator.SetFloat(AnimatorConstants.HoldSnowBallInHandHeight, height);
        _animator.SetBool(AnimatorConstants.HoldSnowBallInHand, true);
    }

    public void HoldoffSnowBall()
    {
        _animator.SetBool(AnimatorConstants.HoldSnowBallInHand, false);
    }

    private void Idle()
    {
        _animator.SetBool(AnimatorConstants.Walk,false);
        Idled?.Invoke();
    }

    private void Fall()
    {
        _animator.SetTrigger(AnimatorConstants.Fall);
    }

    private void NinjaIdle()
    {
        if (_animator.GetBool(AnimatorConstants.OnWatter))
        {
            _animator.SetBool(AnimatorConstants.OnWatter,false);
        }
        else
        {
            _animator.SetBool(AnimatorConstants.OnWatter,true);
        }
      
    }

    private void OnEnteredOnWaterColdier()
    {
        _animator.SetBool(AnimatorConstants.OnWatter, true);
    }

    private void OnExitOnWaterColdier()
    {
        _animator.SetBool(AnimatorConstants.OnWatter, false);
    }

    private void RunOnTheBall()
    {
        Debug.Log("вызвалось");
        
            _animator.SetBool(AnimatorConstants.BackWalk,true);
    }
    
    private void StopRunOnTheBall()
    {
        _animator.SetBool(AnimatorConstants.BackWalk,false);
    }

    private void OnSetOnSpringBoard()
    {
        _animator.SetTrigger(AnimatorConstants.OnSetOnSpringBoard);
    }

    private void Stand()
    {
        _animator.SetTrigger(AnimatorConstants.Stand);
    }

    private void OnDisable()
    {
        _player.PlayerStopped -= Idle;
        _player.Walked -= Walk;
        _player.WasTakenDamage -= Fall;
       // _player.EnteredOnLawaColdier-=RunOnTheBall;
        //_player.ExitOnLawaColdier-=StopRunOnTheBall;
        _player.EnteredOnWaterColdier -= OnEnteredOnWaterColdier;
        _player.ExitOnWaterColdier -= OnExitOnWaterColdier;
        _player.SettedOnSpringBoard -= OnSetOnSpringBoard;
        _player.Landed -= Stand;
        _player.JumpedTuCart -= OnJumpedTuCart;
        _player.StopedFolowCart -= OnStopedFolowCart;
        _player.Wined -= Idle;
    }

    public void ChangeAnimator(Animator animator)
    {
        _animator = animator;
    }

    private void OnJumpedTuCart()
    {
        _animator.SetTrigger(AnimatorConstants.OnJumpedTuCart);
    }

    private void OnStopedFolowCart()
    {
        _animator.SetTrigger(AnimatorConstants.OnStopedFolowCart);
    }

}
