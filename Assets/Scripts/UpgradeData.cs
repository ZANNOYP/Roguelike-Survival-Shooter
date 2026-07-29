using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 强化数据
/// </summary>
[CreateAssetMenu(menuName ="UpgradeData")]
public class UpgradeData : ScriptableObject
{
    // 强化名字
    public string upgradeName;
    // 描述
    public string description;

    // 移速增加
    public float moveSpeedAdd;
    // 最大血量增加
    public int maxHpAdd;
    // 伤害倍率增加
    public float damageMultiplierAdd;
    // 射速倍率增加
    public float fireRateMultiplierAdd;
    // 子弹穿透数增加
    public int penetrateCountAdd;

    /// <summary>
    /// 应用强化
    /// </summary>
    public void Apply()
    {
        PlayerData playerData = DataManager.instance.playerRuntimeData;
        playerData.moveSpeed += moveSpeedAdd;
        playerData.maxHp += maxHpAdd;
        playerData.damageMultiplier += damageMultiplierAdd;
        playerData.fireRateMultiplier += fireRateMultiplierAdd;
        playerData.globalPenetrateCount += penetrateCountAdd;
    }
}
