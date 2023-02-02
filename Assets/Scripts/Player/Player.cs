using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Events;

public class Player : Stickman
{
    [SerializeField] private BafsHandler _bafsHandler;
    [SerializeField] private InputOnPlane _inputOnPlane;
    [SerializeField] private InputOnBridge _inputOnBridge;
    [SerializeField] private InputOnElevator _inputOnElevator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _minSizeForPhysicsSnowball = 1.0f;
    [SerializeField] private float _delayOnTakeDamage = 1;
    [SerializeField] private Reaction _reaction;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [Header("Landing settings")]
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _offsetOnGround;
    [SerializeField] private float _durationLanding;

    private WaitForSeconds _waitForSeconds;
    private WaitForSeconds _waitForSecondsSwitchOnInput;
    private Coroutine _startMovementWithDelay;
    private Coroutine _switchOnInputWithDelay;

    public event Action PlayerStopped;
    public event Action<float> Walked;
    public event Action WasTakenDamage;
    public event UnityAction Disabled;
    public event Action Wined;

    public Reaction Reaction => _reaction;
    public Joystick Joystick => _joystick;

    private new void Start()
    {
        base.Start();
        _inputOnPlane.Init(_joystick, _controller);
        _inputOnBridge.Init(_joystick, _controller);
        _inputOnElevator.Init(_joystick, _controller);
        SetInputOnPlane();
        
        _inputOnPlane.Stopped += OnStopped;
        _inputOnPlane.Walked += OnWalked;

        _inputOnBridge.Stopped += OnStopped;
        _inputOnBridge.Walked += OnWalked;
        _waitForSeconds = new WaitForSeconds(_delayOnTakeDamage);
        _waitForSecondsSwitchOnInput = new WaitForSeconds(_delayOnTakeDamage - 0.2f);
    }

    
    private void OnWalked()
    {
        Walked?.Invoke(Snowball.NormalizedScale);
    }

    private void OnStopped()
    {
        PlayerStopped?.Invoke();
    }

    public void SetInputOnBridge()
    {
        _inputOnPlane.enabled = false;
        _inputOnElevator.enabled = false;
        _inputOnBridge.enabled = true;
    }

    public void TurnOffAllInputs()
    {
        _inputOnPlane.enabled = false;
        _inputOnBridge.enabled = false;
        _inputOnElevator.enabled = false;
    }

    public void Init(Animator animator,GameObject skelet,List<GameObject> ski)
    {
        _playerAnimator.ChangeAnimator(animator);
        ChangeSkelet(skelet);
        _bafsHandler.ChangeSki(ski);
        _bafsHandler.ChangeAnimator(animator);
    }
    
    public void SetInputOnPlane()
    {
        _inputOnBridge.enabled = false;
        _inputOnElevator.enabled = false;
        _inputOnPlane.enabled = true;
    }

    public override void OnEnterOnBridge()
    {
        Snowball.SwitchOffSnowTrail();
        SetInputOnBridge();
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    public override void OnExitFromBridge()
    {
        if (Snowball.Scale > _minSizeForPhysicsSnowball)
        {
            Snowball.SwitchOnSnowTrail();
        }
        SetInputOnPlane();
    }

    public void UseSki()
    {
        _bafsHandler.UseSki();
    }
    
    public void SwitchOffCharacterController()
    {
        _controller.enabled = false;
    }

    public void Win()
    {
        Wined?.Invoke();
    }

    public void SwitchOnCharacterController()
    {
        _controller.enabled = true;
    }

    public void SwitchOnInput()
    {
        _inputOnPlane.enabled = true;
    }

    public void SwitchOffInput()
    {
        _inputOnPlane.enabled = false;
    }

    private void OnDisable()
    {
        _inputOnPlane.Stopped -= OnStopped;
        _inputOnPlane.Walked -= OnWalked;

        _inputOnBridge.Stopped -= OnStopped;
        _inputOnBridge.Walked -= OnWalked;
        
        Disabled?.Invoke();
    }

    public override void OnTakeDamage()
    {
        if(_bafsHandler.SkiUsed == false)
        {
            if (_startMovementWithDelay != null)
            {
                StopCoroutine(_startMovementWithDelay);
                _startMovementWithDelay = null;
            }

            if (_switchOnInputWithDelay != null)
            {
                StopCoroutine(_switchOnInputWithDelay);
                _switchOnInputWithDelay = null;
            }

            WasTakenDamage?.Invoke();
            StopMovement();
            Snowball.SetZeroScale();

            _switchOnInputWithDelay = StartCoroutine(SwitchOnInputWithDelay());
            _startMovementWithDelay = StartCoroutine(StartMovementWithDelay());
        }
    }

    private IEnumerator StartMovementWithDelay()
    {
        yield return _waitForSeconds;
        _controller.detectCollisions = true;
    }

    private IEnumerator SwitchOnInputWithDelay()
    {
        yield return _waitForSecondsSwitchOnInput;
        SwitchOnInput();
    }

    public void StopMovement()
    {
        JumpOffTheBall();
        SwitchOffInput();
        _controller.detectCollisions = false;
    }


    public override void JumpTuCart(Transform cartPosition,SplineComputer splineComputer,Cart cart)
    {
        _inputOnPlane.enabled = false;
        base.JumpTuCart(cartPosition,splineComputer,cart);
    }
    
    public override void OnEndedFolowSpringBoard()
    {
        TrySetOnGroundPosition();
        SetInputOnPlane();
        base.OnEndedFolowSpringBoard();
    }
    
    public override void OnStopFolowCart()
    {
        TrySetOnGroundPosition();
        SetInputOnPlane();
        base.OnStopFolowCart();
    }
    
    public override void OnEnterOnSpringBoard()
    {
        Snowball.SwitchOffSnowTrail();
        _inputOnPlane.enabled = false;
        _inputOnBridge.enabled = false;
        _inputOnElevator.enabled = false;
    }

    public override void OnExitOnElevator(Platform platform)
    {
        Snowball.SwitchOnSnowTrail();
        platform.RiseWasOver -= OnPlatformRiseWasOver;
    }

    public override void OnEnterOnElevator(Platform platform)
    {
        base.OnEnterOnElevator(platform);

        SwitchOffCharacterController();
        _inputOnPlane.enabled = false;
        _inputOnBridge.enabled = false;
        
    }


    public override void OnPlatformRiseWasOver()
    {
        base.OnPlatformRiseWasOver();

        SwitchOnCharacterController();
        SetInputOnPlane();
    }

    public override void OnEnterOnPlane()
    {
        SetInputOnPlane();
        Snowball.SnowballScaler.SetScallingUpMode();
    }

    public override void OnExitFromPlane()
    {
        Snowball.SnowballScaler.TrySetZeroMode();
    }


    private bool TrySetOnGroundPosition()
    {
        Ray ray = new Ray(transform.position, -Vector3.up * 100);
        RaycastHit hit;
        Vector3 newPosition = new Vector3();
        if (Physics.Raycast(ray, out hit, 100, _groundLayerMask))
        {
            newPosition = new Vector3(transform.position.x, hit.point.y + _offsetOnGround, transform.position.z);
            transform.DOMove(newPosition, _durationLanding);
            return true;
            
        }
        return false;
    }

}
