using System.Collections;
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class NextLevelHandler : MonoBehaviour
{
    [Header("The bridge the player came from")]
    [SerializeField] private Bridge _previousBridge;
    [SerializeField] private Transform _nextLevelTeleportationPoint;

    private BoxCollider _backWall;

    private void Start()
    {
        _backWall = GetComponent<BoxCollider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.transform.position = _nextLevelTeleportationPoint.position;
            player.JumpOffTheBall();
            player.SetInputOnPlane();
            _previousBridge?.SwitchOffEnterPlayerHandler(player);
            _backWall.isTrigger = false;
            player.Reaction.ShowReaction();
        }
    }

}
