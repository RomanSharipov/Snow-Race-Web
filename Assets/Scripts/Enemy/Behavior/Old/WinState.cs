using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.Scripting;

[Preserve]
public class WinState : Action
{
    [SerializeField] private Enemy _enemy;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override TaskStatus OnUpdate()
	{
        _enemy.TryWin();
        return TaskStatus.Success;
	}
}