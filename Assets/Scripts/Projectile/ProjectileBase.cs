using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 阵营
/// </summary>
public enum Faction
{
    /// <summary>
    /// 玩家阵容
    /// </summary>
    Player,
    /// <summary>
    /// 敌人阵营
    /// </summary>
    Enemy,
}

/// <summary>
/// 投射物控制
/// </summary>
public class ProjectileBase : MonoBehaviour
{
    // 是否死亡
    public bool isDead;
    // 投射物行为类型
    public ProjectileBehaviorType behaviorType;
    // 刚体
    private Rigidbody2D rb;
    // 穿透次数
    private int penetrateCount;
    // 已伤害目标
    private HashSet<Health> hitTargets = new HashSet<Health>();
    // 伤害
    private float damage;
    // 移动方向向量
    private Vector2 moveDir;
    // 移速
    private float moveSpeed;
    // 计时器
    private float timer;
    // 存活时间
    private float lifetime;
    // 击退数据
    private RepelData repelData;
    // 所属阵营
    private Faction faction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Dead();
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="data"></param>
    public void Init(ProjectileData data)
    {
        // 设置出生位置
        transform.position = data.birthPos;
        // 设置移动方向
        moveDir = data.moveDir;
        // 设置伤害
        damage = data.damage;
        // 设置穿透数
        penetrateCount = data.penetrateCount;
        // 设置移速
        moveSpeed = data.moveSpeed;
        // 设置存活时间
        lifetime = data.lifetime;
        // 重置存活定时器
        timer = 0;
        // 设置朝向
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        // 设置击退数据
        repelData = data.repelData;
        // 子弹重生
        isDead = false;
        gameObject.SetActive(true);
        // 设置刚体移速
        Vector2 vel = moveDir * moveSpeed;
        rb.velocity = vel;
        // 设置阵营
        faction = data.faction;
        // 设置行为类型
        behaviorType = data.behaviorType;
    }


    /// <summary>
    /// 击中目标
    /// </summary>
    /// <param name="target"></param>
    public void HitTarget(Health target)
    {
        // 已经撞击过 返回
        if (hitTargets.Contains(target)) return;
        // 添加敌人
        hitTargets.Add(target);
        // 敌人掉血
        target.ChangeHp(-damage, repelData);

        if (penetrateCount <= 0)
        {
            Dead();
        }
        else
        {
            penetrateCount--;
        }
        
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Dead()
    {
        gameObject.SetActive(false);
        isDead = true;
        rb.velocity = Vector2.zero;
        hitTargets.Clear();
        ProjectileManager.instance.RemoveProjectile(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 撞击到外围墙壁 死亡
        if (collision.gameObject.CompareTag("Wall"))
        {
            Dead();
        }

        if (!collision.TryGetComponent<Health>(out var health))
            return;

        if (CanHit(faction, health.Faction))
            HitTarget(health);
    }

    /// <summary>
    /// 能否击中
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool CanHit(Faction attacker, Faction target)
    {
        if (attacker == Faction.Player && target == Faction.Enemy)
            return true;
        if (attacker == Faction.Enemy && target == Faction.Player)
            return true;

        return false;
    }
}
