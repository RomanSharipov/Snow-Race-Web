using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class IsLevelWithBridges : Conditional
{
    [SerializeField] private Enemy _enemy;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override TaskStatus OnUpdate()
    {
        if (_enemy.CurrentMovingZone.ÑontainsBridges)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

}
