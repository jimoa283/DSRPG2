using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="WeaponShow",menuName ="FSM/ChangeAnimation/WeaponShow")]
    public class WeaponShowAction : EnterExitAction
    {
        public override void Excute(State state)
        {
            state.sm.character.weaponManager.WeaponShow();
        }
    }
}

