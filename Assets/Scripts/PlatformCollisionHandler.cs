using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

[RequireComponent(typeof(Platform),typeof(BoxCollider))]
public class PlatformCollisionHandler : MonoBehaviour
{
    [SerializeField] private Transform _centerPlatform;
    private Platform _platform;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _platform = GetComponent<Platform>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Stickman>(out Stickman stickman))
        {
            stickman.transform.position = new Vector3(_centerPlatform.position.x, stickman.transform.position.y, _centerPlatform.position.z);
            stickman.OnEnterOnElevator(_platform);
            _platform.RiseUp();
            _boxCollider.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Stickman>(out Stickman stickman))
        {
            stickman.OnExitOnElevator(_platform);
        }
    }
}
