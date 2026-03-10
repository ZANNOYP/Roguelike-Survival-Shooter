using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器选择管理器
/// </summary>
public class WeaponSelectedManager : MonoBehaviour
{
    // 武器选择面板
    [SerializeField]
    private ChooseWeaponPanel chooseWeaponPanel;
    // 武器管理器
    [SerializeField]
    private WeaponManager weaponManager;
    // 升级管理器
    [SerializeField]
    private UpgradeMgr upgradeMgr;

    private void OnEnable()
    {
        // 游戏流程事件注册
        GameFlowEvents.OnWeaponChoose += StartChooseWeapon;
    }

    private void OnDisable()
    {
        // 游戏流程事件反注册
        GameFlowEvents.OnWeaponChoose -= StartChooseWeapon;
    }

    /// <summary>
    /// 开始选择武器事件
    /// </summary>
    private void StartChooseWeapon()
    {
        // 显示选择武器面板
        chooseWeaponPanel.Show(weaponManager.Weapons);
    }

    /// <summary>
    /// 武器选择点击事件
    /// </summary>
    /// <param name="data"></param>
    public void OnWeaponSelected(WeaponData data)
    {
        // 装备武器
        data.Apply(weaponManager);
        // 升级管理器初始化升级列表
        upgradeMgr.Init();
        // 隐藏面板
        chooseWeaponPanel.Hide();
        // 开始游戏流程事件
        GameFlow.Instance.StartGame();
    }
}
