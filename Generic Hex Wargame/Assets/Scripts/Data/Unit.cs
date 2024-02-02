using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Unit
    {
        public int id;
        public string unitName;
        public int HP;
        public UnitType unitType;
        public Side side;
        public List<StatusEffect> statusEffects;
        public UnitProfiles activeProfile;
        public UnitSO unitTemplate;
        // temp pos, will be replaced with hex and an enum (deployed, reserve etc)
        public float posX, posY;
    }
}
