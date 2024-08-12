using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitsEditorHelper : EditorWindow
{
    public GameObject unitsParent;

    [MenuItem("Scenario/Unit Editor Helper")]
    public static void ShowWindow()
    {
        UnitsEditorHelper unitsEditorHelper = GetWindow<UnitsEditorHelper>();
        unitsEditorHelper.titleContent = new GUIContent("Units Editor Helper");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        Label label = new Label("Units Helper");
        root.Add(label);

        ObjectField objectField = new ObjectField("Units Parent");
        objectField.objectType = typeof(GameObject);
        objectField.allowSceneObjects = true;
        objectField.RegisterValueChangedCallback(evt => unitsParent = (GameObject)evt.newValue);
        root.Add(objectField);

        Button button = new Button();
        button.name = "PlaceUnits";
        button.text = "Place Units";
        button.clickable.clicked += PlaceUnits;
        root.Add(button);
    }

    public void PlaceUnits()
    {
        // go through all units and raycast to get hex under
        HexController controller = null;
        UnitController currentUnit = null;

        for(int i = 0; i < unitsParent.transform.childCount; i++) 
        {
            currentUnit = unitsParent.transform.GetChild(i).GetComponent<UnitController>();

            LayerMask targetLayer = GetLayerMaskByName("HexGrid");
            RaycastHit2D hit = Physics2D.Raycast(unitsParent.transform.GetChild(i).transform.position, Vector2.down, Mathf.Infinity, targetLayer);

            // Check if the raycast hit something on the target layer
            if(hit.collider != null)
            {
                Debug.Log($"Hit object: {hit.collider.gameObject.transform.parent.name}");
                controller = hit.collider.gameObject.GetComponentInParent<HexController>();
                if(controller != null)
                {
                    currentUnit.gameObject.GetComponentInChildren<UnitPositionHelper>().UnitPlacedOnHex(currentUnit, controller.GetHexId());
                }
            }

        }
    }

    private LayerMask GetLayerMaskByName(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if(layer == -1)
        {
            Debug.LogError($"Layer '{layerName}' does not exist.");
            return LayerMask.GetMask(); // Returns an empty layer mask
        }

        return 1 << layer;
    }
}
