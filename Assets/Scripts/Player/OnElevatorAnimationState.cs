using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class OnElevatorAnimationState : MonoBehaviour
{
    [SerializeField] private Stickman _stickman;
    [SerializeField] private Transform _upperPoint;
    [SerializeField] private Transform _snowballScalling;
    [SerializeField] private MonoBehaviour _animatorHoldSnowballBehaviour;
    [SerializeField] private Player _player;

    private IAnimatorHoldSnowball _animatorHoldSnowball => (IAnimatorHoldSnowball)_animatorHoldSnowballBehaviour;

    private Vector3 _lowerPoint;
    private Vector3 _startPositions;

    private void OnValidate()
    {
        if (_animatorHoldSnowballBehaviour is IAnimatorHoldSnowball)
            return;

        _animatorHoldSnowballBehaviour = null;
    }

    private void Awake()
    {
        _stickman.EnterdOnElevator += SwitchOnState;
        _stickman.RiseUpOnElevatorIsOver += SwitchOffState;
        _startPositions = _snowballScalling.localPosition;
        _lowerPoint = _startPositions;
    }

    private void OnEnabled()
    {
        _player.Disabled += OnPlayerDisabled;

    }

    private void OnDisabled()
    {
        _player.Disabled -= OnPlayerDisabled;
    }
    
    private void Update()
    {
        _animatorHoldSnowball.HoldSnowBallInHand(_stickman.Snowball.NormalizedScale);
        _snowballScalling.localPosition = Vector3.Lerp(_lowerPoint, _upperPoint.localPosition, _stickman.Snowball.NormalizedScale);
    }

    private void SwitchOnState()
    {
        enabled = true;
    }

    private void SwitchOffState()
    {
        _snowballScalling.localPosition = _startPositions;
        _animatorHoldSnowball.HoldoffSnowBall();
        enabled = false;
    }

    private void OnPlayerDisabled()
    {
         _stickman.EnterdOnElevator -= SwitchOnState;
        _stickman.RiseUpOnElevatorIsOver -= SwitchOffState;
    }
}
