using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家运行时数据
/// </summary>
public class PlayerData
{
    /// <summary>
    /// 移速
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 最大血量
    /// </summary>
    public int maxHp;
    /// <summary>
    /// 伤害倍率
    /// </summary>
    public float damageMultiplier;
    /// <summary>
    /// 射速倍率
    /// </summary>
    public float fireRateMultiplier;
    /// <summary>
    /// 子弹穿透数
    /// </summary>
    public int globalPenetrateCount;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="config"></param>
    public void Init(PlayerConfig config)
    {
        moveSpeed = config.moveSpeed;
        maxHp = config.maxHp;
        damageMultiplier = config.damageMultiplier;
        fireRateMultiplier = config.fireRateMultiplier;
        globalPenetrateCount = config.globalPenetrateCount;
    }
}
