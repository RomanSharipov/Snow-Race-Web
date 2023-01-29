using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class MoveToCurrentExitPoint : Action
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _currentDistanceTotargetPoint;
    [SerializeField] private Transform _enemyCurrentExitPointsFromCurrentLevel;

    private float _minDistanceForTarget = 1.5f;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyCurrentExitPointsFromCurrentLevel = _enemy.CurrentExitPointFromCurrentLevel;
    }

    public override TaskStatus OnUpdate()
    {
        _currentDistanceTotargetPoint = Vector3.Distance(_enemy.transform.position, _enemyCurrentExitPointsFromCurrentLevel.position);

        if (_currentDistanceTotargetPoint < _minDistanceForTarget)
        {
            return TaskStatus.Success;
        }
        _enemy.EnemyMovement.Move();
        return TaskStatus.Running;

    }

    public override void OnStart()
    {
        _enemyCurrentExitPointsFromCurrentLevel = _enemy.CurrentExitPointFromCurrentLevel;
        _enemy.NavMeshAgent.enabled = true;
        _enemy.NavMeshAgent.SetDestination(_enemyCurrentExitPointsFromCurrentLevel.position);
        
    }

    public override void OnEnd()
    {
        _enemy.SetNextExitPoint();
    }
}
