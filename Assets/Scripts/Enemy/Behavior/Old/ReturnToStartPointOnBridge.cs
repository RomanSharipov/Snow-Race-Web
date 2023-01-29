using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ReturnToStartPointOnBridge : Action
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private SharedFloat _speedRotation;
    [SerializeField] private SharedFloat _speedMoving;
    [SerializeField] private float _currentDistance;

    public Bridge Bridge => _enemy.CurrentBridge;

    private float _minDistanceForSuccess = 1.0f;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override TaskStatus OnUpdate()
    {
        _currentDistance = Vector3.Distance(_enemy.transform.position, Bridge.StartPoint.position);

        if (_currentDistance < _minDistanceForSuccess)
        {
            return TaskStatus.Success;
        }

        _enemy.EnemyMovement.Move(/*Bridge.StartPoint.position, _speedMoving.Value, _speedRotation.Value*/);
        return TaskStatus.Running;
    }

    public override void OnStart()
    {
        _enemy.NavMeshAgent.SetDestination(Bridge.StartPoint.position);
    }

}
