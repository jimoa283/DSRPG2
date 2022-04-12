using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    public class StateMachine : MonoBehaviour
    {
        public CharacterController character;
        public List<State> states;

        public State currentState;
        public int layIndex;
        public float lastNormalizedTime = 0;

         public void Init(CharacterController character)
        {
            this.character = character;

            foreach(var state in states)
            {
                state.Init(this);
            }

            currentState = states[0];
            currentState.OnEnter();
        }

        private void Update()
        {
            AnimatorStateInfo animatorStateInfo = character.animatorManager.GetCurrentStateInfo(layIndex);
            if(!animatorStateInfo.IsName(currentState.stateName))
            {
                foreach(var state in states)
                {
                    if(animatorStateInfo.IsName(state.stateName))
                    {
                        currentState.OnExit();
                        state.OnEnter();
                        lastNormalizedTime = 0;
                        currentState = state;
                        break;
                    }
                }
            }
            else
            {
                if(lastNormalizedTime>animatorStateInfo.normalizedTime)
                {
                    currentState.OnExit();
                    currentState.OnEnter();
                }
            }

            currentState.OnUpdate();
            lastNormalizedTime = animatorStateInfo.normalizedTime;
        }

        private void FixedUpdate()
        {
            AnimatorStateInfo animatorStateInfo = character.animatorManager.GetCurrentStateInfo(layIndex);
            if (!animatorStateInfo.IsName(currentState.stateName))
            {
                foreach (var state in states)
                {
                    if (animatorStateInfo.IsName(state.stateName))
                    {
                        currentState.OnExit();
                        state.OnEnter();
                        lastNormalizedTime = 0;
                        currentState = state;
                        break;
                    }
                }
            }
            else
            {
                if (lastNormalizedTime > animatorStateInfo.normalizedTime)
                {
                    currentState.OnExit();
                    currentState.OnEnter();
                }
            }

            currentState.OnFixedUpdate();
            lastNormalizedTime = animatorStateInfo.normalizedTime;
            
        }

        private void LateUpdate()
        {
            AnimatorStateInfo animatorStateInfo = character.animatorManager.GetCurrentStateInfo(layIndex);
            if (!animatorStateInfo.IsName(currentState.stateName))
            {
                foreach (var state in states)
                {
                    if (animatorStateInfo.IsName(state.stateName))
                    {
                        currentState.OnExit();
                        state.OnEnter();
                        lastNormalizedTime = 0;
                        currentState = state;
                        break;
                    }
                }
            }
            else
            {
                if (lastNormalizedTime > animatorStateInfo.normalizedTime)
                {
                    currentState.OnExit();
                    currentState.OnEnter();
                }
            }

            currentState.OnLateUpdate();
            lastNormalizedTime = animatorStateInfo.normalizedTime;
            
        }

       
    }
}

