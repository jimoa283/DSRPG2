using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="SetCurrentAttack",menuName ="FSM/Base/Attack/SetCurrentAttack")]
    public class SetCurrentAttack : EnterExitAction
    {
        public override void Excute(State state)
        {
            state.sm.character.attackManager.SetCurrentAttackAction();
            state.sm.character.animatorManager.anim.SetBool(AnimatorParam.ChangeAttackType.ToString(), false);
        }
    }
}

