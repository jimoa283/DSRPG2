using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="SetBeCounterBack",menuName ="FSM/Base/BeCounterBack/SetBeCounterBack")]
    public class SetBeCounterBack : EnterExitAction
    {
        public bool value;

        public override void Excute(State state)
        {
            state.sm.character.stateManager.isBeCounterBack = value;
        }
    }
}

