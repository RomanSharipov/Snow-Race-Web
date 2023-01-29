using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class MoveToEndPointOnBridge : Action
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _speedMoving;
    [SerializeField] private float _currentDistance;
    [SerializeField] private Transform _targetPoint;

    private Vector3 _direction;

    public Bridge Bridge => _enemy.CurrentBridge;
    public float _previousSpeed ;

    private float _minDistanceForSuccess = 1.0f;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override TaskStatus OnUpdate()
    {
        _currentDistance = Vector3.Distance(_enemy.transform.position, Bridge.EndPoint.position);

        _enemy.EnemyMovement.Move(/*Bridge.EndPoint.position, _speedMoving.Value, _speedRotation.Value*/);

        if (_enemy.Snowball.Scale == 0)
            return TaskStatus.Failure;

        if (_currentDistance < _minDistanceForSuccess)
        {
            return TaskStatus.Success;

        }
        _direction = _targetPoint.position - _enemy.transform.position;
        _enemy.NavMeshAgent.Warp(_enemy.transform.position + _direction.normalized * _speedMoving * Time.deltaTime);

        _enemy.transform.rotation = Quaternion.Euler(0,0,0);
        return TaskStatus.Running;
    }

    public override void OnStart()
    {
        _targetPoint = Bridge.EndPoint;
        _direction = new Vector3();
        _previousSpeed = _enemy.NavMeshAgent.angularSpeed;
        _enemy.NavMeshAgent.angularSpeed = _speedRotation;
    }

    public override void OnEnd()
    {
        _enemy.NavMeshAgent.angularSpeed = _previousSpeed;
    }
}
