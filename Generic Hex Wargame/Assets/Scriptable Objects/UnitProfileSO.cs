using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Unit Profile", menuName = "ScriptableObjects/Unit Profile", order = 2)]
public class UnitProfileSO : ScriptableObject
{
    public List<WeaponProfileSO> unitWeapons;
}
