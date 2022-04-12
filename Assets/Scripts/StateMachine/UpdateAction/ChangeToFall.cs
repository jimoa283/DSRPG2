using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DS
{
    [CreateAssetMenu(fileName = "ChangeToFall", menuName = "FSM/Base/ChangeToFall")]
    public class ChangeToFall : UpdateAction
    {
        public override void Excute(State state)
        {
            if(!state.sm.character.isGround)
            {
                state.sm.character.animatorManager.anim.SetInteger(AnimatorParam.Trans.ToString(), TransIndex.FallLoop.GetHashCode());
            }
        }
    }
}

