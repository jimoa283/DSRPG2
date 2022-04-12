using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphSaveUtility
{
    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        /*if (!Edges.Any()) return;  //if there are no edges(no connections) then return

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();      //找到有input接口，且input接口有连接的节点集合
        for(int i=0;i<connectedPorts.Length;i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;

            dialogueContainer.nodeLinks.Add(new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            }) ;
        }

        foreach(var dialogueNode in nodes.Where(node=>!node.EntryPoint))       //对于所有不是入口节点的节点
        {
            dialogueContainer.dialogueNodeData.Add(new DialogueNodeData()
            {
                Guid = dialogueNode.GUID,
                DialogueText=dialogueNode.DialogueText,
                Position=dialogueNode.GetPosition().position
            }); 
        }*/

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        if (!SaveNodes(dialogueContainer)) return;
        SaveExposedProperties(dialogueContainer);

        if (AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assert", "Resources");

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    private bool SaveNodes(DialogueContainer dialogueContainer)
    {
        if (!Edges.Any()) return false;  //if there are no edges(no connections) then return

        //var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();      //找到有input接口，且input接口有连接的节点集合
        for (int i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;

            dialogueContainer.nodeLinks.Add(new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }

        foreach (var dialogueNode in nodes.Where(node => !node.EntryPoint))       //对于所有不是入口节点的节点
        {
            dialogueContainer.dialogueNodeData.Add(new DialogueNodeData()
            {
                Guid = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogueText,
                audioClip=dialogueNode.audioClip,
                Position = dialogueNode.GetPosition().position
            });
        }

        return true;
    }

    private void SaveExposedProperties(DialogueContainer dialogueContainer)
    {
        dialogueContainer.exposedProperties.AddRange(_targetGraphView.ExposedProperties);
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);

        if(_containerCache==null)
        {
            EditorUtility.DisplayDialog("File Not Found", "Target dialogue graph file does not exists!", "OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
        CreateExposedProperties();
    }

    private void CreateExposedProperties()
    {
        //Clear existing properties on hot-reload
        _targetGraphView.ClearBlackBoardAndExposedProperties();
        //Add properties form data
        foreach(var exposedProperty in _containerCache.exposedProperties)
        {
            _targetGraphView.AddPropertyToBlackBoard(exposedProperty);
        }
    }

    private void ClearGraph()
    {
        //Set entry guid back from the save. Discard existing guid.
        nodes.Find(x => x.EntryPoint).GUID = _containerCache.nodeLinks[0].BaseNodeGuid;

        foreach(var node in nodes)
        {
            if (node.EntryPoint) continue;

            //Remove edges that connected to this node
            Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge=>_targetGraphView.RemoveElement(edge));

            //Then remove the node
            _targetGraphView.RemoveElement(node);
        }
    }

    private void CreateNodes()
    {
        foreach(var nodeData in _containerCache.dialogueNodeData)
        {
            //We pass position later on,so we can just use Vector.zero
            var _tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText,Vector2.zero,nodeData.audioClip);
            _tempNode.GUID = nodeData.Guid;
            _targetGraphView.AddElement(_tempNode);

            var nodePorts = _containerCache.nodeLinks.Where(x => x.BaseNodeGuid == nodeData.Guid).ToList();
            nodePorts.ForEach(x => _targetGraphView.AddChoicePort(_tempNode, x.PortName));
        }
    }

    private void ConnectNodes()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            var connections = _containerCache.nodeLinks.Where(x => x.BaseNodeGuid == nodes[i].GUID).ToList();

            for(int j=0;j<connections.Count;j++)
            {
                var targetNodeGuid = connections[j].TargetNodeGuid;
                var targetNode = nodes.First(x => x.GUID == targetNodeGuid);
                LinkNodes(nodes[i].outputContainer[j].Q<Port>(),(Port)targetNode.inputContainer[0]);

                targetNode.SetPosition(new Rect(_containerCache.dialogueNodeData.First(x => x.Guid == targetNodeGuid).Position,
                    _targetGraphView.defaultNodeSize)
                    );

            }
        }
    }

    private void LinkNodes(Port output,Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input
        };

        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);

        _targetGraphView.Add(tempEdge);
    }
}

