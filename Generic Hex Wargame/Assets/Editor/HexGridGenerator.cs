using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class HexGridGenerator : EditorWindow
{
    public GameObject firstHex;
    public GameObject hexPrefab;
    private IntegerField gridWidth;
    private IntegerField gridHeight;
    public float hexWidth = 1.0f;
    public float hexHeight = 1.0f;

    [MenuItem("Scenario/Hex Grid Generation")]
    public static void ShowWindow()
    {
        HexGridGenerator hexGridGenerationWindow = GetWindow<HexGridGenerator>();
        hexGridGenerationWindow.titleContent = new GUIContent("Hex Grid Generation");
    }

    protected void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        Label label = new Label("Hexes");
        root.Add(label);

        gridWidth = new IntegerField("GridWidth:");
        gridWidth.value = 0; // Default value
        root.Add(gridWidth);

        gridHeight = new IntegerField("GridHeight:");
        gridHeight.value = 0; // Default value
        root.Add(gridHeight);


        ObjectField objectField = new ObjectField("First Hex");
        objectField.objectType = typeof(GameObject);
        objectField.allowSceneObjects = true;
        objectField.RegisterValueChangedCallback(evt => firstHex = (GameObject)evt.newValue);
        root.Add(objectField);

        Button button = new Button();
        button.name = "GenerateHexGrid";
        button.text = "Generate Hex Grid";
        button.clickable.clicked += GenerateGrid;
        root.Add(button);
    }

    public void GenerateGrid()
    {

    }
}
