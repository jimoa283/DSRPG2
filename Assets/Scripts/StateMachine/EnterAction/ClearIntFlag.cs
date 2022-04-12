using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="ClearIntFlag",menuName ="FSM/Base/ClearIntFlag")]
    public class ClearIntFlag : EnterExitAction
    {
        public AnimatorParam animatorParam;
        public int value;

        public override void Excute(State state)
        {
            state.sm.character.animatorManager.anim.SetInteger(animatorParam.ToString(), value);
        }
    }
}

