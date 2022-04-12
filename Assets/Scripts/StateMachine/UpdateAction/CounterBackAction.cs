using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="CounterBackAction",menuName ="FSM/Base/Locomotion/CounterBackAction")]
    public class CounterBackAction : UpdateAction
    {
       
         public override void Excute(State state)
        {
            CharacterController character = state.sm.character;
            WeaponManager weaponManager = character.weaponManager;
            if(weaponManager.leftWeapon!=null&&!weaponManager.isBothHand&&weaponManager.leftWeapon.weaponType==WeaponType.Shield
                &&character.input.ReadKey(InputBufferKey.LT))
            {
                character.animatorManager.anim.SetInteger(AnimatorParam.Trans.ToString(), TransIndex.CounterBack.GetHashCode());
            }
        }
    }
}

