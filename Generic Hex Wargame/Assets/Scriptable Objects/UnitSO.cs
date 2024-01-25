using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit", order = 1)]
public class UnitSO : ScriptableObject
{
    public string unitName;
    public int HP;
    public UnitType UnitType;

    public UnitProfileSO greenProfile;
    public UnitProfileSO yellowProfile;
    public UnitProfileSO redProfile;
}
