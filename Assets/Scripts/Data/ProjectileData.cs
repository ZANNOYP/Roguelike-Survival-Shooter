using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 投射物初始化数据
/// </summary>
public class ProjectileData
{
    /// <summary>
    /// 出生位置
    /// </summary>
    public Vector2 birthPos;
    /// <summary>
    /// 移动方向向量
    /// </summary>
    public Vector2 moveDir;
    /// <summary>
    /// 子弹伤害
    /// </summary>
    public float damage;
    /// <summary>
    /// 穿透次数
    /// </summary>
    public int penetrateCount;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 存活时间
    /// </summary>
    public float lifetime;
    /// <summary>
    /// 击退数据
    /// </summary>
    public RepelData repelData;
    /// <summary>
    /// 所属阵营
    /// </summary>
    public Faction faction;
    // 行为类型
    public ProjectileBehaviorType behaviorType;
}
