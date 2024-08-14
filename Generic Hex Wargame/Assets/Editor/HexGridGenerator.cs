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

        Button getNeighboursButton = new Button();
        getNeighboursButton.name = "GenerateNeighbours";
        getNeighboursButton.text = "Get Hex Neighbours";
        getNeighboursButton.clickable.clicked += ConnectHexes;
        root.Add(getNeighboursButton);
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
                int res = 0;
                int.TryParse(x + "" + y, out res);
                hexInstance.GetComponent<HexController>().SetHexId(res);
            }
        }
    }

    public void ConnectHexes()
    {
        LayerMask targetLayer = GetLayerMaskByName("HexGrid");
        float rayDistance = 10f;

        //for (int i = 0; i < hexGridParent.transform.childCount; i++)
        for (int i = 0; i < 1; i++)
        {
            HexController currentHex = hexGridParent.transform.GetChild(i).GetComponent<HexController>();

            // cast six rays to get hex controllers adjacent

            float[] angles = { 0f, 60f, 120f, 180f, 240f, 300f };

            foreach (float angle in angles)
            {
                // Calculate direction based on angle
                Vector3 direction = Quaternion.Euler(0, 0, angle) * currentHex.gameObject.transform.up;

                // Perform the raycast
                RaycastHit2D[] hits = Physics2D.RaycastAll(currentHex.gameObject.transform.position, direction, rayDistance, targetLayer);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.gameObject != currentHex.gameObject.GetComponent<HexController>().colliderGameObject.gameObject)
                    {
                        Debug.Log($"Ray hit: {hit.collider.gameObject.GetComponentInParent<HexController>().GetHexId()} at angle {angle} degrees");
                        break; // Exit after finding the first valid hit
                    }
                }

                // Draw the ray in the Scene view for visualization
                Debug.DrawRay(currentHex.gameObject.transform.position, direction * rayDistance, Color.red);
            }
        }
    }

    private LayerMask GetLayerMaskByName(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogError($"Layer '{layerName}' does not exist.");
            return LayerMask.GetMask(); // Returns an empty layer mask
        }

        return 1 << layer;
    }
}
