using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="SetRootMotion",menuName ="FSM/Base/SetRootMotion")]
    public class SetRootMotion : EnterExitAction
    {
        public bool value;

        public override void Excute(State state)
        {
            state.sm.character.animatorManager.canRootMotion = value;
        }
    }

}
