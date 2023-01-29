using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineFollower))]
public class ChangeSplineFolowSpeed : MonoBehaviour
{
    private SplineFollower _splineFollower;

    private float _startSpeed;
    
    private void Awake()
    {
        _splineFollower = GetComponent<SplineFollower>();
        _startSpeed = _splineFollower.followSpeed;
    }

    public void Change()
    {
        _splineFollower.followSpeed = 30;
    }

    public void ReturnStartSpeed()
    {
        _splineFollower.followSpeed = _startSpeed;
    }
}
