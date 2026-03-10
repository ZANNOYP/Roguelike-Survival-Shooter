using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器选项
/// </summary>
[CreateAssetMenu(menuName ="WeaponData/Basic")]
public class WeaponData : ScriptableObject
{
    // 武器名字
    public string Name;
    // 武器描述
    public string Description;
    // 武器预设体
    public Weapon weaponPrefab;

    // 武器选项点击
    public void Apply(WeaponManager weaponManager)
    {
        // 装备武器
        weaponManager.Equip(weaponPrefab);
    }
}
