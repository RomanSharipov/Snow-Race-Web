using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            stickman.OnEnterOnPlane();
        }

        if (other.TryGetComponent(out SnowballTrailSwitcher snowballTrailSwitcher))
        {
            snowballTrailSwitcher.OnEnterOnPlane();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Snowball snowball))
        {
            snowball.SwitchOffSnowTrail();
            snowball.TrySetZeroMode();
        }

        if (other.TryGetComponent(out Stickman stickman))
        {
            stickman.OnExitFromPlane();

        }
        if (other.TryGetComponent(out SnowballTrailSwitcher snowballTrailSwitcher))
        {
            snowballTrailSwitcher.OnExiFromPlane();
        }
    }
}
