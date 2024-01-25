using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeforeRoll
{
    public void BeforeRollEffect(UnitController active, UnitController passive);
    
}
