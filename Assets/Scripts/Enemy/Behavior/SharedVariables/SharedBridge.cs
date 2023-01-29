using System;
using BehaviorDesigner.Runtime;

namespace Agava.Samples.FakeMoba
{
    [Serializable]
    public class SharedBridge : SharedVariable<Bridge>
    {
        public static implicit operator SharedBridge(Bridge value) => new SharedBridge { Value = value };
    }
}