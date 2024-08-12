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
    public GameObject hexGridParent;
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


        ObjectField firstHex = new ObjectField("First Hex");
        firstHex.objectType = typeof(GameObject);
        firstHex.allowSceneObjects = true;
        firstHex.RegisterValueChangedCallback(evt => this.firstHex = (GameObject)evt.newValue);
        root.Add(firstHex);

        ObjectField hexPrefab = new ObjectField("Hex Prefab");
        hexPrefab.objectType = typeof(GameObject);
        hexPrefab.RegisterValueChangedCallback(evt => this.hexPrefab = (GameObject)evt.newValue);
        root.Add(hexPrefab);

        ObjectField hexParent = new ObjectField("Hex Parent");
        hexParent.objectType = typeof(GameObject);
        hexParent.RegisterValueChangedCallback(evt => this.hexGridParent = (GameObject)evt.newValue);
        root.Add(hexParent);

        Button button = new Button();
        button.name = "GenerateHexGrid";
        button.text = "Generate Hex Grid";
        button.clickable.clicked += GenerateGrid;
        root.Add(button);
    }

    public void GenerateGrid()
    {
        if(hexPrefab == null)
        {
            Debug.LogError("Hex Prefab is not assigned!");
            return;
        }

        if(firstHex == null)
        {
            Debug.LogError("First Hex GameObject is not assigned!");
            return;
        }

        float xOffset = hexPrefab.GetComponentInChildren<HexColliderHelper>().GetColliderWidth() * 0.75f;
        float yOffset = hexPrefab.GetComponentInChildren<HexColliderHelper>().GetColliderHeight();

        Vector3 startPosition = firstHex.transform.position;

        for(int y = 0; y < gridHeight.value; y++)
        {
            for(int x = 0; x < gridWidth.value; x++)
            {
                float xPos = x * xOffset;
                float yPos = y * yOffset;

                // Offset for staggered rows
                if(x % 2 == 1)
                {
                    yPos += hexPrefab.GetComponentInChildren<HexColliderHelper>().GetColliderHeight() / 2;
                }

                Vector3 hexPosition = new Vector3(startPosition.x + xPos, startPosition.y + yPos, startPosition.z);
                GameObject hexInstance = (GameObject)PrefabUtility.InstantiatePrefab(hexPrefab);
                hexInstance.transform.parent = hexGridParent.transform;
                hexInstance.transform.position = hexPosition;
                hexInstance.name = $"Hex_{x}_{y}";
            }
        }
    }
}
