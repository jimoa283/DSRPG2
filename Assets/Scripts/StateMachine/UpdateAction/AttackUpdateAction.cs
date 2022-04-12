using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DS
{
    [CreateAssetMenu(fileName ="AttackUpdateAction",menuName ="FSM/Base/Attack/AttackUpdateAction")]
    public class AttackUpdateAction : UpdateAction
    {
        public override void Excute(State state)
        {
            CharacterController character = state.sm.character;
            AnimatorStateInfo stateInfo =character.animatorManager.GetCurrentStateInfo(0);
            AttackManager attackManager = character.attackManager;
            AttackActionData currentAttack =attackManager.currentAttackAction;
            WeaponData weaponData =character.weaponManager.currentAttackWeapon;

           /* if (currentAttack == null)
                Debug.Log("OJK");*/

            if(character.stateManager.isBeCounterBack)
            {
                if (stateInfo.normalizedTime > currentAttack.openAttackColliderTime && stateInfo.normalizedTime < currentAttack.closeAttackColliderTime)
                {
                    weaponData.CloseCollider();
                }
                character.animatorManager.anim.SetBool(AnimatorParam.Ban.ToString(), true);
                character.animatorManager.anim.SetInteger(AnimatorParam.Trans.ToString(), TransIndex.BeCounterBack.GetHashCode());
                return;
            }

            if(stateInfo.normalizedTime>currentAttack.openAttackColliderTime&&stateInfo.normalizedTime<currentAttack.closeAttackColliderTime)
            {
                weaponData.OpenCollider();
            }

            if(stateInfo.normalizedTime>currentAttack.closeAttackColliderTime)
            {
                weaponData.CloseCollider();
            }

            if(stateInfo.normalizedTime>currentAttack.openRotateTime&&stateInfo.normalizedTime<currentAttack.closeRotateTime)
            {
                Vector3 v = character.input.Dvec;
                float angle =Vector3.Angle(character.model.forward,character.attackManager.startAttackDir);
                if(v.magnitude>0.1f&&angle<currentAttack.maxRotateAngle)
                {
                    v.Normalize();
                    Quaternion target = Quaternion.LookRotation(v);
                    character.model.rotation= Quaternion.Slerp(character.model.rotation, target,10 * Time.deltaTime);
                }
            }

            if(stateInfo.normalizedTime>currentAttack.startAttackBackTime)
            {
                AttackActionData attackActionData = attackManager.nextAttackAction;

                if (attackActionData==null)
                {
                    attackManager.GetNextAttack();
                    attackActionData = attackManager.nextAttackAction;
                    if(attackActionData!=null)
                    {
                        Animator anim = state.sm.character.animatorManager.anim;
                        anim.SetFloat(AnimatorParam.Speed.ToString(), attackActionData.animTrigger.speedValue);
                        anim.SetInteger(AnimatorParam.Trans.ToString(), attackActionData.animTrigger.transValue);
                    }
                }
            }
        }
    }
}

