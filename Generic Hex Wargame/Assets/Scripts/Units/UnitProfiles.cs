using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitProfiles
{
    public List<WeaponProfile> unitWeapons;

    #region helpers

    public List<WeaponProfile> GetWeaponProfilesWithAbility(Ability ability)
    {
        List<WeaponProfile> result = new List<WeaponProfile>();
        foreach(WeaponProfile weaponProfile in unitWeapons)
        {
            if(weaponProfile.abilities.Contains(ability))
            {
                result.Add(weaponProfile);
            }
        }
        return result;
    }

    #endregion
}
