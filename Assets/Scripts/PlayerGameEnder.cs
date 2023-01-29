using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameEnder : MonoBehaviour
{
    [SerializeField] private GameStateHandler _gameStateHandler;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (_gameStateHandler.GameIsOver)
            {
                return;
            }
            _gameStateHandler.SetPlayerWin();
        }
    }
}
