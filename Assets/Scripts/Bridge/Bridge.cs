using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [Header("The point to which the player is dragged")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _nextPlanePoint;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Collider _plane;
    [SerializeField] private TypeBridge _typeBridge;

    private IBridgeAction _bridgeAction;
    private bool _stickmanOnBridge;

    public Transform StartPoint => _startPoint;
    public Transform EndPoint => _endPoint;
    public Transform NextPlanePoint => _nextPlanePoint;

    public event Action BridgeWasDisabled;

    private void Start()
    {
        switch (_typeBridge)
        {
            case TypeBridge.WaterBridge:
                _bridgeAction = new WatterBridge();
                break;
            case TypeBridge.LavaBridge:
                _bridgeAction = new LawaBridge();
                break;
            case TypeBridge.SimpleBridge:
                _bridgeAction = new SimpleBridge();
                break;
            default:
                break;
        }
        _stickmanOnBridge = false;
    }

    public void SwitchOffEnterPlayerHandler(Player player)
    {
        _boxCollider.isTrigger = false;
        _boxCollider.enabled = false;
        _bridgeAction.OnExitStickman(player);
        BridgeWasDisabled?.Invoke();
        _plane.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            if (_stickmanOnBridge)
                return;

            stickman.transform.localPosition = StartPoint.position;
            stickman.OnEnterOnBridge();
            _bridgeAction.OnEnterStickman(stickman);
            _stickmanOnBridge = true;
        }

        if (other.TryGetComponent(out Snowball snowball))
        {
            snowball.SwitchOffSnowTrail();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            _bridgeAction.OnExitStickman(stickman);
            stickman.OnExitFromBridge();
            _stickmanOnBridge = false;
        }
    }

}


public enum TypeBridge {WaterBridge,LavaBridge,SimpleBridge }
