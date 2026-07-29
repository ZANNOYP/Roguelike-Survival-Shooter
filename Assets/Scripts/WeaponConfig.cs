using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器默认数据
/// </summary>
[CreateAssetMenu(menuName ="Weapon/Config")]
public abstract class WeaponConfig : ScriptableObject
{
    /// <summary>
    /// 武器名字
    /// </summary>
    public string weaponName;
    /// <summary>
    /// 武器描述
    /// </summary>
    public string description;
    /// <summary>
    /// 基础伤害
    /// </summary>
    public float baseDamage;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float atkInterval;
    /// <summary>
    /// 武器预设体
    /// </summary>
    public GameObject prefab;
    /// <summary>
    /// 武器射程
    /// </summary>
    public float range;
    /// <summary>
    /// 选择武器应用
    /// </summary>
    public abstract void Apply();
}
