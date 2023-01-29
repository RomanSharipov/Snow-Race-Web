using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Snowball : MonoBehaviour
{
    [Header("Rotating object(Child)")]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private GameObject _snowballMesh;
    [SerializeField] private SplineFollower _splineFollower;
    [SerializeField] private GameObject _stickmanPosition;

    [Header("Scaling object(Child)")]
    [SerializeField] private float _scaleUpSpeed;
    [SerializeField] private float _scaleDownSpeed;
    [SerializeField] private float _scaleDownSpeedForFinish;
    [SerializeField] private GameObject _snowballScalingMesh;
    [SerializeField] private Transform _stickmanPositionMax;
   

    [Header("Size snowball settings")]
    [SerializeField] private float _maxSize;
    [SerializeField] private float _minSize;

    [SerializeField] private SnowballCollisionAttackHandler _snowballCollisionAttackHandler;
    [SerializeField] private SnowTrail _snowTrail;
    [SerializeField] private ParticleSystem _snowParticle;
    
    private SnowballRotation _snowballRotation;
    private bool _onPlane;

    private ISnowballScalable _snowballCurrentScalingMode;
    private SnowballScalingUp _snowballScalingUp;
    private SnowballScalingDown _snowballScalingDown;
    private SnowballScalingDown _snowballScalingDownForFinish;
    private SnowballScalingZero _snowballScalingZero;

    public event Action WasScaledDownSmallSize;
    public event Action SnowballBecomesZero;
    public event Action WasScaledUpMiddleSize;
    public event Action WasScaledDownMiddleSize;

    public float Scale => _snowballScalingMesh.transform.localScale.x;
    public float NormalizedScale => Scale / _maxSize;
    public GameObject StickmanPosition => _stickmanPosition;
    public SnowballCollisionAttackHandler SnowballCollisionAttackHandler => _snowballCollisionAttackHandler;
    public Transform StickmanPositionMax => _stickmanPositionMax;

    public event UnityAction Stoped; 
    
    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
    }
    
    public void Init()
    {
        _snowballRotation = new SnowballRotation(_snowballMesh.transform, _rotateSpeed);
        _snowballScalingUp = new SnowballScalingUp(_maxSize, _snowballScalingMesh.transform, _scaleUpSpeed);
        _snowballScalingUp.WasScaledUpSmallSize += LittleStage;
        _snowballScalingDown = new SnowballScalingDown(_minSize, _snowballScalingMesh.transform, _scaleDownSpeed);
        _snowballScalingDownForFinish = new SnowballScalingDown(_minSize, _snowballScalingMesh.transform, _scaleDownSpeedForFinish);
        _snowballScalingDown.SnowballBecomesZero += OnSnowballBecomesZero;
        _snowballScalingZero = new SnowballScalingZero();
        _snowballCurrentScalingMode = _snowballScalingUp;
        _snowballScalingDown.WasScaledDownZeroSizelSize += OnScaledDownSmallSize;
        SetRollMode();
        SwitchOffSnowTrail();
    }

    public void InitSplineComputer(SplineComputer splineComputer)
    {
        _splineFollower.spline = splineComputer;
        _splineFollower.follow = true;
        SetUnRollModeForFinish();
        StartCoroutine(Rolling());
    }

    private IEnumerator Rolling()
    {
        SwitchOffSnowTrail();
        
        while (_snowballScalingMesh.transform.localScale.x>_minSize)
        {
            Roll();
            yield return null;
        }

        _splineFollower.follow = false;
        Stoped?.Invoke();
    }
    
    public void Roll()
    {
        _snowParticle.Play();
        _snowballRotation.Rotate();
        _snowballCurrentScalingMode.ChangeScale();
    }

    public void SetRollMode()
    {
        _snowballScalingUp.SetStartStages();
        _snowballCurrentScalingMode = _snowballScalingUp;
    }

    public void SetUnRollMode()
    {
        _snowballScalingDown.SetStartStages();
        _snowballCurrentScalingMode = _snowballScalingDown;
    }

    public void SetZeroMode()
    {
        _snowballCurrentScalingMode = _snowballScalingZero;
    }

    public void TrySetZeroMode()
    {
        if (_snowballCurrentScalingMode == _snowballScalingDown)
            return;

        SetZeroMode();
    }

    public void SetUnRollModeForFinish()
    {
        _snowballCurrentScalingMode = _snowballScalingDownForFinish;
    }
    
    public void SwitchOffSnowTrail()
    {
        _snowTrail.SwithOffTrail();
    }

    public void SwitchOnSnowTrail()
    {
        _snowTrail.SwithOnTrail();
    }

    private void LittleStage()
    {
        if (_onPlane)
        {
            SwitchOnSnowTrail();
        }
    }

    private void OnDisable()
    {
        _snowballScalingUp.WasScaledUpSmallSize -= LittleStage;
        _snowballScalingDown.SnowballBecomesZero -= OnSnowballBecomesZero;
        _snowballScalingDown.WasScaledDownZeroSizelSize += OnScaledDownSmallSize;
    }

    private void OnScaledDownSmallSize()
    {
        WasScaledDownSmallSize?.Invoke();
    }
    
    private void OnSnowballBecomesZero()
    {
        SwitchOffSnowTrail();
        SnowballBecomesZero?.Invoke();
    }

    public void SetZeroScale()
    {
        _snowballScalingMesh.transform.localScale = Vector3.zero;
        _snowballScalingUp.SetStartStages();
        SwitchOffSnowTrail();
    }

    public void SetMaxSize()
    {
        _snowballScalingMesh.transform.localScale = new Vector3(_maxSize, _maxSize, _maxSize);
        SwitchOnSnowTrail();
    }

    public void OnEnterOnPlane()
    {
        _onPlane = true;

        if (Scale > 1)
        {
            SwitchOnSnowTrail();
        }
    }

    public void OnExiFromPlane()
    {
        _onPlane = false;
        SwitchOffSnowTrail();
    }
}
