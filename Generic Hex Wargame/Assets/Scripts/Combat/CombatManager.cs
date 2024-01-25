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
    public CombatResult attackerResult, defenderResult;

    public void StartCombat(UnitController attacker, UnitController defender)
    {
        // get info from both units, roll dice, total up hits and deal damage to each
        attackingUnit = attacker;
        defendingUnit = defender;

        UnitInCombat attackingUnitInCombat = new UnitInCombat(attacker.id, attacker.name, attacker.HP, attacker.unitType, attacker.side, attacker.activeProfile, attacker.statusEffects);
        UnitInCombat defendingUnitInCombat = new UnitInCombat(defender.id, defender.name, defender.HP, defender.unitType, defender.side, defender.activeProfile, defender.statusEffects);

        // go through units and check if they have before roll abilities

        attackerResult = new CombatResult();
        defenderResult = new CombatResult();

        foreach(WeaponProfile weaponProfile in attacker.activeProfile.unitWeapons)
        {
            WeaponProfileResults weaponProfileResults = new WeaponProfileResults();
            weaponProfileResults.weaponProfileName = weaponProfile.weaponName;
            weaponProfileResults.results = new List<DieRoll>();

            for(int i = 0; i < weaponProfile.numberOfDice; i++)
            {
                DieRoll roll = new DieRoll();
                roll.originalResult = Random.Range(1, 7);
                weaponProfileResults.results.Add(roll);
                if(roll.originalResult >= weaponProfile.toHitNumber)
                {
                    attackerResult.causedHits++;
                }
            }
            attackerResult.weaponProfileResults.Add(weaponProfileResults);
        }
        foreach(WeaponProfile weaponProfile in defender.activeProfile.unitWeapons)
        {
            WeaponProfileResults weaponProfileResults = new WeaponProfileResults();
            weaponProfileResults.weaponProfileName = weaponProfile.weaponName;
            weaponProfileResults.results = new List<DieRoll>();

            for(int i = 0; i < weaponProfile.numberOfDice /2; i++)
            {
                DieRoll roll = new DieRoll();
                roll.originalResult = Random.Range(1, 7);
                weaponProfileResults.results.Add(roll);
                if(roll.originalResult >= weaponProfile.toHitNumber)
                {
                    defenderResult.causedHits++;
                }
            }
            defenderResult.weaponProfileResults.Add(weaponProfileResults);
        }

        // go through units and check if they have any post roll abilities

        

        foreach(WeaponProfileResults result in attackerResult.weaponProfileResults)
        {
            Debug.Log(attackerResult.PrintResults(result));
        }
        Debug.Log("Attacker caused " + attackerResult.causedHits + " hits");

        foreach(WeaponProfileResults result in defenderResult.weaponProfileResults)
        {
            Debug.Log(defenderResult.PrintResults(result));
        }
        Debug.Log("Defender caused " + defenderResult.causedHits + " hits");
    }
}
