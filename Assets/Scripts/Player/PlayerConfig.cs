using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家默认数据
/// </summary>
[CreateAssetMenu(menuName = "Player/Config")] 
public class PlayerConfig : ScriptableObject
{
    /// <summary>
    /// 初始移速
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 初始最大血量
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
    /// 全局子弹穿透数
    /// </summary>
    public int globalPenetrateCount;
}
