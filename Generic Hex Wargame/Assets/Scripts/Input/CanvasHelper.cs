using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasHelper : MonoBehaviour
{
    private static CanvasHelper instance;
    GraphicRaycaster graphicsRaycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    public static CanvasHelper Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure there is only one instance, destroy duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        // Set the instance to this object
        instance = this;

        // Optional: Make the GameObject persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        graphicsRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    public bool IsUIElementAtPosition(Vector3 mousePosition)
    {
        //Set up the new Pointer Event
        pointerEventData = new PointerEventData(eventSystem);
        //Set the Pointer Event Position to that of the mouse position
        pointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        graphicsRaycaster.Raycast(pointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        }
        if(results.Count > 0)
        {
            return true;
        } else 
        {
            return false;
        }
    }
}
