using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [CreateAssetMenu(fileName ="FallAction",menuName ="FSM/Base/Fall/FallAction")]
    public class FallAction : FixedUpdateAction
    {
        public float fallSpeed;

        public override void Excute(State state)
        {
            var charactor = state.sm.character;
            var vy = charactor.rigid.velocity.y;
            var v = charactor.velocityBeforeTransition;
            v.y = vy + fallSpeed * Time.fixedDeltaTime;
            charactor.rigid.velocity = v;
        }
    }
}

