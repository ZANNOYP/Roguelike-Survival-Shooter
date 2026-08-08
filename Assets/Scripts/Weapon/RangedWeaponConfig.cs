using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 远程武器配置数据
/// </summary>
[CreateAssetMenu(menuName = "Weapon/Ranged")] 
public class RangedWeaponConfig : WeaponConfig
{
    /// <summary>
    /// 投射物数量
    /// </summary>
    public int projectileCount;
    /// <summary>
    /// 发散角度
    /// </summary>
    public float spreadAngle;
    /// <summary>
    /// 基础穿透次数
    /// </summary>
    public int basePenetrateCount;
    /// <summary>
    /// 投射物配置数据
    /// </summary>
    public ProjectileConfig projectileConfig;
    
}
