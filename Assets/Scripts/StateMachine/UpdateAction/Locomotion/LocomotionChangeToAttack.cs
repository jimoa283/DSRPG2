using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="ChangeToFirstAttack",menuName ="FSM/Base/Locomotion/ChangeToFirstAttack")]
    public class LocomotionChangeToAttack : UpdateAction
    {

        public float startTime;
        public override void Excute(State state)
        {
            if(state.sm.character.animatorManager.GetCurrentStateInfo(0).normalizedTime>startTime)
            {
                AttackActionData attackActionData = state.sm.character.attackManager.nextAttackAction;

                if (attackActionData==null)
                {          
                    state.sm.character.attackManager.GetFirstAttack();
                    attackActionData = state.sm.character.attackManager.nextAttackAction;
                    if (attackActionData != null)
                    {
                        Animator anim = state.sm.character.animatorManager.anim;
                        anim.SetBool(AnimatorParam.ChangeAttackType.ToString(), true);
                        anim.SetFloat(AnimatorParam.Speed.ToString(), attackActionData.animTrigger.speedValue);
                        anim.SetInteger(AnimatorParam.Trans.ToString(), attackActionData.animTrigger.transValue);
                    }
                }     
            }
            
        }
    }
}

