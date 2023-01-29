using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class MoveOnPoints : Action
{
    [SerializeField] private Enemy _enemy;

    [SerializeField] private int  _countPoints;

    [SerializeField] private Vector3[] _points;
    [SerializeField] private int _currentPoint;
    [SerializeField] private float _currentDistanceTotargetPoint;

    private Vector3 _targetPoint = new Vector3();
    private float _minDistanceForNextPoint = 1.0f;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override void OnStart()
    {
        _countPoints = Random.Range(2, 5);
        InitPath(_countPoints);
        _enemy.NavMeshAgent.SetDestination(_targetPoint);
    }

    public override TaskStatus OnUpdate()
    {
        if (_enemy.Snowball.Scale > 5)
        {
            
            return TaskStatus.Success;
        }

        if (_currentPoint == _points.Length)
        {
            _currentPoint = 0;
            return TaskStatus.Success;
        }
        _targetPoint = _points[_currentPoint];


        _currentDistanceTotargetPoint = Vector3.Distance(_enemy.transform.position, _targetPoint);

        if (_currentDistanceTotargetPoint < _minDistanceForNextPoint)
        {
            _currentPoint++;
            if (_currentPoint == _points.Length)
            {
                _currentPoint = 0;
                return TaskStatus.Success;
            }
            _targetPoint = _points[_currentPoint];
            _enemy.NavMeshAgent.SetDestination(_targetPoint);
            
            
        }
        _enemy.EnemyMovement.Move(/*Bridge.StartPoint.position, _speedMoving.Value, _speedRotation.Value*/);
        return TaskStatus.Running;
    }

    public void InitPath(int countPoints)
    {
        _points = new Vector3[countPoints];
        _currentPoint = 0;
        for (int i = 0; i < _points.Length; i++)
        {
            float x = Random.Range(_enemy.CurrentMovingZone.transform.position.x - Random.Range(0, _enemy.CurrentMovingZone.BoxCollider.bounds.extents.x), _enemy.CurrentMovingZone.transform.position.x + Random.Range(0, _enemy.CurrentMovingZone.BoxCollider.bounds.extents.x));
            float z = Random.Range(_enemy.CurrentMovingZone.transform.position.z - Random.Range(0, _enemy.CurrentMovingZone.BoxCollider.bounds.extents.z), _enemy.CurrentMovingZone.transform.position.z + Random.Range(0, _enemy.CurrentMovingZone.BoxCollider.bounds.extents.z));
            _points[i] = new Vector3(x, _enemy.CurrentMovingZone.transform.position.y, z);
        }
        _targetPoint = _points[_currentPoint];
    }
}
