using Agava.Samples.FakeMoba;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class SetNewLevel : Action
{
    [SerializeField] private Enemy _enemy;

    public override void OnAwake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public override void OnStart()
    {
        _enemy.SetNextMovingZone();
        
    }
}
