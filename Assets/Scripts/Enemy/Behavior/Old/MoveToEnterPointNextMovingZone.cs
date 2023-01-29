using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class MoveToEnterPointNextMovingZone : Action
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _currentDistance;
    [SerializeField] private Transform _currentPoint;

    private float _minDistanceForSuccess = 1.0f;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override TaskStatus OnUpdate()
    {
        _currentDistance = Vector3.Distance(_enemy.transform.position, _currentPoint.position);

        if (_currentDistance < _minDistanceForSuccess)
        {
            return TaskStatus.Success;
        }

        _enemy.EnemyMovement.Move(/*Bridge.NextPlanePoint.position, _speedMoving.Value, _speedRotation.Value*/);
        return TaskStatus.Running;
    }

    public override void OnStart()
    {
        _currentPoint = _enemy.CurrentPointEnterToNextLevel;
        _enemy.NavMeshAgent.enabled = true;
        _enemy.NavMeshAgent.SetDestination(_currentPoint.position);
    }

}
