using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class HexConnectionController : MonoBehaviour
{
    public HexController parent;

    [SerializeField] private List<HexConnection> hexConnections;

    public void AddConnectionToHex(HexConnection connection)
    {
        hexConnections.Add(connection);
    }

    // Returns hex connection object that is connectiong this hex and hex passed as argument
    public HexConnection GetConnectionToHex(HexController hex)
    {
        HexConnection result = null;
        foreach(HexConnection connection in hexConnections) 
        {
            foreach(HexController controller in connection.GetAllHexesOnConnection())
            {
                if(controller == hex)
                {
                    result = connection;
                    break;
                }
            }
            if(result != null)
            {
                break;
            }
        }
        return result;
    }
}
