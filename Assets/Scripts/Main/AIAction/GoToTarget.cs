using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace DS
{
    public class GoToTarget : Action
    {
        private AIInput input;

        public override void OnAwake()
        {
            input = GetComponent<AIInput>();
        }

        public override TaskStatus OnUpdate()
        {
            NavMeshAgent agent = input.agent;

            agent.destination =input.target.transform.position;
            agent.speed = 0;
            var path = agent.path;
            var nextPos = path.corners;

            float distance = Vector3.Distance(transform.position, input.target.transform.position);

            if (distance<=input.stopDistance)
            {
                Stop();
                //input.target = null;
                return TaskStatus.Success;
            }
            if(distance>=input.stopDistance+1||!input.Run)
            {
                if(distance>input.loseTargetDistance)
                {
                    Stop();
                    input.target = null;
                    return TaskStatus.Failure;
                }
                if (nextPos.Length < 2)
                {
                    Stop();
                    input.target = null;
                    return TaskStatus.Failure;
                }
                //input.SetRun(true);
                Vector3 localDir = input.character.model.InverseTransformDirection(nextPos[1] - transform.position);
                localDir.Normalize();
                input.SetDup(Mathf.Lerp(input.Dup, localDir.z, 0.2f));
                input.SetDright(Mathf.Lerp(input.Dright, localDir.x, 0.2f));
                return TaskStatus.Running;
            }
            else
            {
                Stop();
                return TaskStatus.Success;
            }

            
        }

        private void Stop()
        {
            //input.SetRun(false);

            input.SetDup(0);
            input.SetDright(0);
        }

    }
}

