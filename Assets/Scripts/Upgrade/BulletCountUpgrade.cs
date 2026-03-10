using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 增加子弹数量的升级数据
/// </summary>
[CreateAssetMenu(menuName = "Upgrade/BulletCount")]
public class BulletCountUpgrade : UpgradeData
{
    // 增加的子弹数量
    public int bulletCount = 1;
    // 武器更新数据
    public override void Apply(Weapon weapon)
    {
        weapon.IncreaseCount(bulletCount);
    }
}
