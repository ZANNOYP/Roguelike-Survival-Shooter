using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 远程武器
/// </summary>
public class RangedWeapon : Weapon
{
    // 开火点
    public Transform firePoint;
    public override void Atk()
    {
        // 没有敌人进入范围 直接返回
        EnemyBase enemy = EnemyManager.Instance.GetAtkEnemy(data.weaponConfig);
        if (enemy == null) return;

        if (!isAimReady) return;
        // 播放音效
        MusicManager.instance.PlayEff(Eff_Type.Gun);

        Vector2 dir = (enemy.transform.position - transform.position).normalized;
        RangedWeaponConfig rwConfig = data.weaponConfig as RangedWeaponConfig;
        ProjectileConfig projectileConfig = rwConfig.projectileConfig;
        // 投射物初始化数据
        ProjectileData projectileData = new ProjectileData();

        projectileData.birthPos = firePoint.position;
        float realDamage = (rwConfig.baseDamage + data.bonusDamage) * playerData.damageMultiplier;
        projectileData.damage = realDamage;
        int realPenetrateCount = (rwConfig.basePenetrateCount + data.bonusPenetrateCount) + playerData.globalPenetrateCount;
        projectileData.penetrateCount = realPenetrateCount;
        projectileData.moveSpeed = projectileConfig.moveSpeed;
        projectileData.lifetime = projectileConfig.lifetime;
        RepelData repelData = new RepelData();
        repelData.repelSpeed = rwConfig.repelSpeed;
        repelData.repelTime = rwConfig.repelTime;
        projectileData.repelData = repelData;
        // 单发
        if (rwConfig.projectileCount == 1)
        {
            BulletControl bullet = BulletManager.instance.GetBullet(projectileConfig);
            projectileData.moveDir = dir;
            bullet.Init(projectileData);
            return;
        }
        // 散射
        for (int i = 0; i < rwConfig.projectileCount; i++)
        {
            Vector2 startDir = Quaternion.Euler(new Vector3(0, 0, -rwConfig.spreadAngle / 2)) * dir;
            float angle = rwConfig.spreadAngle / (rwConfig.projectileCount - 1);
            Vector2 diri = Quaternion.Euler(new Vector3(0, 0, angle * i)) * startDir;

            BulletControl bullet = BulletManager.instance.GetBullet(projectileConfig);

            projectileData.moveDir = diri;
            bullet.Init(projectileData);
        }
    }

    /// <summary>
    /// 设置开火点位置
    /// </summary>
    public void SetFirePointPos()
    {
        WeaponConfig config = data.weaponConfig;
        firePoint.localPosition = config.spritePos;
    }
}
