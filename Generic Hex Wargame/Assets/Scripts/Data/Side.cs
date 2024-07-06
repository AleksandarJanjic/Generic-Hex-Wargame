using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Side
{
    [SerializeField] public string name;
    [SerializeField] public bool isPlayerControlled;
    [SerializeField] public Controller controller;

    //public Side()
    //{

    //}
}

[System.Serializable]
public enum Controller
{
    PLAYER,
    AI,
    OTHER_PLAYER
}
