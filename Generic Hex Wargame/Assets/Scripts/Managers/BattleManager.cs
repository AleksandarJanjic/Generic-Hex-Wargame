using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject tempUnitsParent;
    public UnitController selectedUnit;
    Dictionary<int, UnitController> allUnits;
    private List<Side> sides;

    void OnEnable()
    {
        InputController.OnUnitClicked += UnitIsClicked;
    }

    void OnDisable()
    {
        InputController.OnUnitClicked -= UnitIsClicked;
    }

    void Start()
    {
        SidesSetup();   
        UnitsSetup();
    }

    public void SidesSetup()
    {
        sides = new List<Side>();
        Side sideA = new Side();
        sideA.name = "Side A";
        sideA.isPlayerControlled = true;
        sideA.controller = Controller.PLAYER;
        Side sideB = new Side();
        sideB.name = "Side B";
        sideB.isPlayerControlled = false;
        sideB.controller = Controller.AI;
        sides.Add(sideA);
        sides.Add(sideB);
    }

    public void UnitsSetup()
    {
        allUnits = new Dictionary<int, UnitController>();
        // assign units id's and add them to dictionary
        // TODO load them from game manager
        // for now collect them from scene
        for(int i = 0; i < tempUnitsParent.transform.childCount; i++)
        {
            UnitController unitController = tempUnitsParent.transform.GetChild(i).GetComponent<UnitController>();
            unitController.id = i;

            if(i == 0 || i == 1)
            {
                unitController.activeProfile = UnitProfileFactory.instance.CreateProfile(ScenarioInfo.instance.GetUnitSOByName("Infantry Division").greenProfile);
            } else if(i == 2)
            {
                unitController.activeProfile = UnitProfileFactory.instance.CreateProfile(ScenarioInfo.instance.GetUnitSOByName("Rifle Division").greenProfile);
            } else if(i == 3)
            {
                unitController.activeProfile = UnitProfileFactory.instance.CreateProfile(ScenarioInfo.instance.GetUnitSOByName("Tank Division").greenProfile);
            }
            //unitController.activeProfile.unitWeapons = new List<WeaponProfile>();
            //WeaponProfile smallArms = new WeaponProfile();
            //smallArms.numberOfDice = 6;
            //smallArms.toHitNumber = 4;
            //smallArms.abilities = new List<Ability>();
            //smallArms.abilities.Add(new SmallArms());
            //unitController.activeProfile.unitWeapons.Add(smallArms);

            if(i == 0 || i == 1)
            {
                unitController.side = sides[0];
            } else 
            {
                unitController.side = sides[1];
            }
            allUnits.Add(i, unitController);
        }
    }

    public void UnitIsClicked(int id, bool rightClick)
    {
        // get the unit clicked from dictionary 
        UnitController unitTemp;
        allUnits.TryGetValue(id, out unitTemp);
        // check if the unit clicked is friendly
        if(unitTemp.side.controller == Controller.PLAYER && !rightClick)
        {
            Debug.Log("Unit selected");
            selectedUnit = unitTemp;
        } else if(unitTemp.side.controller != Controller.PLAYER && rightClick && selectedUnit != null)
        {
            Debug.Log("Start combat");
            //Go to combat ui
            CombatManager.instance.StartCombat(selectedUnit, unitTemp);
        }
    }
}
