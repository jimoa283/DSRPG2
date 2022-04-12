using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="CleatAttack",menuName ="FSM/Base/Attack/ClearAttack")]
    public class ClearAttack : EnterExitAction
    {
        public override void Excute(State state)
        {
            state.sm.character.attackManager.ClearAttack();
            state.sm.character.animatorManager.anim.SetBool(AnimatorParam.ChangeAttackType.ToString(), false);
        }
    }
}

