using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="ShieldAction",menuName ="FSM/Base/Locomotion/ShieldAction")]
    public class ShieldAction : UpdateAction
    {
        public override void Excute(State state)
        {
            CharacterController character = state.sm.character;
            Animator anim = character.animatorManager.anim;
            WeaponManager weaponManager = character.weaponManager;
            var shield = anim.GetFloat(AnimatorParam.Shield.ToString());
            if (character.input.Shield&&
                    (
                    (weaponManager.leftWeapon != null && weaponManager.leftWeapon.weaponType == WeaponType.Shield)
                    ||(weaponManager.isBothHand)
                    )
                )
            {

                weaponManager.isShield = shield>0.8;

                var target = Mathf.Lerp(shield, 1, 0.2f);

                anim.SetFloat(AnimatorParam.Shield.ToString(), target);

                anim.SetLayerWeight(2, target);
            }
            else
            {
                var target = Mathf.Lerp(shield, 0, 0.2f);
                anim.SetFloat(AnimatorParam.Shield.ToString(), target);
                anim.SetLayerWeight(2, target);
                weaponManager.isShield = false;
            }
            
        }
    }
}

