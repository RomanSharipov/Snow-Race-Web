using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.Scripting;

[Preserve]
public class CurentMovingZoneIsFinishZone : Conditional
{
    [SerializeField] private Enemy _enemy;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override TaskStatus OnUpdate()
	{
        if (_enemy.CurrentMovingZone.IsFinishZone)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}