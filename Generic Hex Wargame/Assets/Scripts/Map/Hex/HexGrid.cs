using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [SerializeField] private List<HexController> hexes;

    public HexController GetHexById(int id)
    {
        foreach(HexController hex in hexes)
        {
            if(hex.GetHexId() == id)
            {
                return hex;
            }
        }

        return null;
    }

    public List<HexController> GetAllHexes() 
    { 
        return hexes;
    }

    public void AddHexToGrid(HexController hex)
    {
        if(hexes.Contains(hex))
        {
            return;
        }

        hexes.Add(hex);
    }
}
