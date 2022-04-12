using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace DS
{
    public class AIInput : InputBase
    {
        public LayerMask targetLayer;
        public CharacterController target;
        public Vector3 seekRange;
        public NavMeshAgent agent;
        public float stopDistance;
        public float loseTargetDistance;
        public float attackReadyDistance;
        public float attackReadyTime;
        public int attackReadyDir;

        protected override void InputExcute()
        {
            //RBAttack = true;
            //inputBufferDic[InputBufferKey.RB].AddKey();
            //Debug.Log("OK");
        }

        public void SetRun(bool value){Run = value;}
        public void SetDup(float value){Dup = value;}
        public void SetDright(float value){Dright = value;}
        public void SetLock(bool value){Lock = value;}

        public void SetRBAttack(bool value) { RBAttack = value;}

        public void SetWalkCheck(bool value) { WalkCheck = value; }

        public override void Init(CharacterController character)
        {
            base.Init(character);
            agent = GetComponentInChildren<NavMeshAgent>();
        }
    }

}

