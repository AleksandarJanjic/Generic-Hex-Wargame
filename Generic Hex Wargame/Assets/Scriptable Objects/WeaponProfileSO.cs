using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Weapon Profile", menuName = "ScriptableObjects/Weapon Profile", order = 3)]
public class WeaponProfileSO : ScriptableObject
{
    public string weaponName;
    public int numberOfDice;
    public int toHitNumber;
    public int hexRange;

    public List<AbilityEnums> abilities;
}

[System.Serializable]
public enum AbilityEnums 
{
    SMALL_ARMS,
    TANK_MAIN_GUN,
}
