using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineComputer))]
public class SpringBoardCollsionHnadler : MonoBehaviour
{
    [SerializeField] private SplineComputer _splineComputer;

    private void Awake()
    {
        _splineComputer = GetComponent<SplineComputer>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent( out Stickman stickman))
        {
            stickman.FolowSpringBoard(_splineComputer);
        }
    }
}
