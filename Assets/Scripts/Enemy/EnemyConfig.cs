using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人配置数据
/// </summary>
[CreateAssetMenu(menuName ="Enemy/Config")]
public class EnemyConfig : ScriptableObject
{
    /// <summary>
    /// 预设体
    /// </summary>
    public GameObject prefab;
    /// <summary>
    /// 移速
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 经验值
    /// </summary>
    public int exp;
    /// <summary>
    /// 初始最大生命值
    /// </summary>
    public float maxHp;
    /// <summary>
    /// 对象池初始化容量
    /// </summary>
    public int initialSize;
    /// <summary>
    /// 对象池最大容量
    /// </summary>
    public int maxSize;
    /// <summary>
    /// 敌人类型
    /// </summary>
    public EnemyType type;
    /// <summary>
    /// 颜色
    /// </summary>
    public Color color;
}
