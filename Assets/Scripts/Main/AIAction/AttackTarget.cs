using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace DS
{
    public class AttackTarget : Action
    {
        private AIInput input;

        public override void OnAwake()
        {
            input = GetComponent<AIInput>();
        }

        public override TaskStatus OnUpdate()
        {
            input.inputBufferDic[InputBufferKey.RB].AddKey();
            return TaskStatus.Success;
        }
    }
}

