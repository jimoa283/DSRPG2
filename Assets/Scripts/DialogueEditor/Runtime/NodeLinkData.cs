using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeLinkData 
{
    public string BaseNodeGuid;       //他的output节点
    public string PortName;          //接口名称
    public string TargetNodeGuid;    //与该接口连接的节点的ID，指前一个节点
}
