using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAfterRoll
{
    public void AfterRollEffect(UnitController active, UnitController passive);
}
