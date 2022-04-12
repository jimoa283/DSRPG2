using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName = "ClearShield", menuName = "FSM/Base/Locomotion/ClearShield")]
    public class ClearShield : EnterExitAction
    {
        
        public override void Excute(State state)
        {
            var anim = state.sm.character.animatorManager.anim;

            anim.SetFloat(AnimatorParam.Shield.ToString(), 0);
            anim.SetLayerWeight(2, 0);
            state.sm.character.weaponManager.isShield = false;
        }
    }
}


