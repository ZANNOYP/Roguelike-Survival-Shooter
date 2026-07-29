using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponConfig : WeaponConfig
{

    public override void Apply()
    {
        WeaponSystem.instance.Equip(this);
    }
}
