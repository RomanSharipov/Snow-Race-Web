using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineFollower))]
public abstract class Stickman : MonoBehaviour
{
    [SerializeField] private GameObject _skelet;
    [SerializeField] private Snowball _snowball;
    [SerializeField] private Transform _skins;
    [SerializeField] private SplineComputer _splineComputer;
    

    private SplineFollower _splineFollower;
    public Snowball Snowball => _snowball;
    public GameObject Skelet => _skelet;

    private bool _onWaterBridge;
    private const int JumpPower = 3;
    private const int JumpDuration=1;
    private Vector3 _skeletStartPosition  = new Vector3(0,0.3f,0);
    private Platform _platform;

    private Coroutine _stayOnSnowballJob;
    public event Action EnterdOnElevator;
    public event Action ExitOnElevator;
    public event Action ExitOnWaterColdier;
    public event Action EnteredOnWaterColdier;
    public event Action ExitOnLawaColdier;
    public event Action ITakeDamage;
    public event Action IGetDamage;
    public event Action Landed;
    public event Action SettedOnSpringBoard;
    public event Action RiseUpOnElevatorIsOver;
    public event Action StopedFolowCart;
    public event Action JumpedTuCart;

    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
    }



    protected void Start()
    {
        _skeletStartPosition = _skelet.transform.localPosition;
        _snowball.Init();
    }

    public abstract void OnEnterOnBridge();

    public abstract void OnExitFromBridge();
    public abstract void OnExitOnElevator(Platform platform);
    

    public abstract void OnEnterOnSpringBoard();

    public abstract void OnEnterOnPlane();

    public abstract void OnExitFromPlane();

    public abstract void OnTakeDamage();

    public virtual void OnEnterOnElevator(Platform platform)
    {
        transform.parent = platform.transform;
        _platform = platform;
        _platform.RiseWasOver += OnPlatformRiseWasOver;
        Snowball.SwitchOffSnowTrail();
        EnterdOnElevator?.Invoke();
    }

    public virtual void OnPlatformRiseWasOver()
    {
        transform.parent = null;
        RiseUpOnElevatorIsOver?.Invoke();
    }

    public virtual void OnEndedFolowSpringBoard()
    {
        _splineFollower.follow = false;
        _snowball.SwitchOnSnowTrail();
        _splineFollower.spline = null;
        _splineFollower.enabled = false;
        ResetRotation();
        _splineComputer = null;


    }

    public virtual void FolowSpringBoard(SplineComputer splineComputer)
    {
        if (_splineComputer == null)
        {
            _splineComputer = splineComputer;
            _splineFollower.SetPercent(0);
            Folow();
            OnEnterOnSpringBoard();
            _snowball.SwitchOffSnowTrail();
            SettedOnSpringBoard?.Invoke();
        }
    }

    public virtual void OnStopFolowCart()
    {
        transform.parent = null;
        _snowball.SwitchOnSnowTrail();
        ResetRotation();
        StopedFolowCart?.Invoke();
    }

    public virtual void JumpTuCart(Transform cartPosition,SplineComputer splineComputer,Cart cart)
    {
        _snowball.SwitchOffSnowTrail();
        StartCoroutine(JumpTuCartCorrutine(cartPosition, splineComputer, cart));
        JumpedTuCart?.Invoke();
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }
    
    public void TakeDamage(Snowball enemySnowball)
    {
        if (enemySnowball.Scale > Snowball.Scale)
        {
            OnTakeDamage();
            ITakeDamage?.Invoke();
        }
    }

    protected void ChangeSkelet(GameObject skelet)
    {
        _skelet = skelet;
    }
    
    public void GetDamage(Snowball enemySnowball)
    { 
        IGetDamage?.Invoke();
    }

    public void OnEnterOnWaterBridge()
    {
        EnteredOnWaterColdier?.Invoke();
    }
    
    public void OnExitOnWaterBridge()
    {
        ExitOnWaterColdier?.Invoke();
        _skelet.transform.localPosition = _skeletStartPosition;
    }

    public void JumpOffTheBall()
    {
        if (_stayOnSnowballJob != null)
        {
            StopCoroutine(_stayOnSnowballJob);
        }
        _stayOnSnowballJob = null;

        _skins.localPosition = _skeletStartPosition;
    }

    private void Folow()
    {
        _splineFollower.enabled = true;
        _splineFollower.follow = true;
        _splineFollower.spline = _splineComputer;
    }
    
    public void JumpOnSnowBall()
    {
        _stayOnSnowballJob = StartCoroutine(StayOnSnowball());
    }

    
    private IEnumerator JumpTuCartCorrutine(Transform cartPosition,SplineComputer splineComputer,Cart cart)
    {
        transform.DOJump(cartPosition.position, JumpPower, 1, JumpDuration);
        yield return new WaitForSeconds(1);
        cart.StartFolow();
    }

    private IEnumerator StayOnSnowball()
    {
        while (enabled)
        {
            _skins.transform.localPosition = Vector3.Lerp(Vector3.zero, _snowball.StickmanPositionMax.localPosition, _snowball.NormalizedScale);
            yield return null;
        }
    }

    public void Land()
    {
        Landed?.Invoke();
    }

    public void ResetSkeletonPositions()
    {
        _skelet.transform.localPosition = _skeletStartPosition;
    }

    private void OnDisable()
    {
        if (_platform == null)
            return;

        _platform.RiseWasOver -= OnPlatformRiseWasOver;
    }
}
