using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="AttackExitAction",menuName ="FSM/Base/Attack/AttackExitAction")]
    public class AttackExitAction : UpdateAction
    {
        public override void Excute(State state)
        {
            AnimatorManager animatorManager = state.sm.character.animatorManager;
            if (animatorManager.GetCurrentStateInfo(0).normalizedTime>=1f)
            {               
                animatorManager.anim.Play("Base.Locomotion");
            }
        }
    }
}

