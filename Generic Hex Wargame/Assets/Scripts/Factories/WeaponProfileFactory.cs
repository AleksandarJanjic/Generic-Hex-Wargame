using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProfileFactory : MonoBehaviour
{
    public static WeaponProfileFactory instance;
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

    public WeaponProfile CreateWeaponProfile(WeaponProfileSO weaponProfileSO)
    {
        WeaponProfile weaponProfile = new WeaponProfile();
        weaponProfile.weaponName = weaponProfileSO.weaponName;
        weaponProfile.numberOfDice = weaponProfileSO.numberOfDice;
        weaponProfile.toHitNumber = weaponProfileSO.toHitNumber;
        weaponProfile.hexRange = weaponProfileSO.hexRange;

        weaponProfile.abilities = new List<Ability>();

        foreach(AbilityEnums ability in weaponProfileSO.abilities)
        {
            Ability result = AbilityFactory.instance.CreateAbility(ability);
            weaponProfile.abilities.Add(result);
        }

        return weaponProfile;
    }
}
