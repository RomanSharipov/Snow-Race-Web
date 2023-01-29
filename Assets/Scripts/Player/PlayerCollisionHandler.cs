using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Plane>( out Plane Plane))
        {
        _player.SetInputOnPlane();
        }
    }
}
