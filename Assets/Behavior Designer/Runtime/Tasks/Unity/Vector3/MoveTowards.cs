using Agava.Samples.FakeMoba;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
    [TaskCategory("Unity/Vector3")]
    [TaskDescription("Move from the current position to the target position.")]
    public class MoveTowards : Action
    {
        [Tooltip("The current position")]
        public SharedEnemy _enemy;
        [Tooltip("The target position")]
        public SharedTransform targetPoint;
        [Tooltip("The movement speed")]
        public SharedFloat speed;
        [Tooltip("The move resut")]
        [RequiredField]
        public SharedVector3 storeResult;

        public override TaskStatus OnUpdate()
        {
            storeResult.Value = Vector3.MoveTowards(_enemy.Value.transform.position, targetPoint.Value.position, speed.Value * Time.deltaTime);
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            _enemy.Value.transform.position = Vector3.zero;
            targetPoint.Value.position = Vector3.zero;
            storeResult = Vector3.zero;
            speed = 0;
        }
    }
}