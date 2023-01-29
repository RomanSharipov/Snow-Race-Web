using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballRotation 
{
    private Transform _transform;
    private float _speedRotate;

    public SnowballRotation(Transform transform, float speedRotate)
    {
        _speedRotate = speedRotate;
        _transform = transform;
    }

    public void Rotate()
    {
        _transform.rotation *= Quaternion.Euler(_speedRotate * Time.deltaTime,0,0);
    }
}
