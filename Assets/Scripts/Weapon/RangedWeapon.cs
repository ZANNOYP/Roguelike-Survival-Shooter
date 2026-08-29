using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
        //// 瞄准未结束 返回
        //if (!isAimReady) return;
        // 播放音效
        PlayAttackSound();
        // 武器配置数据
        RangedWeaponConfig rwConfig = data.weaponConfig as RangedWeaponConfig;
        // 创建投射物初始化数据
        ProjectileData projectileData = CreateProjectileData(rwConfig);
        // 投射物移动方向
        Vector2 dir = (enemy.transform.position - transform.position).normalized;
        // 发射投射物
        FireProjectiles(projectileData, rwConfig, dir);
    }

    /// <summary>
    /// 播放攻击音效
    /// </summary>
    private void PlayAttackSound()
    {
        MusicManager.instance.PlayEff(Eff_Type.Gun);
    }

    /// <summary>
    /// 创建投射物数据
    /// </summary>
    /// <param name="rwConfig"></param>
    /// <returns></returns>
    public ProjectileData CreateProjectileData(RangedWeaponConfig rwConfig)
    {
        ProjectileData projectileData = new ProjectileData();

        projectileData.birthPos = firePoint.position;
        float realDamage = (rwConfig.baseDamage + data.bonusDamage) * playerData.damageMultiplier;
        projectileData.damage = realDamage;
        int realPenetrateCount = (rwConfig.basePenetrateCount + data.bonusPenetrateCount) + playerData.globalPenetrateCount;
        projectileData.penetrateCount = realPenetrateCount;
        projectileData.moveSpeed = rwConfig.projectileConfig.moveSpeed;
        projectileData.lifetime = rwConfig.projectileConfig.lifetime;
        RepelData repelData = new RepelData();
        repelData.repelSpeed = rwConfig.repelSpeed;
        repelData.repelTime = rwConfig.repelTime;
        projectileData.repelData = repelData;
        projectileData.faction = rwConfig.projectileConfig.faction;
        projectileData.behaviorType = rwConfig.projectileConfig.behaviorType;

        return projectileData;
    }

    /// <summary>
    /// 生成投射物
    /// </summary>
    /// <param name="rwConfig"></param>
    /// <param name="dir"></param>
    public void FireProjectiles(ProjectileData projectileData, RangedWeaponConfig rwConfig, Vector2 dir)
    {
        // 单发
        if (rwConfig.projectileCount == 1)
        {
            ProjectileBase projectile = ProjectileManager.instance.GetProjectile(rwConfig.projectileConfig.id);
            projectileData.moveDir = dir;
            projectile.Init(projectileData);
            return;
        }
        // 散射
        for (int i = 0; i < rwConfig.projectileCount; i++)
        {
            Vector2 startDir = Quaternion.Euler(new Vector3(0, 0, -rwConfig.spreadAngle / 2)) * dir;
            float angle = rwConfig.spreadAngle / (rwConfig.projectileCount - 1);
            Vector2 diri = Quaternion.Euler(new Vector3(0, 0, angle * i)) * startDir;

            ProjectileBase projectile = ProjectileManager.instance.GetProjectile(rwConfig.projectileConfig.id);

            projectileData.moveDir = diri;
            projectile.Init(projectileData);
        }
    }

    /// <summary>
    /// 设置开火点位置
    /// </summary>
    public void SetFirePointPos()
    {
        WeaponConfig config = data.weaponConfig;
        RangedWeaponConfig rwConfig = config as RangedWeaponConfig;
        firePoint.localPosition = rwConfig.firePoint;
    }
}
