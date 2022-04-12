using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace DS
{
    public class LockTarget : Action
    {
        private AIInput input;

        public override void OnAwake()
        {
            input = GetComponent<AIInput>();
        }

        public override TaskStatus OnUpdate()
        {
            input.SetLock(false);
            if(!input.character.lockManager.isLock)
            {
                //Vector3 localDir = input.character.model.InverseTransformDirection(input.target.transform.position - transform.position);
                input.SetLock(true);
                return TaskStatus.Running;
            }
            else
            {
                return TaskStatus.Success;
            }
        }
    }
}

