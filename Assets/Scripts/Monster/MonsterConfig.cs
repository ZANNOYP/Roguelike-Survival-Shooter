using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物数据
/// </summary>
[CreateAssetMenu(menuName ="MonsterConfig")]
public class MonsterConfig : ScriptableObject
{
    public GameObject monsterPrefab;// 预设体
    public int hp;// 血量
    public int contactDamage;// 碰撞伤害
    public float moveSpeed;// 移动速度
    public int exp;// 经验值

    public MonsterConfig eliteVersion;// 精英版本怪物数据
}
