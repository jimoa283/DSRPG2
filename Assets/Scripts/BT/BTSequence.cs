using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BT
{
    public class BTSequence : Node
    {
        private List<Node> m_nextNodes;         //此节点所连接的所有子节点，按从左到右依次执行
        private int nodeIndex;

        public BTSequence(List<Node> nodes)
        {
            m_nextNodes = nodes;
        }

        public override NodeState Evaluate()
        {
            if (m_nodeState != NodeState.RUNNING)
                Init();

            while(nodeIndex<m_nextNodes.Count)
            {
                var _nodeState = m_nextNodes[nodeIndex].Evaluate();

                if(_nodeState==NodeState.FAILURE)
                {
                    m_nodeState = NodeState.FAILURE;
                    nodeIndex = 0;                       //回到第一个
                    break;
                }
                else if(_nodeState==NodeState.RUNNING)
                {
                    m_nodeState = NodeState.RUNNING;
                    break;                             //下次继续执行
                }
                else
                {
                    m_nodeState = NodeState.RUNNING;
                    nodeIndex++;                       //执行下面的
                }
            }

            return m_nodeState;
        }

        public override void Init()
        {            
            foreach (var node in m_nextNodes)
                node.Init();

            nodeIndex = 0;
            m_nodeState = NodeState.RUNNING;
        }
    }

}
