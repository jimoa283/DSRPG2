using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    /// <summary>
    /// 通用行为节点
    /// </summary>
    public class ActionNode : Node
    {
        /// <summary>
        /// 方法主体
        /// </summary>
        /// <returns></returns>
        public delegate NodeState ActionNodeDelegate();

        private ActionNodeDelegate m_Action;
        private ActionNodeDelegate m_init;

        public ActionNode(ActionNodeDelegate action,ActionNodeDelegate init=null)
        {
            m_Action = action;
            m_init = init;
        }

        public override void Init()
        {
            m_nodeState = NodeState.RUNNING;
            m_init?.Invoke();
        }

        public override NodeState Evaluate()
        {
            if (nodeState != NodeState.RUNNING)
                Init();

            return m_Action.Invoke();
        }
    }
}

