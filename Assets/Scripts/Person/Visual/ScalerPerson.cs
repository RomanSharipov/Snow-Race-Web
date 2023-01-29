using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalerPerson : MonoBehaviour
{
    [SerializeField] private float _maxScale;
    [SerializeField] private float _minScale;
    [SerializeField] private float _speedChangedBlendShape;
    [SerializeField] private float _durationChangedScale;
    [SerializeField] private float _maxBlendShape;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    private Coroutine _coroutine;
    private float _currentBlendShape;

    public float MaxScale => _maxScale;

    public void Increase()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        transform.DOScale(_maxScale, _durationChangedScale);

        _coroutine = StartCoroutine(ChangeScale(_maxBlendShape));
    }

    public void Decrease()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        transform.DOScale(_minScale, _durationChangedScale);

        _coroutine = StartCoroutine(ChangeScale(0f));
    }

    public void Change(float targetBlendShape, float deltaTime)
    {
        _currentBlendShape = Mathf.MoveTowards(_currentBlendShape, targetBlendShape,
            (_speedChangedBlendShape * 200) * deltaTime);
        SetBlendShape(_currentBlendShape);
    }

    private IEnumerator ChangeScale(float targetBlendShape)
    {
        while (_currentBlendShape != targetBlendShape)
        {
            Change(targetBlendShape, Time.deltaTime);
            yield return null;
        }
    }

    public void SetBlendShape(float value)
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(0, value);
        _currentBlendShape = value;
    }
}
