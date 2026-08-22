using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 投射物默认数据
/// </summary>
[CreateAssetMenu(menuName = "Projectile/Config")]
public class ProjectileConfig : ScriptableObject
{
    /// <summary>
    /// 预设体
    /// </summary>
    public GameObject prefab;
    /// <summary>
    /// 子弹移动速度
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 存活时间
    /// </summary>
    public float lifetime;
    /// <summary>
    /// 投射物Id
    /// </summary>
    public ProjectileId id;
    /// <summary>
    /// 投射物行为类型
    /// </summary>
    public ProjectileBehaviorType behaviorType;
    /// <summary>
    /// 对象池初始化容量
    /// </summary>
    public int initialSize;
    /// <summary>
    /// 对象池最大容量
    /// </summary>
    public int maxSize;
    /// <summary>
    /// 所属阵营
    /// </summary>
    public Faction faction;
}
