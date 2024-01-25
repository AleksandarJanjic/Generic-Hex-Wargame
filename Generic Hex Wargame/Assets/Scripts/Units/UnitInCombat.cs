using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitInCombat 
{
    public int id;
    public string name;
    public int HP;
    public UnitType unitType;
    public Side side;
    public UnitProfiles currentProfile;
    public List<StatusEffect> statusEffects;
    public WeaponProfileResults profileResults;
    public CombatResult combatResult;

    // This constructor could be changed to only take the id
    // And then use list of all units and reconstruct it with id only
    public UnitInCombat(int id, string name, int HP, UnitType type, Side side, UnitProfiles profile,
                        List<StatusEffect> statuses)
    {
        this.id = id;
        this.name = name;
        this.HP = HP;
        unitType = type;
        this.side = side;
        currentProfile = profile;
        statusEffects = statuses;

        combatResult = new CombatResult();
        statusEffects = new List<StatusEffect>();
        profileResults = new WeaponProfileResults();
        profileResults.results = new List<DieRoll>();
    }
}
