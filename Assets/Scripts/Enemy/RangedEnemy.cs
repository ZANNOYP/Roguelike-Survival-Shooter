using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 远程敌人
/// </summary>
public class RangedEnemy : EnemyBase
{
    // 投射物配置数据
    public ProjectileConfig projectileConfig;
    // 一次移动时间
    private float moveTime;
    // 移动定时器
    private float timer;
    // 攻击伤害
    private float atkDamage;
    // 攻击间隔
    private float atkInterval;
    // 穿透数
    private int penetrateCount;
    // 击退速度
    private float repelSpeed;
    // 击退时间
    private float repelTime;
    // 投射物数量
    private int projectileCount;
    // 发散角度
    private float spreadAngle;
    // 攻击协程
    private Coroutine atkCoroutine;

    protected override void Start()
    {
        base.Start();
        
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(atkInterval);
        }
    }

    public override void Dead()
    {
        base.Dead();
        if (atkCoroutine != null)
        {
            StopCoroutine(atkCoroutine);
            atkCoroutine = null;
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="rangedEnemyConfig"></param>
    public void Init(RangedEnemyConfig rangedEnemyConfig)
    {
        moveTime = rangedEnemyConfig.moveTime;
        atkDamage = rangedEnemyConfig.atkDamage;
        atkInterval = rangedEnemyConfig.atkInterval;
        penetrateCount = rangedEnemyConfig.penetrateCount;
        repelSpeed = rangedEnemyConfig.repelSpeed;
        repelTime = rangedEnemyConfig.repelTime;
        projectileCount = rangedEnemyConfig.projectileCount;
        spreadAngle = rangedEnemyConfig.spreadAngle;

        atkCoroutine = StartCoroutine(AttackCoroutine());

        timer = 0;
        Vector2 dir = Random.insideUnitCircle.normalized;
        Vector2 vel = dir * moveSpeed;
        rb.velocity = vel;
    }

    protected override void Move()
    {
        if (isDead) return;
        if (isRepelled) return;

        timer += Time.deltaTime;
        if (timer > moveTime)
        {
            timer = 0;
            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector2 vel = dir * moveSpeed;
            rb.velocity = vel;
        }

        Boundary boundary = BoundaryManager.instance.GetBoundary(col.bounds);
        transform.position = BoundaryManager.instance.ClampPosition(transform.position, boundary);
    }

    protected override void Attack()
    {
        // 创建投射物初始化数据
        ProjectileData projectileData = CreateProjectileData(projectileConfig);
        // 投射物移动方向
        Vector2 dir = Random.insideUnitCircle.normalized;
        // 发射投射物
        FireProjectiles(projectileData, projectileConfig, dir);
    }

    /// <summary>
    /// 创建投射物数据
    /// </summary>
    /// <param name="projectileConfig"></param>
    /// <returns></returns>
    public ProjectileData CreateProjectileData(ProjectileConfig projectileConfig)
    {
        ProjectileData projectileData = new ProjectileData();

        projectileData.birthPos = transform.position;
        projectileData.damage = atkDamage;
        projectileData.penetrateCount = penetrateCount;
        projectileData.moveSpeed = projectileConfig.moveSpeed;
        projectileData.lifetime = projectileConfig.lifetime;
        RepelData repelData = new RepelData();
        repelData.repelSpeed = repelSpeed;
        repelData.repelTime = repelTime;
        projectileData.repelData = repelData;
        projectileData.faction = projectileConfig.faction;
        projectileData.behaviorType = projectileConfig.behaviorType;

        return projectileData;
    }

    /// <summary>
    /// 生成投射物
    /// </summary>
    /// <param name="rwConfig"></param>
    /// <param name="dir"></param>
    public void FireProjectiles(ProjectileData projectileData, ProjectileConfig projectileConfig, Vector2 dir)
    {
        // 单发
        if (projectileCount == 1)
        {
            ProjectileBase projectile = ProjectileManager.instance.GetProjectile(projectileConfig.id);
            projectileData.moveDir = dir;
            projectile.Init(projectileData);
            return;
        }
        // 散射
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 startDir = Quaternion.Euler(new Vector3(0, 0, -spreadAngle / 2)) * dir;
            float angle = spreadAngle / (projectileCount - 1);
            Vector2 diri = Quaternion.Euler(new Vector3(0, 0, angle * i)) * startDir;

            ProjectileBase projectile = ProjectileManager.instance.GetProjectile(projectileConfig.id);

            projectileData.moveDir = diri;
            projectile.Init(projectileData);
        }
    }
}
