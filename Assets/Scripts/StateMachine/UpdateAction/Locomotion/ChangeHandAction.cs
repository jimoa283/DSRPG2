using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="ChangeHandAction",menuName ="FSM/Base/Locomotion/ChangeHandAction")]
    public class ChangeHandAction : UpdateAction
    {
        public override void Excute(State state)
        {
            if(state.sm.character.animatorManager.anim.GetFloat(AnimatorParam.Speed.ToString())<=1f)
            {
                state.sm.character.weaponManager.BothHandChange();
            }
        }
    }
}

