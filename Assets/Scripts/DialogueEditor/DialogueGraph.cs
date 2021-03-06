using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Linq;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView _graphView;
    private string _fileName = "New Narrative";


    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolBar();
        GenerateMiniMap();
        GenerateBlackBoard();
    }

    private void GenerateBlackBoard()
    {
        var blackboard = new Blackboard(_graphView);
        blackboard.Add(new BlackboardSection { title = "Exposed Properties" });
        blackboard.addItemRequested = _blackboard =>
          {
              _graphView.AddPropertyToBlackBoard(new ExposedProperty());
          };
        blackboard.editTextRequested = (blackboard1, element,newValue) =>
          {
              var oldPropertyName = ((BlackboardField)element).text;
              if(_graphView.ExposedProperties.Any(x=>x.PropertyName==newValue))
              {
                  EditorUtility.DisplayDialog("Error", "This property name already exists,please chose another one!", "OK");
                  return;
              }

              var propertyIndex = _graphView.ExposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
              _graphView.ExposedProperties[propertyIndex].PropertyName = newValue;
              ((BlackboardField)element).text = newValue;
          };
        blackboard.SetPosition(new Rect(10, 30, 200, 300));
        _graphView.Add(blackboard);
        _graphView.blackboard = blackboard;     
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }

    private void ConstructGraphView()
    {
        _graphView = new DialogueGraphView(this)
        {
            name = "Dialogue Graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolBar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback((value) =>
        {
            _fileName = value.newValue;
        });
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });
        toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Date" });

        /*var nodeCreateButton = new Button(() => { _graphView.CreateNode("Dialogue Node"); });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);*/

        rootVisualElement.Add(toolbar);

    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))           //Unity弹窗警告
        {
            EditorUtility.DisplayDialog("Invalid file name!", "Please enter a valid file name.", "OK");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(_graphView);
        if (save)
            saveUtility.SaveGraph(_fileName);
        else
            saveUtility.LoadGraph(_fileName);
    }

    private void GenerateMiniMap()      //生成小地图
    {
        var miniMap = new MiniMap { anchored = true };
        //this will give 10 px offset from left side
        var cords = _graphView.contentViewContainer.WorldToLocal(new Vector2(this.maxSize.x-10,30));
        miniMap.SetPosition(new Rect(cords.x/5,cords.y, 200, 140));
        _graphView.Add(miniMap);
    }
}
