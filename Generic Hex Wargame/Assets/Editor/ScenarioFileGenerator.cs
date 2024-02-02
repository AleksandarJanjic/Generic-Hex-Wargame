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
        scenarioData.units = new List<Data.Unit>();
        Debug.Log("Generating JSON");
        for(int i = 0; i < unitsParent.transform.childCount; i++)
        {
            UnitController controller = unitsParent.transform.GetChild(i).GetComponent<UnitController>();
            Data.Unit unit = new Data.Unit();
            unit.id = controller.id;
            unit.unitName = controller.unitName;
            unit.unitType = controller.unitType;
            unit.side = controller.side;
            unit.HP = controller.HP;
            unit.statusEffects = controller.statusEffects;
            unit.unitTemplate = controller.unitTemplate;
            unit.posX = controller.transform.position.x;
            unit.posY = controller.transform.position.y;
            scenarioData.units.Add(unit);
        }
        string json = JsonConvert.SerializeObject(scenarioData, Formatting.Indented, new JsonSerializerSettings
        {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        string filePath = Path.Combine(Application.dataPath, "Scenario File.json");
        File.WriteAllText(filePath, json);

    }
}
