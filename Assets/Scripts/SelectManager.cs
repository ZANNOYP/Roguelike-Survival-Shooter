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
    // 玩家
    public PlayerControl player;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 显示
    /// </summary>
    public void Show()
    {
        selectPanel.Show(weaponConfigs);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public void Hide()
    {
        selectPanel.Hide();
    }

    /// <summary>
    /// 选择武器按钮点击事件
    /// </summary>
    /// <param name="config"></param>
    public void SelectWeapon(WeaponConfig config)
    {
        config.Apply();
        Hide();
        player.Rebirth();
        WaveManager.instance.StartWaveLoop();
        GamePanel.instance.Show();
    }
}
