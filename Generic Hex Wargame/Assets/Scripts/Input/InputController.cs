using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public delegate void UnitClicked(int id, bool rightClick);
    public static event UnitClicked OnUnitClicked;

    // Update is called once per frame
    void Update()
    {
        // Get Left Mouse Button click
        if(Input.GetMouseButtonUp(0))
        {
            // Left Mouse Click
            MouseInput(false);
        } else if(Input.GetMouseButtonUp(1))
        {
            // Right Mouse Click
            MouseInput(true);
        }
    }

    public void MouseInput(bool rightClick)
    {
        // TODO check if mouse input is allowed

        // Grab mouse position
        Vector3 mousePosition = Input.mousePosition;

        // Check if there is a ui element here
        // If true stop the check as you are clickig a ui element
        if(CanvasHelper.Instance.IsUIElementAtPosition(mousePosition))
        {
            Debug.Log("UI element at position, cancel mouse click");
            return;
        }

        // Convert screen position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0f));

        // Raycast in the world based on screen pos
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            // Log information about the hit collider
            // Debug.Log("Hit Collider: " + hit.collider.name);
            if(hit.collider.gameObject.GetComponentInParent<UnitController>() != null)
            {
                Debug.Log("Unit was clicked, send event to Battle Manager");
                OnUnitClicked?.Invoke(hit.collider.gameObject.GetComponentInParent<UnitController>().GetUnitId(), rightClick);
            }
            if(hit.collider.gameObject.GetComponentInParent<HexController>() != null)
            {
                Debug.Log("Hex was clicked");

            }
        }
    }
}
