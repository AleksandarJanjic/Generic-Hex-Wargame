using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    [HideInInspector]
    public ScenarioData scenarioData;
    public GameObject tempUnitsParent;
    public GameObject unitPrefab;
    public Data.Unit selectedUnit;
    public Dictionary<int, Data.Unit> allUnits;
    private List<Side> sides;

    private string filePath = "Assets/Resources/Scenario File.json";

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

    public void LoadUnits()
    {
        string jsonText = System.IO.File.ReadAllText(filePath);

        // Deserialize the JSON string into your data structure
        scenarioData = JsonConvert.DeserializeObject<ScenarioData>(jsonText);

        // Now 'myData' contains the deserialized data, and you can use it as needed
        Debug.Log("Deserialized data: " + scenarioData.ToString());
    }

    public void UnitsSetup()
    {
        LoadUnits();
        allUnits = new Dictionary<int, Data.Unit>();

        for(int i = 0; i < scenarioData.units.Count; i++)
        {
            allUnits.Add(i, scenarioData.units[i]);
        }


        for(int i = 0; i < tempUnitsParent.transform.childCount; i++)
        {
            Destroy(tempUnitsParent.transform.GetChild(i).gameObject);
        }
        
        // assign units id's and add them to dictionary
        // TODO load them from game manager
        // for now collect them from scene

        // Create all the units here, with their UnitControllers and fill them with data
        for(int i = 0; i < allUnits.Count; i++)
        {
            GameObject unit = Instantiate(unitPrefab, new Vector3(allUnits[i].posX, allUnits[i].posY, 0), Quaternion.identity, tempUnitsParent.transform);
            unit.AddComponent<UnitController>();
        }

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
                Debug.Log(sides[0].controller);
                unitController.side = sides[0];
            } else 
            {
                unitController.side = sides[1];
            }
            // allUnits.Add(i, unitController);
        }
    }

    public Data.Unit GetUnitData(int id)
    {
        Data.Unit result = new Data.Unit();
        Data.Unit reference = allUnits[id];
        string json = JsonConvert.SerializeObject(reference);
        result = JsonConvert.DeserializeObject<Data.Unit>(json);
        return result;
    }

    public void UnitIsClicked(int id, bool rightClick)
    {
        // get the unit clicked from dictionary 
        Data.Unit unitTemp;
        allUnits.TryGetValue(id, out unitTemp);
        Debug.Log(unitTemp.side);
        Debug.Log(unitTemp.side.controller);
        // check if the unit clicked is friendly
        if(unitTemp.side.controller == Controller.PLAYER && !rightClick)
        {
            Debug.Log("Unit selected");
            selectedUnit = unitTemp;
        } else if(unitTemp.side.controller != Controller.PLAYER && rightClick && selectedUnit != null)
        {
            Debug.Log("Start combat");
            //Go to combat ui
            CombatManager.instance.StartCombat(selectedUnit.id, unitTemp.id);
        }
    }
}
