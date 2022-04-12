using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace DS
{
    public class SeekTarget : Action
    {
        private AIInput input;
        //public SharedVector3 seekRange;
        public override void OnAwake()
        {
            input = GetComponent<AIInput>();
        }

        public override TaskStatus OnUpdate()
        {
           if(input.target!=null)
            {
                return TaskStatus.Success;
            }

            var ori = input.character.model.position;
            var center = ori + input.character.model.forward*input.character.isMirror * input.seekRange.z + input.character.model.up *input.seekRange.y;
            var cs = Physics.OverlapBox(center, input.seekRange, input.character.model.rotation, input.targetLayer);
            if(cs!=null&&cs.Length>0)
            {
                var target = cs[0].transform.root.GetComponent<CharacterController>();
                input.target = target;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }

    
}

