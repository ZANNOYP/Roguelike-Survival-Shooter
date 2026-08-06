using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 选择武器管理器
/// </summary>
public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;
    // 武器配置数据列表
    public List<WeaponConfig> weaponConfigs = new List<WeaponConfig>();
    // 选择武器面板
    public SelectPanel selectPanel;

    public Action OnWeaponSelected;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 得到数据
    /// </summary>
    /// <returns></returns>
    public List<WeaponConfig> GetWeaponConfigs()
    {
        return weaponConfigs;
    }

    /// <summary>
    /// 选择武器按钮点击事件
    /// </summary>
    /// <param name="config"></param>
    public void SelectWeapon(WeaponConfig config)
    {
        WeaponSystem.instance.Equip(config);
        OnWeaponSelected?.Invoke();
    }
}
