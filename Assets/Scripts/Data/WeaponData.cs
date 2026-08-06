using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器运行时数据
/// </summary>
public class WeaponData
{
    /// <summary>
    /// 武器配置数据
    /// </summary>
    public WeaponConfig weaponConfig;
    /// <summary>
    /// 额外伤害
    /// </summary>
    public float bonusDamage;
    /// <summary>
    /// 额外穿透次数
    /// </summary>
    public int bonusPenetrateCount;

    /// <summary>
    /// 初始化保存武器配置数据
    /// </summary>
    /// <param name="config"></param>
    public void Init(WeaponConfig config)
    {
        weaponConfig = config;
    }

    /// <summary>
    /// 得到武器伤害
    /// </summary>
    /// <returns></returns>
    public float GetDamage()
    {
        return weaponConfig.baseDamage + bonusDamage;
    }
}
