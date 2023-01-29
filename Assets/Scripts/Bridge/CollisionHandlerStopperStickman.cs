using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlerStopperStickman : MonoBehaviour
{
    public event Action SnowPathWasFullyBuilt;

    [SerializeField] private StopperStickman _stopperStickman;
    [SerializeField] private float _currentDistance;
    [SerializeField] private float _mixDistance = 1.0f;

    private void Update()
    {
        _currentDistance = Vector3.Distance(_stopperStickman.transform.position, transform.position);

        if (_currentDistance < 1.0f)
        {
            SnowPathWasFullyBuilt?.Invoke();
        }
    }
}
