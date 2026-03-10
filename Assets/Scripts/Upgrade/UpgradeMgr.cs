using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 升级管理器
/// </summary>
public class UpgradeMgr : MonoBehaviour
{
    // 玩家
    [SerializeField]
    private Player player;
    // 升级面板
    [SerializeField]
    private UpgradePanel upgradePanel;
    // 武器管理器
    public WeaponManager weaponManager;
    // 升级选项
    private List<UpgradeData> upgrades;
    // 初始化升级选项
    public void Init()
    {
        upgrades = weaponManager.CurrentWeapon.upgrades;
    }

    private void OnEnable()
    {
        // 升级事件注册
        PlayerExpSystem.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        // 升级事件反注册
        PlayerExpSystem.OnLevelUp -= OnLevelUp;
    }
    /// <summary>
    /// 升级事件
    /// </summary>
    /// <param name="level">当前等级</param>
    private void OnLevelUp(int level)
    {
        // 时间暂停
        Time.timeScale = 0f;
        // 显示升级面板
        upgradePanel.Show(upgrades);
        
    }
    /// <summary>
    /// 升级选项点击事件
    /// </summary>
    /// <param name="data"></param>
    public void OnUpgradeSelected(UpgradeData data)
    {
        // 更行武器数据
        data.Apply(weaponManager.CurrentWeapon);
        // 隐藏更新面板
        upgradePanel.Hide();
        // 时间重启
        Time.timeScale = 1f;
    }
}
