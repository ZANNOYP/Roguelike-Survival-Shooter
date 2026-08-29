using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 近战武器配置数据
/// </summary>
[CreateAssetMenu(menuName = "Weapon/Melee")] 
public class MeleeWeaponConfig : WeaponConfig
{
    /// <summary>
    /// 攻击持续时间
    /// </summary>
    public float atkDuration;
    /// <summary>
    /// 碰撞器偏移
    /// </summary>
    public Vector2 colOffset;
    /// <summary>
    /// 碰撞器大小
    /// </summary>
    public Vector2 colSize;
}
