using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.Scripting;

[Preserve]
public class IsSmallSnowball : Conditional
{
    [SerializeField] private Enemy _enemy;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override TaskStatus OnUpdate()
	{
        if (_enemy.Snowball.Scale < 5)
        {
            return TaskStatus.Success;
        }

		return TaskStatus.Failure;
	}
}