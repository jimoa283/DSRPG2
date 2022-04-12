using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS.FSM
{
    [CreateAssetMenu(fileName = "Move Action", menuName = "FSM/Base/MoveAction")]
    public class MoveAction : FixedUpdateAction
    {

        [Header("静态")]
        public bool isConst = false;
        public AnimationCurve speedCurve;
        public float multipler = 1f;
        [Header("非静态")]
        public float speed;
        [Range(0.01f, 100f)]
        public float lerpSpeed = 10f;
        [Header("Root Motion")]
        public bool useRootMotion = true;

        public override void Excute(State state)
        {
            var character = state.sm.character;
            Rigidbody rigid = character.rigid;
            var input = character.input;

            

        }
    }
}



