using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="CounterBackStateSetAction",menuName ="FSM/Base/CounterBack/CounterBackStateSetAction")]
    public class CounterBackStateSetAction : UpdateAction
    {
        public float startCounterBackTime;
        public float endCounterBackTime;        

        public override void Excute(State state)
        {
            CharacterController character = state.sm.character;
            AnimatorStateInfo stateInfo = character.animatorManager.GetCurrentStateInfo(0);
            if (stateInfo.normalizedTime > startCounterBackTime && stateInfo.normalizedTime < endCounterBackTime)
            {
                character.hitManager.canCounterBack = true;
            }
            else
                character.hitManager.canCounterBack = false;
        }
    }
}

