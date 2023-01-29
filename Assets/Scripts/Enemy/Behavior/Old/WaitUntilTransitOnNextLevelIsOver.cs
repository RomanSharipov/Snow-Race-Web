using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.Scripting;

[Preserve]
public class WaitUntilTransitOnNextLevelIsOver : Action
{
   
    [SerializeField] private Enemy _enemy;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override TaskStatus OnUpdate()
	{
        if (_enemy.TransitOnNextLevelIsOver)
        {
            return TaskStatus.Success;
        }

        else
        {
            return TaskStatus.Running;
        }
	}
}