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

    [Header("Snowball Scaler")]
    [SerializeField] private SnowballScaler _snowballScaler;
    [SerializeField] private GameObject _snowballScalingMesh;
    [SerializeField] private Transform _stickmanPositionMax;

    [SerializeField] private SnowballCollisionAttackHandler _snowballCollisionAttackHandler;
    [SerializeField] private SnowTrail _snowTrail;
    [SerializeField] private ParticleSystem _snowParticle;
    
    private SnowballRotation _snowballRotation;
    private bool _onPlane;

    public event Action WasScaledDownSmallSize;
    public event Action SnowballBecomesZero;
    public event Action WasScaledUpMiddleSize;
    public event Action WasScaledDownMiddleSize;

    public float Scale => _snowballScaler.Scale;
    public float NormalizedScale => _snowballScaler.NormalizedScale;
    public GameObject StickmanPosition => _stickmanPosition;
    public SnowballCollisionAttackHandler SnowballCollisionAttackHandler => _snowballCollisionAttackHandler;
    public Transform StickmanPositionMax => _stickmanPositionMax;
    public SnowballScaler SnowballScaler => _snowballScaler;

    public event UnityAction Stoped; 
    
    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
    }
    
    public void Init()
    {
        _snowballRotation = new SnowballRotation(_snowballMesh.transform, _rotateSpeed);
        _snowballScaler = new SnowballScaler(_snowballScalingMesh, _stickmanPositionMax);
        _snowballScaler.SnowballScalingUpToLittleStage += SwitchOnSnowTrail;
        _snowballScaler.SnowballBecomesZero += OnSnowballBecomesZero;
        SnowballScaler.SetScallingUpMode();
        SwitchOffSnowTrail();
    }

    public void InitSplineComputer(SplineComputer splineComputer)
    {
        _splineFollower.spline = splineComputer;
        _splineFollower.follow = true;
        SnowballScaler.SetUnRollModeForFinishLine();
        StartCoroutine(Rolling());
    }

    private IEnumerator Rolling()
    {
        SwitchOffSnowTrail();
        
        while (_snowballScaler.SnowballGreaterSmallSize)
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
        _snowballScaler.ChangeScale();
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
        SwitchOnSnowTrail();
    }

    private void OnDisable()
    {
        _snowballScaler.UnSubscribeEvents();
        _snowballScaler.SnowballScalingUpToLittleStage -= SwitchOnSnowTrail;
        _snowballScaler.SnowballBecomesZero -= OnSnowballBecomesZero;
    }

    private void OnSnowballBecomesZero()
    {
        SwitchOffSnowTrail();
        SnowballBecomesZero?.Invoke();
    }

    public void SetZeroScale()
    {
        _snowballScaler.SetZeroScale();
        SwitchOffSnowTrail();
    }

    public void SetMaxSize()
    {
        _snowballScaler.SetMaxSize();
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
