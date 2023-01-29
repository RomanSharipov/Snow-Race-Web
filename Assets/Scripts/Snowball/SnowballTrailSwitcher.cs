using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballTrailSwitcher : MonoBehaviour
{
    [SerializeField] Snowball _snowball;

    public void OnExiFromPlane()
    {
        _snowball.OnExiFromPlane();
    }
    public void OnEnterOnPlane()
    {
        _snowball.OnEnterOnPlane();
    }
}
