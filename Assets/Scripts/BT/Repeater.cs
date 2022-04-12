using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    /// <summary>
    /// 重复执行一定次数，直到子节点成功或者失败达到一定次数，0表示一直重复，返回running
    /// </summary>
    public class Repeater : Node
    {
        protected Node m_nextNode;           //此节点连接的下一个节点
        public uint repeatTimes = 0;
        protected uint m_times = 0;


        public Repeater(Node node,uint times)
        {
            m_nextNode = node;
            repeatTimes = times;
        }

        public override NodeState Evaluate()
        {
            if (m_nodeState != NodeState.RUNNING)
                Init();

            if(repeatTimes==0||m_times<repeatTimes)
            {
                //初始化
                var state = m_nextNode.Evaluate();

                //执行完毕
                if (state != NodeState.RUNNING)
                    m_times++;

                m_nodeState = NodeState.RUNNING;

                return m_nodeState;
            }

            m_nodeState = NodeState.SUCCESS;

            return m_nodeState;
        }

        public override void Init()
        {
            m_times = 0;
            m_nextNode.Init();
            m_nodeState = NodeState.RUNNING;
        }
    }
}


