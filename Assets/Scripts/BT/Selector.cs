using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{

    /// <summary>
    /// 选择器；
    /// 选择器序列中某个子节点返回true，则选择器返回true，且不再继续估算其他子节点；
    /// </summary>
    public class Selector : Node
    {
        //子节点们
        private List<Node> m_nextNodes;
        private int nodeIndex;

        public Selector(List<Node> nodes)
        {
            m_nextNodes = nodes;
        }

        /// <summary>
        /// 如果任何结点返回成功，则立刻返回成功，如果所有都失败，则返回失败
        /// </summary>
        /// <returns></returns>
        public override NodeState Evaluate()
        {
            if (m_nodeState != NodeState.RUNNING)
                Init();

            while (nodeIndex < m_nextNodes.Count)
            {
                var state = m_nextNodes[nodeIndex].Evaluate();

                if (state == NodeState.FAILURE)
                {
                   nodeIndex++;
                   m_nodeState = NodeState.RUNNING;
                }
                else if (state == NodeState.RUNNING)
                {
                    m_nodeState = NodeState.RUNNING;
                    break;
                }
                else
                {
                    nodeIndex = 0;
                    m_nodeState = NodeState.SUCCESS;
                    return m_nodeState;
                }
            }

            m_nodeState = NodeState.FAILURE;

            return m_nodeState;
        }

        public override void Init()
        {
            foreach(var node in m_nextNodes)
            {
                node.Init();
            }
            nodeIndex = 0;
            m_nodeState = NodeState.RUNNING;
        }
    }
}


