using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OnAplicationFocus : MonoBehaviour
{
    [SerializeField] private GameStoper _gameStoper;
    [SerializeField] private MaxSizeSnowballAD _starter;
    [SerializeField] private ElevatorInter[] _elevatorInters;
    [SerializeField] private SkiColisionHandler[] _skiColisionHandlers;
    
    private AudioHandler _audioHandler;

    private void Awake()
    {
        _audioHandler = FindObjectOfType<AudioHandler>();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (IsOpenSkiRewardAD())
            return;

        if (IsOpenElevatorInter())
            return;

        if (_starter.IsStarted)
        {
            if (focus)
            {
                _gameStoper.Resume();
            }

            else
            {
                _gameStoper.Stop();
            }
                
        }
        TryTurnAudiolistener(focus);

    }

    private void TryTurnAudiolistener(bool focus)
    {
        if (focus&&!Constants.IsWatchAdd)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    public bool IsOpenElevatorInter()
    {
        foreach (var elevatorInter in _elevatorInters)
        {
            if (elevatorInter.ElevatorInterIsOpen)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsOpenSkiRewardAD()
    {
        foreach (var skiColisionHandler in _skiColisionHandlers)
        {
            if (skiColisionHandler.IsOpenSkiRewardAD)
            {
                return true;
            }
        }
        return false;
    }
}