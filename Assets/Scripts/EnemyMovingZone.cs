using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingZone : MonoBehaviour
{
    [SerializeField] private Bridge[] _bridges;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private bool _containsBridges ;
    [SerializeField] private bool _isFinishZone;

    public BoxCollider BoxCollider => _boxCollider;
    public bool СontainsBridges=> _containsBridges;
    public bool IsFinishZone => _isFinishZone;

    private void Start()
    {
        _containsBridges = _bridges.Length != 0 ? true : false;
    }


    
} 
