using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProfileFactory : MonoBehaviour
{
    public static UnitProfileFactory instance;
    private void Awake()
    {
        // Ensure there is only one instance, destroy duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        // Set the instance to this object
        instance = this;

        // Optional: Make the GameObject persist across scenes
        DontDestroyOnLoad(gameObject);
    }
    public UnitProfiles CreateProfile(UnitProfileSO unitProfileSO)
    {
        UnitProfiles result = new UnitProfiles();
        result.unitWeapons = new List<WeaponProfile>();

        foreach(WeaponProfileSO weaponProfileSO in unitProfileSO.unitWeapons)
        {
            WeaponProfile profile = WeaponProfileFactory.instance.CreateWeaponProfile(weaponProfileSO);
            result.unitWeapons.Add(profile);
        }

        return result;
    }
}
