using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController : MonoBehaviour
{
    [SerializeField] private List<HexConnection> connections;

    public void AddConnection(HexConnection connection)
    {
        connections.Add(connection);
    }
}
