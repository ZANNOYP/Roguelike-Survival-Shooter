using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 远程敌人配置
/// </summary>
[CreateAssetMenu(menuName ="Enemy/Ranged")]
public class RangedEnemyConfig : EnemyConfig
{
    /// <summary>
    /// 移动时间
    /// </summary>
    public float moveTime;
    /// <summary>
    /// 投射物伤害
    /// </summary>
    public float atkDamage;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float atkInterval;
    /// <summary>
    /// 穿透数
    /// </summary>
    public int penetrateCount;
    /// <summary>
    /// 击退速度
    /// </summary>
    public float repelSpeed;
    /// <summary>
    /// 击退时间
    /// </summary>
    public float repelTime;
    /// <summary>
    /// 投射物数量
    /// </summary>
    public int projectileCount;
    /// <summary>
    /// 发散角度
    /// </summary>
    public float spreadAngle;
}
