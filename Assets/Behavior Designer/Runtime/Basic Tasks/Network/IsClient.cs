#if !UNITY_5_0

using UnityEngine.Networking;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNetwork
{
    public class IsClient : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            return  TaskStatus.Success;
        }
    }
}
#endif