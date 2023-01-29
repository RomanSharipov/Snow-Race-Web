using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class IsSnowballScaleNotSizeZero : Conditional
{
    [SerializeField] private Enemy _enemy;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override TaskStatus OnUpdate()
    {
        return _enemy.Snowball.Scale == 0 ? TaskStatus.Failure : TaskStatus.Success;
    }
}
