using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Profile", menuName = "ScriptableObjects/Weapon Profile", order = 1)]
public class WeaponProfileSO : ScriptableObject
{
    public int numberOfDice;
    public int toHitNumber;
    public int hexRange;
}
