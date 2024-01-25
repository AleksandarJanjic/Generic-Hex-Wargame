using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory : MonoBehaviour
{
    public static AbilityFactory instance;
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

    public Ability CreateAbility(AbilityEnums abilityEnum)
    {
        switch(abilityEnum)
        {
            case AbilityEnums.SMALL_ARMS:
                return new SmallArms();
            case AbilityEnums.TANK_MAIN_GUN:
                return new TankMainGun();
            default:
                return new Ability();
        }
    }
}
