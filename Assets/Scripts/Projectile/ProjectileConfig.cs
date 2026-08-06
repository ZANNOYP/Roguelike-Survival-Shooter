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
}
