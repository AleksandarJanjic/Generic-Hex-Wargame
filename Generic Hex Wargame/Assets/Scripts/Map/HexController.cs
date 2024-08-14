using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexController : MonoBehaviour
{
    [SerializeField] private int hexId;
    public HexColliderHelper colliderGameObject;

    public int GetHexId()
    {
        return hexId;
    }

    public void SetHexId(int hexId)
    {
        this.hexId = hexId;
    }
}
