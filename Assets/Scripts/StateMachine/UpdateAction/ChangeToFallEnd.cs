using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="ChangeToFallEnd",menuName ="FSM/Base/ChangeToFallEnd")]
    public class ChangeToFallEnd : UpdateAction
    {
        public override void Excute(State state)
        {
            if(state.sm.character.isGround)
            {
                state.sm.character.animatorManager.anim.SetInteger(AnimatorParam.Trans.ToString(), TransIndex.FallLand.GetHashCode());
            }
        }
    }
}

