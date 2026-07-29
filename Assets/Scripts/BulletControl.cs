using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹控制
/// </summary>
public class BulletControl : MonoBehaviour
{
    // 是否死亡
    public bool isDead;
    // 刚体
    private Rigidbody2D rb;
    // 穿透次数
    private int penetrateCount;
    // 已伤害敌人
    private HashSet<EnemyHealth> hitTargets = new HashSet<EnemyHealth>();
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
    /// 移动
    /// </summary>
    public void Move()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 vel = moveDir * moveSpeed;
        rb.velocity = vel;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="data"></param>
    public void Init(ProjectileData data)
    {
        SetPos(data.birthPos);
        SetMoveDir(data.moveDir);
        SetDamage(data.damage);
        SetPenetrateCount(data.penetrateCount);
        SetMoveSpeed(data.moveSpeed);
        SetLifeTime(data.lifetime);
        timer = 0;
        SetRotation();

        Rebirth();
        Move();
    }

    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="pos"></param>
    public void SetPos(Vector2 pos)
    {
        transform.position = pos;
    }

    /// <summary>
    /// 设置移动方向向量
    /// </summary>
    /// <param name="dir"></param>
    public void SetMoveDir(Vector2 dir)
    {
        this.moveDir = dir;
    }

    /// <summary>
    /// 设置伤害
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    /// <summary>
    /// 设置穿透次数
    /// </summary>
    /// <param name="penetrateCount"></param>
    public void SetPenetrateCount(int penetrateCount)
    {
        this.penetrateCount = penetrateCount;
    }

    /// <summary>
    /// 设置移动速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    /// <summary>
    /// 设置存活时间
    /// </summary>
    /// <param name="lifetime"></param>
    public void SetLifeTime(float lifetime)
    {
        this.lifetime = lifetime;
    }

    /// <summary>
    /// 设置朝向
    /// </summary>
    public void SetRotation()
    {
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    /// <summary>
    /// 撞击敌人
    /// </summary>
    /// <param name="enemy"></param>
    public void HitEnemy(EnemyHealth enemy)
    {
        // 已经撞击过 返回
        if (hitTargets.Contains(enemy)) return;
        // 添加敌人
        hitTargets.Add(enemy);
        // 敌人掉血
        enemy.ChangeHp(-damage);

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
        Move();
        hitTargets.Clear();
    }

    /// <summary>
    /// 重生
    /// </summary>
    public void Rebirth()
    {
        isDead = false;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 撞击到敌人 触发逻辑
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            HitEnemy(enemy);
        }
        // 撞击到外围墙壁 死亡
        if (collision.gameObject.CompareTag("Wall"))
        {
            Dead();
        }
    }
}
