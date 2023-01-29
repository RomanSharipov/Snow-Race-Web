using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineComputer))]
public class FinishColisionHandler : MonoBehaviour
{
    [SerializeField] private CameraSwitcher _cameraSwitcher;
    [SerializeField] private GameStateHandler _gameStateHandler;

    private SplineComputer _splineComputer;

    public event Action PlayerWon;

    public void Init(GameStateHandler gameStateHandler)
    {
        _gameStateHandler = gameStateHandler;
    }

    private void Awake()
    {
        _splineComputer = GetComponent<SplineComputer>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (_gameStateHandler.GameIsOver)
                return;

            Win(player);
        }
    }

    private void Win(Player player)
    {
        player.Win();
        player.TurnOffAllInputs();
        player.Snowball.InitSplineComputer(_splineComputer);
        _cameraSwitcher.Switch();
        PlayerWon?.Invoke();
    }
}
