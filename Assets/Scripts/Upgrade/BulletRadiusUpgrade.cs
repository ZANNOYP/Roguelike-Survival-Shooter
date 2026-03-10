using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 增加旋转半径的升级数据
/// </summary>
[CreateAssetMenu(menuName = "Upgrade/BulletRadius")]
public class BulletRadiusUpgrade : UpgradeData
{
    // 增加的子弹旋转半径
    public float increaseRadius = 0.2f;
    // 武器更新数据
    public override void Apply(Weapon weapon)
    {
        weapon.IncreaseRadius(1 + increaseRadius);
    }
}
