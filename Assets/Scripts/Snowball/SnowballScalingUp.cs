using System;
using UnityEngine;

public class SnowballScalingUp : ISnowballScalable
{
    private float _maxSize;
    private Transform _transform;
    private float _speed;

    private float _lowerStage = 1.0f;
    private float _middleStage = 2.0f;
    private float _bigStage = 4.0f;

    public event Action WasScaledUpSmallSize;
    public event Action WasScaledUpMiddleSize;
    public event Action WasScaledUpBigSize;

    public bool _isAlreadyPassedLowerStage;
    public bool _isAlreadyPassedMiddleStage;
    public bool _isAlreadyPassedBigStage;

    public SnowballScalingUp(float maxSize, Transform transform, float speed)
    {
        _maxSize = maxSize;
        _transform = transform;
        _speed = speed;
        SetStartStages();
    }

    public void ChangeScale()
    {
        if (_transform.localScale.x > _maxSize)
            return;

        if (_transform.localScale.x > _lowerStage && _isAlreadyPassedLowerStage == false)
        {
            WasScaledUpSmallSize?.Invoke();
            _isAlreadyPassedLowerStage = true;
        }

        if (_transform.localScale.x > _middleStage && _isAlreadyPassedMiddleStage == false)
        {
            WasScaledUpMiddleSize?.Invoke();
            _isAlreadyPassedMiddleStage = true;

        }

        if (_transform.localScale.x > _bigStage && _isAlreadyPassedBigStage == false)
        {
            WasScaledUpBigSize?.Invoke();
            _isAlreadyPassedBigStage = true;
        }

        _transform.localScale += new Vector3(_speed * Time.deltaTime, _speed * Time.deltaTime, _speed * Time.deltaTime);
        
    }

    public void SetStartStages()
    {
        _isAlreadyPassedLowerStage = false;
        _isAlreadyPassedMiddleStage = false;
        _isAlreadyPassedBigStage = false;
    }
}
