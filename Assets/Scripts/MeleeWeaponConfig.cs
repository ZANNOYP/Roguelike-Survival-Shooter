using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 近战武器配置数据
/// </summary>
[CreateAssetMenu(menuName = "Weapon/Melee")] 
public class MeleeWeaponConfig : WeaponConfig
{
    // 攻击范围
    public float atkRange;
    // 攻击武器旋转角度
    public float atkAngle;
    // 攻击持续时间
    public float atkDuration;

    public override void Apply()
    {
        WeaponSystem.instance.Equip(this);
    }
}
