using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class Unit 
    {
        [SerializeField] public int id;
        [SerializeField] public string unitName;
        [SerializeField] public int HP;
        [SerializeField] public UnitType unitType;
        [SerializeField] public Side side;
        [SerializeField] public List<StatusEffect> statusEffects;
        [SerializeField] public UnitProfiles activeProfile;
        [SerializeField] public UnitSO unitTemplate;
        // temp pos, will be replaced with hex and an enum (deployed, reserve etc)
        [SerializeField] public float posX, posY;


        public Unit(int id)
        {
            this.id = id;
            side = new Side();
            statusEffects = new List<StatusEffect>();
        }

    }
}
