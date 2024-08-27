using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTerrain : MonoBehaviour
{
    public HexController parent;
    [SerializeField] private TerrainType terrainType;

    public TerrainType GetHexTerrainType()
    {
        return terrainType;
    }
}

public enum TerrainType
{
    PLAINS,
    HILLS,
    MOUNTAINS
}
