using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class ElevatorAnimaotr : MonoBehaviour
{
    [SerializeField]private Animator _animator;

    private Platform _platform;
    
    private void Awake()
    {
        _platform = GetComponent<Platform>();
    }

    private void OnEnable()
    {
        _platform.RiseWasOver += OpeBackDoor;
        _platform.GotUp += ClodeFFrontDoor;
    }

    private void OnDisable()
    {
        _platform.RiseWasOver -= OpeBackDoor;
        _platform.GotUp -= ClodeFFrontDoor;
    }

    private void OpeBackDoor()
    {
        _animator.SetTrigger(AnimatorConstants.OpenBackDoor);
    }

    private void ClodeFFrontDoor()
    {
        _animator.SetTrigger(AnimatorConstants.CloseFrontDoor); 
    }
}
