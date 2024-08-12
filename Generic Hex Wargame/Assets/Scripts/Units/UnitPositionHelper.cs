using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class UnitPositionHelper : MonoBehaviour
{
    public UnitController parent;
    public GameObject unitParent;

    public delegate void UnitPlacedOnHexDelegate(UnitController parent, int hexId);
    public static event UnitPlacedOnHexDelegate OnUnitPlacedOnHexEvent;

    public void OnEnable()
    {
        HexStackController.OnPlaceUnitOnHex += PlaceUnitOnHex;
    }

    public void OnDisable()
    {
        HexStackController.OnPlaceUnitOnHex -= PlaceUnitOnHex;
    }

    public void UnitPlacedOnHex(UnitController unitId, int hexId)
    {
        OnUnitPlacedOnHexEvent?.Invoke(unitId, hexId);
    }

    public void PlaceUnitOnHex(int unitId, HexController hex)
    {
        if(unitId != parent.GetUnitId())
        {
            return;
        }
        unitParent.transform.position = hex.transform.position;
    }
}
