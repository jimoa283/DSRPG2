using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNodeData 
{
    public string Guid;         //节点的ID
    public string DialogueText;   //节点的文本
    public AudioClip audioClip;
    public Vector2 Position;      //节点的位置
}
