using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexConnection : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private List<HexController> connectedHexes;
    [SerializeField] private List<HexConnectionType> connectionTypes;

    public void SetConnectionId(int id)
    {
        this.id = id;
    }

    public int GetConnectionId() 
    {
        return this.id;
    }

    public void AddConnectedHex(HexController hex)
    {
        connectedHexes.Add(hex);
    }

    public List<HexController> GetAllHexesOnConnection() 
    {
        return connectedHexes;
    }

    // for a given hex returns a hex that is connected to it trought this connection
    public HexController GetConnectedHex(HexController hex)
    {
        foreach(HexController connectedHex in connectedHexes)
        {
            if(hex != connectedHex)
            {
                return connectedHex;
            }
        }
        return null;
    }

    public void AddConnectionType(HexConnectionType type)
    {
        connectionTypes.Add(type);
    }

    public List<HexConnectionType> GetConnectionTypes()
    {
        return connectionTypes;
    }

    public bool IsConnectionType(HexConnectionType type)
    {
        bool result = false;
        foreach(HexConnectionType connectionType in connectionTypes) 
        {
            if(type == connectionType) 
            {
                result = true;
            }
        }
        return result;
    }
}

public enum HexConnectionType
{
    LAND_CONNECTION
}
