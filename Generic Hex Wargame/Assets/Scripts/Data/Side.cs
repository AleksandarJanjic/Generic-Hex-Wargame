using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side
{
    public string name;
    public bool isPlayerControlled;
    public Controller controller;
}
public enum Controller
{
    PLAYER,
    AI,
    OTHER_PLAYER
}
