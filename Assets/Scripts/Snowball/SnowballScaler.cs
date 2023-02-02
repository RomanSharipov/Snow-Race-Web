using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SnowballScaler
{
    [Header("Scaling object(Child)")]
    [SerializeField] private float _scaleUpSpeed;
    [SerializeField] private float _scaleDownSpeed;
    [SerializeField] private float _scaleDownSpeedForFinish;

    [Header("Size snowball settings")]
    [SerializeField] private float _maxSize;
    [SerializeField] private float _minSize;

    private GameObject _snowballScalingMesh;

    private Transform _stickmanPositionMax;
    private ISnowballScalable _snowballCurrentScalingMode;
    private SnowballScalingUp _snowballScalingUp;
    private SnowballScalingDown _snowballScalingDown;
    private SnowballScalingDown _snowballScalingDownForFinish;
    private SnowballScalingZero _snowballScalingZero;

    public float Scale => _snowballScalingMesh.transform.localScale.x;
    public float NormalizedScale => Scale / _maxSize;
    public Transform StickmanPositionMax => _stickmanPositionMax;
    public bool SnowballGreaterSmallSize  => _snowballScalingMesh.transform.localScale.x > _minSize;

    public event Action SnowballScalingUpToLittleStage;
    public event Action SnowballBecomesZero;

    public SnowballScaler(GameObject snowballScalingMesh,Transform stickmanPositionMax)
    {
        _scaleUpSpeed = 1.0f;
        _scaleDownSpeed = 5.0f;
        _scaleDownSpeedForFinish = 1.5f;
        _maxSize = 5.0f;
        _minSize = 1.0f;
        _stickmanPositionMax = stickmanPositionMax;
        _snowballScalingMesh = snowballScalingMesh;
        _snowballScalingUp = new SnowballScalingUp(_maxSize, _snowballScalingMesh.transform, _scaleUpSpeed);
        _snowballScalingUp.WasScaledUpSmallSize += OnWasScaledUpSmallSize;
        _snowballScalingDown = new SnowballScalingDown(_minSize, _snowballScalingMesh.transform, _scaleDownSpeed);
        _snowballScalingDownForFinish = new SnowballScalingDown(_minSize, _snowballScalingMesh.transform, _scaleDownSpeedForFinish);
        _snowballScalingDown.SnowballBecomesZero += OnSnowballBecomesZero;
        _snowballScalingZero = new SnowballScalingZero();
        _snowballCurrentScalingMode = _snowballScalingUp;
    }

    public void ChangeScale()
    {
        _snowballCurrentScalingMode.ChangeScale();
    }

    public void SetScallingUpMode()
    {
        _snowballScalingUp.SetStartStages();
        _snowballCurrentScalingMode = _snowballScalingUp;
    }

    public void SetScallingDownMode()
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

    public void SetUnRollModeForFinishLine()
    {
        _snowballCurrentScalingMode = _snowballScalingDownForFinish;
    }

    public void SetZeroScale()
    {
        _snowballScalingMesh.transform.localScale = Vector3.zero;
        _snowballScalingUp.SetStartStages();
    }

    public void UnSubscribeEvents()
    {
        _snowballScalingDown.SnowballBecomesZero -= OnSnowballBecomesZero;
        _snowballScalingUp.WasScaledUpSmallSize -= OnWasScaledUpSmallSize;
    }

    public void SetMaxSize()
    {
        _snowballScalingMesh.transform.localScale = new Vector3(_maxSize, _maxSize, _maxSize);
    }

    private void OnSnowballBecomesZero()
    {
        SnowballBecomesZero?.Invoke();
    }

    private void OnWasScaledUpSmallSize()
    {
        SnowballScalingUpToLittleStage?.Invoke(); 
    }
}
