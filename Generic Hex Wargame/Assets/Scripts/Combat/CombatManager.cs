using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
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

    public UnitController attackingUnit, defendingUnit;
    public UnitInCombat attackingUnitInCombat, defendingUnitInCombat;

    public void StartCombat(UnitController attacker, UnitController defender)
    {
        // get info from both units, roll dice, total up hits and deal damage to each
        attackingUnit = attacker;
        defendingUnit = defender;

        attackingUnitInCombat = new UnitInCombat(attacker.id, attacker.name, attacker.HP, attacker.unitType, attacker.side, attacker.activeProfile, attacker.statusEffects);
        defendingUnitInCombat = new UnitInCombat(defender.id, defender.name, defender.HP, defender.unitType, defender.side, defender.activeProfile, defender.statusEffects);

        // go through units and check if they have before roll abilities

        foreach(WeaponProfile weaponProfile in attackingUnitInCombat.currentProfile.unitWeapons)
        {
            foreach(Ability weaponAbility in weaponProfile.abilities)
            {
                if(weaponAbility is IBeforeRoll)
                {
                    IBeforeRoll beforeRollEffect = (IBeforeRoll)weaponAbility;
                    beforeRollEffect.BeforeRollEffect(attacker, defender);
                }
            }
        }

        foreach(WeaponProfile weaponProfile in attackingUnitInCombat.currentProfile.unitWeapons)
        {
            WeaponProfileResults weaponProfileResults = new WeaponProfileResults();
            weaponProfileResults.weaponProfileName = weaponProfile.weaponName;
            weaponProfileResults.results = new List<DieRoll>();

            for(int i = 0; i < weaponProfile.numberOfDice; i++)
            {
                DieRoll roll = new DieRoll();
                roll.originalResult = Random.Range(1, 7);
                weaponProfileResults.results.Add(roll);
                if(roll.originalResult >= weaponProfile.toHitNumber || roll.originalResult == 6)
                {
                    attackingUnitInCombat.combatResult.causedHits++;
                }
            }
            attackingUnitInCombat.combatResult.weaponProfileResults.Add(weaponProfileResults);
        }
        foreach(WeaponProfile weaponProfile in defendingUnitInCombat.currentProfile.unitWeapons)
        {
            WeaponProfileResults weaponProfileResults = new WeaponProfileResults();
            weaponProfileResults.weaponProfileName = weaponProfile.weaponName;
            weaponProfileResults.results = new List<DieRoll>();

            for(int i = 0; i < weaponProfile.numberOfDice /2; i++)
            {
                DieRoll roll = new DieRoll();
                roll.originalResult = Random.Range(1, 7);
                weaponProfileResults.results.Add(roll);
                if(roll.originalResult >= weaponProfile.toHitNumber || roll.originalResult == 6)
                {
                    defendingUnitInCombat.combatResult.causedHits++;
                }
            }
            defendingUnitInCombat.combatResult.weaponProfileResults.Add(weaponProfileResults);
        }

        // go through units and check if they have any post roll abilities

        

        foreach(WeaponProfileResults result in attackingUnitInCombat.combatResult.weaponProfileResults)
        {
            Debug.Log(attackingUnitInCombat.combatResult.PrintResults(result));
        }
        Debug.Log("Attacker caused " + attackingUnitInCombat.combatResult.causedHits + " hits");

        foreach(WeaponProfileResults result in defendingUnitInCombat.combatResult.weaponProfileResults)
        {
            Debug.Log(defendingUnitInCombat.combatResult.PrintResults(result));
        }
        Debug.Log("Defender caused " + defendingUnitInCombat.combatResult.causedHits + " hits");
    }
}
