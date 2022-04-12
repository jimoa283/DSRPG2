using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    /// <summary>
    /// 作为框架所有节点的基类
    /// </summary>
    public abstract class Node 
    {
        protected NodeState m_nodeState = NodeState.FAILURE;

        public NodeState nodeState
        {
            get { return m_nodeState; }
        }

        public Node() { }

        public abstract NodeState Evaluate();

        public virtual void Init() { m_nodeState = NodeState.RUNNING; }
    }

    public enum NodeState
    {
        FAILURE,
        SUCCESS,
        RUNNING
    }
}

