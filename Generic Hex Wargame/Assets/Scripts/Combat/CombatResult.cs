using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class CombatResult
{
    public int causedHits;
    public List<StatusEffect> causedStatusEffects;
    public List<WeaponProfileResults> weaponProfileResults;

    public CombatResult()
    {
        causedStatusEffects = new List<StatusEffect>();
        weaponProfileResults = new List<WeaponProfileResults>();
    }

    public string PrintResults(WeaponProfileResults result)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(result.weaponProfileName + " ");
        for(int i = 0; i < result.results.Count; i ++)
        {
            if(!result.results[i].rerolled)
            {
                sb.Append(" " + result.results[i].originalResult + " ");
            } else 
            {
                sb.Append(" " + result.results[i].rerolledResult + " ");
            }
        }
        return sb.ToString();
    }
}
