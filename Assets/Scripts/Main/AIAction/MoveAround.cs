using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace DS
{
    public class MoveAround : Action
    {
        private AIInput input;
        public float time;

        public override void OnAwake()
        {
            input = GetComponent<AIInput>();
            time = 0;
        }

        public override TaskStatus OnUpdate()
        {
            if(Vector3.Distance(transform.position,input.transform.position)>input.attackReadyDistance)
            {
                input.character.lockManager.UnLock();
                return TaskStatus.Failure;
            }

            input.SetWalkCheck(false);

            if (time <= input.attackReadyTime)
            {
                input.SetWalkCheck(true);
                switch (input.attackReadyDir)
                {
                    case 0:
                        input.SetDup(0);
                        input.SetDright(0);
                        break;
                    case 1:
                        input.SetDright(0.4f);
                        input.SetDup(0);
                        break;
                    case 2:
                        input.SetDright(-0.4f);
                        input.SetDup(0);
                        break;
                    case 3:
                        input.SetDup(0.4f);
                        input.SetDright(0);
                        break;
                    default:
                        break;
                }

                time += Time.deltaTime;
                return TaskStatus.Running;
            }
            else
            {

                time = 0;
                return TaskStatus.Success;
            }
        }
    }
}

