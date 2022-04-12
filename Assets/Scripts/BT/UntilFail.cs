using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class UntilFail : Node
    {
        protected Node m_nextNode;

        public UntilFail(Node node)
        {
            m_nextNode = node;
        }

        public override NodeState Evaluate()
        {
            if (m_nextNode.nodeState != NodeState.RUNNING)
                m_nextNode.Init();

            switch (m_nextNode.Evaluate())
            {
                case NodeState.FAILURE:
                    m_nodeState = NodeState.FAILURE;
                    break;
                case NodeState.SUCCESS:
                    m_nodeState = NodeState.RUNNING;
                    break;
                case NodeState.RUNNING:
                    m_nodeState = NodeState.RUNNING;
                    break;
                default:
                    break;
            }

            return m_nodeState;
        }

        public override void Init()
        {
            m_nextNode.Init();
            m_nodeState = NodeState.RUNNING;
        }
    }
}


