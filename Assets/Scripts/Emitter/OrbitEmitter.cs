using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 环绕子弹发射器
/// </summary>
public class OrbitEmitter : BulletEmitter
{
    // 子弹管理器
    private BulletMgr bulletMgr;
    // 玩家
    private Player player;
    // 武器运行时数据
    private WeaponRuntimeData runtime;

    public override void Init(BulletMgr bulletMgr, MonsterMgr monsterMgr, Player player)
    {
        this.bulletMgr = bulletMgr;
        this.player = player;
    }

    public override void SetEmitter(WeaponRuntimeData runtime)
    {
        this.runtime = runtime;
    }

    public override void StartEmitter()
    {
        Fire();
    }

    public override void StopEmitter()
    {
        if (bulletMgr != null)
        {
            bulletMgr.ClearAllBullets();
        }
    }
    /// <summary>
    /// 开火
    /// </summary>
    private void Fire()
    {
        float angleStep = Mathf.PI * 2f / runtime.bulletCount;
        OrbitBullet bullet;
        for (int i = 0; i < runtime.bulletCount; i++)
        {
            float angle = angleStep * i;
            bullet = CreateOrbitBullet(player.transform, runtime, bulletPrefab);
            bullet.SetInitialAngle(angle);
        }
    }
    /// <summary>
    /// 创建环绕子弹
    /// </summary>
    /// <param name="center">环绕中心</param>
    /// <param name="runtime">武器运行时数据</param>
    /// <param name="bulletPrefab">子弹预设体</param>
    /// <returns></returns>
    private OrbitBullet CreateOrbitBullet(Transform center, WeaponRuntimeData runtime, GameObject bulletPrefab)
    {
        GameObject bulletObj = PoolManager.Instance.Pop(bulletPrefab);
        OrbitBullet bullet = bulletObj.GetComponent<OrbitBullet>();
        bullet.Init(bulletMgr, center, runtime, bulletPrefab);
        bulletMgr.Register(bullet);
        return bullet;
    }
}
