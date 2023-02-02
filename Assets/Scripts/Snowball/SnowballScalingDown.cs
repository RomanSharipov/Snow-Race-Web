using System;
using UnityEngine;

public class SnowballScalingDown : ISnowballScalable
{
    private float _minSize;
    private Transform _transform;
    private float _speed;

    private float _zeroStage = 1;
    private float _lowerStage = 2.0f;
    private float _middleStage = 3.0f;
    private float _bigStage = 4.0f;

    public bool _isAlreadyWasScaledDownSmallSize;
    public bool _isAlreadyWasScaledDownMiddleSize;
    public bool _isAlreadySnowballBecomesZero;

    public event Action WasScaledDownSmallSize;
    public event Action WasScaledDownZeroSizelSize;
    public event Action WasScaledDownMiddleSize;
    public event Action SnowballBecomesZero;

    public SnowballScalingDown(float minSize, Transform transform, float speed)
    {
        _minSize = minSize;
        _transform = transform;
        _speed = -speed;
        SetStartStages();
    }


    public void ChangeScale()
    {
        if (_transform.localScale == Vector3.zero)
        {
            WasScaledDownZeroSizelSize?.Invoke();
            return;
        }

        if (_transform.localScale.x < _minSize && _isAlreadySnowballBecomesZero == false)
        {
            _transform.localScale = Vector3.zero;
            SnowballBecomesZero?.Invoke();
            _isAlreadySnowballBecomesZero = true;
            return;
        }

        if (_transform.localScale.x < _middleStage && _isAlreadyWasScaledDownMiddleSize == false)
        {
            WasScaledDownMiddleSize?.Invoke();
            _isAlreadyWasScaledDownMiddleSize = true;
        }

        if (_transform.localScale.x < _lowerStage && _isAlreadyWasScaledDownSmallSize == false)
        {
            WasScaledDownSmallSize?.Invoke();
            _isAlreadyWasScaledDownSmallSize = true;
        }
        

        _transform.localScale += new Vector3(_speed * Time.deltaTime, _speed * Time.deltaTime, _speed * Time.deltaTime);
    }

    public void SetStartStages()
    {
        _isAlreadyWasScaledDownSmallSize = false;
        _isAlreadyWasScaledDownMiddleSize = false;
        _isAlreadySnowballBecomesZero = false;
    }
}
