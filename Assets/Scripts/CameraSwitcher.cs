using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cameraForPlayer;
    [SerializeField] private CinemachineVirtualCamera _cameraForSnowBall;

    public void Switch()
    {
        _cameraForSnowBall.Priority = _cameraForPlayer.Priority + 1;
    }
}
