using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="ClearBoolFlag",menuName ="FSM/Base/ClearBoolFlag")]
    public class ClearBoolFlag : EnterExitAction
    {
        public AnimatorParam animatorParam;
        public bool value;

        public override void Excute(State state)
        {
            state.sm.character.animatorManager.anim.SetBool(animatorParam.ToString(), value);
        }
    }
}


