using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HexStackController : MonoBehaviour
{
    public HexController parent;
    public List<UnitController> unitStack;

    public delegate void PlaceUnitOnHex(int unitId, HexController parent);
    public static event PlaceUnitOnHex OnPlaceUnitOnHex;

    public void OnEnable()
    {
        UnitPositionHelper.OnUnitPlacedOnHexEvent += UnitPlaced;
    }

    public void OnDisable()
    {
        UnitPositionHelper.OnUnitPlacedOnHexEvent -= UnitPlaced;
    }

    public void UnitPlaced(UnitController unit, int hexId)
    {
        if(hexId != parent.GetHexId())
        {
            return;
        }

        Debug.Log("Unit " + unit.GetUnitId() + " was placed on hex " + parent.transform.name);

        AddUnitToHex(unit);
        // Place on the positon of hex
        OnPlaceUnitOnHex?.Invoke(unit.GetUnitId(), parent);

    }

    public void AddUnitToHex(UnitController unit)
    {
        unitStack.Add(unit);
    }

    public void RemoveUnitFromHex(UnitController unit)
    {
        unitStack.Remove(unit);
    }
}
