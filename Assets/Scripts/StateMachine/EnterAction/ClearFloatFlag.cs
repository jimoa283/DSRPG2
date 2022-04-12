using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="ClearFloatFlag",menuName ="FSM/Base/ClearFloatFlag")]
    public class ClearFloatFlag : EnterExitAction
    {
        public AnimatorParam animatorParam;
        public float value;

        public override void Excute(State state)
        {
            state.sm.character.animatorManager.anim.SetFloat(animatorParam.ToString(), value);
        }
    }
}

