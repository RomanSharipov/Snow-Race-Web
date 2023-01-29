using System;
using BehaviorDesigner.Runtime;

namespace Agava.Samples.FakeMoba
{
    [Serializable]
    public class SharedEnemy : SharedVariable<Enemy>
    {
        public static implicit operator SharedEnemy(Enemy value) => new SharedEnemy { Value = value };
    }
}