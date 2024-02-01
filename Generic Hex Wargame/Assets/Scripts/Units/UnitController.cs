using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitController : MonoBehaviour
{
    public int id;
    public string unitName;
    public int HP;
    public UnitType unitType;
    public Side side;
    public UnitProfiles greenProfile, yellowProfile, redProfile;
    public UnitProfiles activeProfile;
    public List<StatusEffect> statusEffects;
    public UnitSO unitTemplate;
}

[System.Serializable]
public enum UnitType
{
    INFANTRY,
    VEHICLE,
    ARMOR
}
