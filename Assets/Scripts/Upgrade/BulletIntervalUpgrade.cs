using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 减少攻击间隔的升级数据
/// </summary>
[CreateAssetMenu(menuName = "Upgrade/BulletInterval")]
public class BulletIntervalUpgrade : UpgradeData
{
    // 攻击减少的间隔比率
    public float decreaseGenerateInterval = 0.1f;
    // 武器更新数据
    public override void Apply(Weapon weapon)
    {
        weapon.DecreaseGenerateInterval(1 - decreaseGenerateInterval);
    }
}
