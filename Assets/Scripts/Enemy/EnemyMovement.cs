using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement
{
    private Vector3 _direction = new Vector3();
    private Quaternion _targetRotation;
    private Transform _transform;
    private Enemy _enemy;

    public event Action Walked;

    public void Init(Transform transform,Enemy enemy)
    {
        _transform = transform;
        _enemy = enemy;
    }

    public void Move()
    {
        Walked?.Invoke();
    }

    //public void MoveTo(Vector3 _target, float speed, float rotationSpeed)
    //{
    //    Walked?.Invoke();
    //    Vector3 positionDifference = _target - _transform.position;

    //    Vector3 _direction = Vector3.Normalize(positionDifference);
    //    _targetRotation = Quaternion.LookRotation(_direction);
    //    Quaternion lookAtRotationOnly_Y = Quaternion.Euler(_transform.rotation.eulerAngles.x, _targetRotation.eulerAngles.y, _transform.rotation.eulerAngles.z);
    //    _transform.rotation = Quaternion.Lerp(_transform.rotation, lookAtRotationOnly_Y, rotationSpeed * Time.deltaTime);
    //    _transform.position = Vector3.MoveTowards(_transform.position, _target, speed * Time.deltaTime);
    //    _enemy.Snowball.Roll();
    //}
}