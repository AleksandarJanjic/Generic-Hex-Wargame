using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ScenarioFileGenerator : EditorWindow
{
    // fields for GO's needed for this
    // Hex parent obj, units parent obj, etc...
    public GameObject unitsParent;

    [MenuItem("Scenario/Scenario File Generator")]
    public static void ShowWindow()
    {
        ScenarioFileGenerator scenarioFileGeneratorWindow = GetWindow<ScenarioFileGenerator>();
        scenarioFileGeneratorWindow.titleContent = new GUIContent("Scenario File Generation");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        Label label = new Label("Units");
        root.Add(label);

        ObjectField objectField = new ObjectField("Units Parent");
        objectField.objectType = typeof(GameObject);
        objectField.allowSceneObjects = true;
        objectField.RegisterValueChangedCallback(evt => unitsParent = (GameObject)evt.newValue);
        root.Add(objectField);

        Button button = new Button();
        button.name = "GenerateUnitsJSON";
        button.text = "Generate Units JSON";
        button.clickable.clicked += GenerateJSON;
        root.Add(button);
    }

    public void GenerateJSON()
    {
        ScenarioData scenarioData = new ScenarioData();
        scenarioData.units = new List<UnitController>();
        Debug.Log("Generating JSON");
        for(int i = 0; i < unitsParent.transform.childCount; i++)
        {
            scenarioData.units.Add(unitsParent.transform.GetChild(i).GetComponent<UnitController>());
        }
        string json = JsonConvert.SerializeObject(scenarioData, Formatting.Indented, new JsonSerializerSettings
        {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        string filePath = Path.Combine(Application.dataPath, "Scenario File.json");
        File.WriteAllText(filePath, json);

    }
}
