using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 增加子弹伤害的升级数据
/// </summary>
[CreateAssetMenu(menuName ="Upgrade/BulletDemage")]
public class BulletDamageUpgrade : UpgradeData
{
    // 增加的子弹伤害
    public float increaseDamage = 1;
    // 武器更新数据
    public override void Apply(Weapon weapon)
    {
        weapon.IncreaseDamage(increaseDamage);
    }
}
