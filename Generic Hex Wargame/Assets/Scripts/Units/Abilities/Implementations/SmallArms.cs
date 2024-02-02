using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class SmallArms : Ability, IBeforeRoll
{
    public void BeforeRollEffect(Data.Unit active, Data.Unit passive)
    {
        if(passive.unitType == UnitType.ARMOR)
        {
            List<WeaponProfile> weaponsWithSmallArms = CombatManager.instance.attackingUnitInCombat.currentProfile.GetWeaponProfilesWithAbility(this);

            foreach(WeaponProfile weaponProfile in weaponsWithSmallArms)
            {
                weaponProfile.toHitNumber += 2;
            }
        }
    }
}
