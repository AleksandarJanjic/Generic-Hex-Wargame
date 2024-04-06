using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Side
{
    public string name;
    public bool isPlayerControlled;
    public Controller controller;
}
[System.Serializable]
public enum Controller
{
    PLAYER,
    AI,
    OTHER_PLAYER
}
