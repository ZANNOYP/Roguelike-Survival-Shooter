using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器默认数据
/// </summary>
[CreateAssetMenu(menuName = "Weapon/BulletWeapon")]
public class WeaponConfig : ScriptableObject
{
    public float defaultGenerateInterval = 0.15f;// 默认攻击间隔
    public float defaultDamage = 3f;// 默认伤害
    public int maxBulletCount = 10;// 最大子弹数量
    public int defaultBulletCount = 1;// 默认子弹数量
    public float spreadAngle = 40f;// 散射角度
    public float defaultAtkRange = 8f;// 默认攻击距离
    public float defaultMoveSpeed = 5f;// 默认移动速度
    public float defaultDeadTime = 0.5f;// 默认子弹存活时间
    public float defaultRotateSpeed = 5f;// 默认旋转速度
    public float defaultRadius = 2f;// 默认旋转半径
}
