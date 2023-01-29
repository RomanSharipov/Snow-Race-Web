using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Platform : MonoBehaviour
{
    //[SerializeField] private BoxCollider _wallColdier;
    [SerializeField] private GameObject _endPoint;
    [SerializeField] private bool _riseIsOver = false;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private BoxCollider _frontDoorColdier;
    [SerializeField] private BoxCollider _backDoorColdier;

    private Rigidbody _rigidbody;
    private float _speed = 2;

    public bool RiseIsOver => _riseIsOver;
    public float Speed => _speed;
    
    public event Action RiseWasOver;
    public event Action GotUp;
    public Coroutine _riseUpJob;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _frontDoorColdier.enabled = false;
    }

    public void RiseUp()
    {
        //_rigidbody.MovePosition(_endPoint.transform.position);
        _riseUpJob = StartCoroutine(Rise());
    }

    private IEnumerator Rise()
    {
        GotUp?.Invoke();
        _frontDoorColdier.enabled = true;
        while (transform.position.y < _endPoint.transform.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint.transform.position, _speed * Time.deltaTime);
            
            
            yield return null;
        }
        _riseIsOver = true;
        RiseWasOver?.Invoke();
        _backDoorColdier.enabled = true;
        yield return null;
    }

    private void StopRise()
    {
        if (_riseUpJob != null)
        {
            StopCoroutine(_riseUpJob);
        }
        _riseUpJob = null;
    }

    private void OnEnable()
    {
        RiseWasOver += StopRise;
    }

    private void OnDisable()
    {
        RiseWasOver -= StopRise;
    }

}
