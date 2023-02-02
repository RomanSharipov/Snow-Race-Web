using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class Enemy : Stickman
{
    [SerializeField] private EnemyMovingZone[] _enemyMovingZones;
    [SerializeField] private Bridge[] _bridges;
    [SerializeField] private Transform[] _pointsEnterToNextLevel;
    [SerializeField] private Transform[] _exitPointsFromCurrentLevel;
    [SerializeField] private Transform _currentExitPointFromCurrentLevel;
    [SerializeField] private EnemyMovingZone _currentMovingZone;
    [SerializeField] private Transform _currentPointEnterToNextLevel;
    [SerializeField] private float _minSizeForPhysicsSnowball = 1.0f;
    [SerializeField] private float _delayOnTakeDamage = 3.0f;

    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private BehaviorTree _behaviorTree;
    [SerializeField] private Collider _collider;

    [SerializeField] private int _indexCurrentMovingZone;
    [SerializeField] private int _indexCurrentExitPoint;
    [SerializeField] private bool _transitOnNextLevelIsOver;

    [SerializeField] private GameStateHandler _gameStateHandler;

    private int _indexCurrentBridge;
    private int _indexCurrentPointEnterToNextLevel;

    private EnemyMovement _enemyMovement;

    private WaitForSeconds _waitForSeconds;
    private Coroutine _delayStopMovementJob;
    private Coroutine _moveOnElevatorJob;

    public EnemyMovingZone CurrentMovingZone => _currentMovingZone;
    public Bridge CurrentBridge => _bridges[_indexCurrentBridge];
    public Transform CurrentPointEnterToNextLevel => _currentPointEnterToNextLevel;
    public Transform CurrentExitPointFromCurrentLevel => _currentExitPointFromCurrentLevel;
    
    public EnemyMovement EnemyMovement => _enemyMovement;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public bool TransitOnNextLevelIsOver => _transitOnNextLevelIsOver;

    public event Action<float> Walked;
    public event Action WasTakenDamage;
    public event Action Won;
    public event Action TriedWin;


    public void Init(GameStateHandler gameStateHandler)
    {
        _gameStateHandler = gameStateHandler;
    }

    private new void Start()
    {
        base.Start();
        _enemyMovement = new EnemyMovement();
        _enemyMovement.Init(transform,this);
        _enemyMovement.Walked += OnWalked;
        _indexCurrentMovingZone = 0;
        _waitForSeconds = new WaitForSeconds(_delayOnTakeDamage);
        _collider = GetComponent<Collider>();
        _currentExitPointFromCurrentLevel = _exitPointsFromCurrentLevel[_indexCurrentExitPoint];
        _currentMovingZone = _enemyMovingZones[_indexCurrentMovingZone];
        Debug.Log("_currentMovingZone");
        _currentPointEnterToNextLevel = _pointsEnterToNextLevel[_indexCurrentPointEnterToNextLevel];
        _transitOnNextLevelIsOver = false;
    }

    public override void OnEnterOnBridge()
    {
        Snowball.SwitchOffSnowTrail();
    }

    public override void OnExitFromBridge()
    {
        if (Snowball.Scale > _minSizeForPhysicsSnowball)
        {
            Snowball.SwitchOnSnowTrail();
        }
        ResetSkeletonPositions();
    }

    public void SetNextMovingZone()
    {
        _transitOnNextLevelIsOver = false;
        _indexCurrentMovingZone++;
        _currentMovingZone = _enemyMovingZones[_indexCurrentMovingZone];

        if (_indexCurrentPointEnterToNextLevel == _pointsEnterToNextLevel.Length - 1)
            return;

        _indexCurrentPointEnterToNextLevel++;
        _currentPointEnterToNextLevel = _pointsEnterToNextLevel[_indexCurrentPointEnterToNextLevel];

        if (CurrentMovingZone.СontainsBridges)
        {
            _indexCurrentBridge++;
        }
    }

    private void OnDisable()
    {
        _enemyMovement.Walked -= OnWalked;
    }

    private void OnWalked()
    {
        Walked?.Invoke(Snowball.NormalizedScale);
    }

    public override void OnTakeDamage()
    {
        if (_delayStopMovementJob != null)
        {
            StopCoroutine(_delayStopMovementJob);
            _delayStopMovementJob = null;
        }

        Snowball.SetZeroScale();
        WasTakenDamage?.Invoke();
        StopMovement();
        _delayStopMovementJob = StartCoroutine(ActionWithDelay(StartMovement));
    }
    public void StartMovement()
    {
        _collider.enabled = true;
        _behaviorTree.EnableBehavior();
        NavMeshAgent.isStopped = false;
    }

    public void StopMovement()
    {
        _collider.enabled = false;
        _behaviorTree.DisableBehavior(true);
        NavMeshAgent.isStopped = true;
        JumpOffTheBall();
    }

    public override void JumpTuCart(Transform cartPosition,SplineComputer splineComputer,Cart cart)
    {
        NavMeshAgent.enabled = false;
        base.JumpTuCart(cartPosition,splineComputer,cart);
    }
    
    public override void OnEndedFolowSpringBoard()
    {
        base.OnEndedFolowSpringBoard();
        _transitOnNextLevelIsOver = true;
    }
    
    public override void OnStopFolowCart()
    {
        base.OnStopFolowCart();
        _transitOnNextLevelIsOver = true;
    }
    
    public override void OnEnterOnSpringBoard()
    {
        Snowball.SwitchOffSnowTrail();
    }

    private IEnumerator ActionWithDelay(Action action)
    {
        yield return _waitForSeconds;
        action();
    }

    public override void OnPlatformRiseWasOver()
    {
        base.OnPlatformRiseWasOver();
        _navMeshAgent.enabled = true;
        _transitOnNextLevelIsOver = true;
    }

    public override void OnEnterOnElevator(Platform platform)
    {
        _navMeshAgent.enabled = false;
        base.OnEnterOnElevator(platform);
    }

    public override void OnExitOnElevator(Platform platform)
    {
        Snowball.SwitchOnSnowTrail();
        platform.RiseWasOver -= OnPlatformRiseWasOver;
        
    }

    public override void FolowSpringBoard(SplineComputer splineComputer)
    {
        base.FolowSpringBoard(splineComputer);
        NavMeshAgent.enabled = false;
    }

    public void TryWin()
    {
        TriedWin?.Invoke();
        if (_gameStateHandler.PlayerIsWin)
            return;

        Won?.Invoke();
    }

    public override void OnEnterOnPlane()
    {
        Snowball.SnowballScaler.SetScallingUpMode();
    }

    public override void OnExitFromPlane()
    {
        Snowball.SwitchOffSnowTrail();
        Snowball.SnowballScaler.TrySetZeroMode();
    }

    public void SetNextExitPoint()
    {
        _currentExitPointFromCurrentLevel = _exitPointsFromCurrentLevel[_indexCurrentExitPoint];
        if (_indexCurrentExitPoint == _exitPointsFromCurrentLevel.Length - 1)
            return;

        _indexCurrentExitPoint++;
        _currentExitPointFromCurrentLevel = _exitPointsFromCurrentLevel[_indexCurrentExitPoint];
    }
}
