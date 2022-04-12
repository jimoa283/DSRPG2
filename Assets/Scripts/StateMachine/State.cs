using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DS
{
    [System.Serializable]
    public class State 
    {
        public string stateName = "state";

        [HideInInspector]
        public StateMachine sm;
        public List<EnterExitAction> enterActions = new List<EnterExitAction>();
        public List<UpdateAction> updationActions = new List<UpdateAction>();
        public List<FixedUpdateAction> fixedUpdateActions = new List<FixedUpdateAction>();
        public List<LateUpdateAction> lateUpdateActions = new List<LateUpdateAction>();
        public List<EnterExitAction> exitActios = new List<EnterExitAction>();

        public void Init(StateMachine sm)
        {
            this.sm = sm;
        }

        public void OnEnter()
        {
            foreach(var enter in enterActions)
            {
                enter.Excute(this);
            }
        }

        public void OnUpdate()
        {
            foreach(var update in updationActions)
            {
                update.Excute(this);
            }
        }

        public void OnFixedUpdate()
        {
            foreach(var fixedUpdate in fixedUpdateActions)
            {
                fixedUpdate.Excute(this);
            }
        }

        public void OnLateUpdate()
        {
            foreach(var lateUpdate in lateUpdateActions)
            {
                lateUpdate.Excute(this);
            }
        }

        public void OnExit()
        {
            foreach(var exit in exitActios)
            {
                exit.Excute(this);
            }
        }
    }
}

