using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器管理类
/// </summary>
public class WeaponManager : MonoBehaviour
{
    // 武器预设体放置的父对象
    [SerializeField]
    private Transform weaponSocket;
    // 武器选项列表
    [SerializeField]
    private List<WeaponData> weapons = new List<WeaponData>();
    public List<WeaponData> Weapons => weapons;
    // 当前装备武器
    private Weapon currentWeapon;
    public Weapon CurrentWeapon => currentWeapon;

    /// <summary>
    /// 装备武器
    /// </summary>
    /// <param name="weaponPrefab">武器预设体</param>
    public void Equip(Weapon weaponPrefab)
    {
        // 创建一个武器 放入指定父对象层级下
        currentWeapon = Instantiate(weaponPrefab, weaponSocket);
    }

    /// <summary>
    /// 卸载装备
    /// </summary>
    public void Unequip()
    {
        // 如果当前武器不为空 则销毁当前武器的对象
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
    }

    /// <summary>
    /// 脚本激活时，在游戏重置事件中注册卸载装备方法
    /// </summary>
    private void OnEnable()
    {
        GameFlowEvents.OnGameReset += Unequip;
    }

    /// <summary>
    /// 脚本失活时，在游戏重置事件中反注册卸载装备方法
    /// </summary>
    private void OnDisable()
    {
        GameFlowEvents.OnGameReset -= Unequip;
    }
}
