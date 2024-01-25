using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int id;
    public string name;
    public int HP;
    public UnitType unitType;
    public Side side;
    public UnitProfiles greenProfile, yellowProfile, redProfile;
    public UnitProfiles activeProfile;
    public List<StatusEffect> statusEffects;
}

public enum UnitType
{
    INFANTRY,
    VEHICLE,
    ARMOR
}
