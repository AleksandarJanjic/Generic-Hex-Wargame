using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponProfile
{
    public string weaponName;
    public int numberOfDice;
    public int toHitNumber;
    public int hexRange;
    public List<Ability> abilities;
}
