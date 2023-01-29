using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class EnemyAnimator : MonoBehaviour, IAnimatorHoldSnowball
{
    [SerializeField] Enemy _enemy;
    [SerializeField] Animator _animator;

    private void OnEnable()
    {
        _enemy.Walked += Walk;
        _enemy.WasTakenDamage += Fall;
        _enemy.TriedWin += WinDance;
        _enemy.EnteredOnWaterColdier += OnEnteredOnWaterColdier;
        _enemy.ExitOnWaterColdier += OnExitOnWaterColdier;
        _enemy.SettedOnSpringBoard += OnSetOnSpringBoard;
        _enemy.Landed += Stand;
        _enemy.JumpedTuCart += OnJumpedTuCart;
        _enemy.StopedFolowCart += OnStopedFolowCart;

    }

    private void Walk(float height)
    {
        _animator.SetFloat(AnimatorConstants.Height, height);
    }

    private void OnDisable()
    {
        _enemy.Walked -= Walk;
        _enemy.WasTakenDamage -= Fall;
        _enemy.TriedWin -= WinDance;
        _enemy.EnteredOnWaterColdier -= OnEnteredOnWaterColdier;
        _enemy.ExitOnWaterColdier -= OnExitOnWaterColdier;
        _enemy.SettedOnSpringBoard -= OnSetOnSpringBoard;
        _enemy.Landed -= Stand;
        _enemy.JumpedTuCart -= OnJumpedTuCart;
        _enemy.StopedFolowCart -= OnStopedFolowCart;
    }

    private void Fall()
    {
        _animator.SetTrigger(AnimatorConstants.Fall);
    }

    private void WinDance()
    {
        _animator.SetTrigger(AnimatorConstants.WinDance);
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

    private void RunOnTheBall()
    {
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

    public void HoldSnowBallInHand(float height)
    {
        _animator.SetFloat(AnimatorConstants.HoldSnowBallInHandHeight, height);
        _animator.SetBool(AnimatorConstants.HoldSnowBallInHand, true);
    }

    public void HoldoffSnowBall()
    {
        _animator.SetBool(AnimatorConstants.HoldSnowBallInHand, false);
    }

    private void OnEnteredOnWaterColdier()
    {
        _animator.SetBool(AnimatorConstants.OnWatter, true);
    }

    private void OnExitOnWaterColdier()
    {
        _animator.SetBool(AnimatorConstants.OnWatter, false);
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
