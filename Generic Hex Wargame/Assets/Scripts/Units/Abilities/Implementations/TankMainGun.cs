using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMainGun : Ability, IBeforeRoll
{
    public void BeforeRollEffect(UnitController active, UnitController passive)
    {
        if (passive.unitType == UnitType.INFANTRY)
        {
            List<WeaponProfile> weaponsTankMainGun = CombatManager.instance.attackingUnitInCombat.currentProfile.GetWeaponProfilesWithAbility(this);

            foreach (WeaponProfile weaponProfile in weaponsTankMainGun)
            {
                weaponProfile.toHitNumber += 1;
            }
        }
    }
}
