using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanSnandUpHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            stickman.Land();
        }
    }
}
