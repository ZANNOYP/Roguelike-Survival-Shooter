using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 直线子弹发射器
/// </summary>
public class IntervalEmitter : BulletEmitter
{
    // 子弹管理器
    private BulletMgr bulletMgr;
    // 怪物管理器
    private MonsterMgr monsterMgr;
    // 玩家
    private Player player;
    // 计时
    private float timer;
    // 武器运行时数据
    private WeaponRuntimeData runtime;

    public override void Init(BulletMgr bulletMgr, MonsterMgr monsterMgr, Player player)
    {
        this.bulletMgr = bulletMgr;
        this.monsterMgr = monsterMgr;
        this.player = player;
    }

    public override void SetEmitter(WeaponRuntimeData runtime)
    {
        this.runtime = runtime;
    }

    public override void StartEmitter()
    {
        enabled = true;
    }

    public override void StopEmitter()
    {
        enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= runtime.generateInterval)
        {
            timer = 0;
            Fire();
        }
    }
    /// <summary>
    /// 开火
    /// </summary>
    private void Fire()
    {
        Vector2 monsterPos;
        float minDist;
        // 有怪物且怪物进入攻击范围才开火
        if (!monsterMgr.GetNearestMonster(out monsterPos, out minDist) || minDist > runtime.atkRange * runtime.atkRange)
        {                                                                  
            return;
        }

        Vector3 startPos = player.transform.position;
        Vector3 targetPos = monsterPos;
        Vector3 dir = (targetPos - startPos).normalized;
        // 子弹发射数量为1 直接发射一次就返回
        if (runtime.bulletCount <= 1)
        {
            CreateStraightBullet(startPos, dir, runtime, bulletPrefab, 0);
            return;
        }
        // 子弹发射数量超过1时 根据子弹数和散射角度算出每颗子弹相差角度 然后创建子弹
        float angleStep = runtime.spreadAngle / (runtime.bulletCount - 1);
        float startAngle = -runtime.spreadAngle * 0.5f;
        for (int i = 0; i < runtime.bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;
            CreateStraightBullet(startPos, dir, runtime, bulletPrefab, angle);
        }
    }
    /// <summary>
    /// 创建直线子弹
    /// </summary>
    /// <param name="startPos">开始位置</param>
    /// <param name="dir">目标方向</param>
    /// <param name="runtime">武器运行时数据</param>
    /// <param name="angle">子弹偏移角度</param>
    /// <param name="bulletPrefab">子弹预设体</param>
    private void CreateStraightBullet(Vector3 startPos, Vector3 dir, WeaponRuntimeData runtime, GameObject bulletPrefab, float angle)
    {
        GameObject bulletObj = PoolManager.Instance.Pop(bulletPrefab);
        StraightBullet bullet = bulletObj.GetComponent<StraightBullet>();
        bullet.Init(bulletMgr, runtime.damage, startPos, dir, runtime.moveSpeed, runtime.deadTime, bulletPrefab, angle);
        bulletMgr.Register(bullet);
    }
}
