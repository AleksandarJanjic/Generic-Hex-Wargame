using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBeforeRollStatusEffect
{
    public void BeforeRollStatusEffect(UnitController active, UnitController passive);
}
