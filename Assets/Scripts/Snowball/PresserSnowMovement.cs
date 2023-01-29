using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresserSnowMovement 
{
    private Transform _snowballRotation;
    private Transform _PresserSnow;

    public PresserSnowMovement(Transform snowballRotation, Transform presserSnow)
    {
        _snowballRotation = snowballRotation;
        _PresserSnow = presserSnow;
    }

    public void Move()
    {
        _PresserSnow.position = new Vector3(_snowballRotation.position.x, _PresserSnow.position.y, _snowballRotation.position.z);
    }
}
