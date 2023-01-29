using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballCollisionAttackHandler : MonoBehaviour
{
    [SerializeField] private Snowball _snowball;
    [SerializeField] private Stickman _stickmanSelf;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            _stickmanSelf.GetDamage(_snowball);
            stickman.TakeDamage(_snowball);
        }
    }
}
