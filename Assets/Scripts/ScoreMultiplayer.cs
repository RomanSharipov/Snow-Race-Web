using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreMultiplayer : MonoBehaviour
{
    [SerializeField] private int _multiplayer;

    public event UnityAction<int> SnowballEntered; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent<Snowball>(out Snowball snowball))
        {
            SnowballEntered?.Invoke(_multiplayer);
           // Debug.Log("entered "+_multiplayer);
        }
    }
}
