using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexController : MonoBehaviour
{
    private int hexId;

    public int GetHexId()
    {
        return hexId;
    }

    public void SetHexId(int hexId)
    {
        this.hexId = hexId;
    }
}
