using System;
using BehaviorDesigner.Runtime;

namespace Agava.Samples.FakeMoba
{
    [Serializable]
    public class SharedEnemyMovingZone : SharedVariable<EnemyMovingZone>
    {
        public static implicit operator SharedEnemyMovingZone(EnemyMovingZone value) => new SharedEnemyMovingZone { Value = value };
    }
}