using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioInfo : MonoBehaviour
{
    public static ScenarioInfo instance;
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

    public List<UnitSO> scenarioUnits;

    public UnitSO GetUnitSOByName(string unitName)
    {
        foreach(UnitSO unit in scenarioUnits)
        {
            if(unit.unitName.Equals(unitName))
            {
                return unit;
            }
        }
        return scenarioUnits[0];
    }
}
