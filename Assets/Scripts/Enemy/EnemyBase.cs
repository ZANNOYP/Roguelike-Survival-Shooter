using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人基类
/// </summary>
public class EnemyBase : MonoBehaviour
{
    // 是否死亡
    public bool isDead;
    // 特效
    public ParticleSystem parSystem;
    /// <summary>
    /// 敌人行为类型
    /// </summary>
    public EnemyBehaviorType type;
    // 碰撞伤害
    public float ContactDamage => contactDamage;

    // 是否被击退
    protected bool isRepelled;
    // 移速
    protected float moveSpeed;
    // 刚体
    protected Rigidbody2D rb;
    // 碰撞器
    protected Collider2D col;

    // 追踪玩家
    private PlayerControl player;
    // 敌人生命
    private EnemyHealth health;
    // 动画
    private Animator anim;
    // 图片渲染
    private SpriteRenderer sr;
    // 碰撞伤害
    private float contactDamage;
    // 经验值
    private int exp;
    

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        health.RegisterDeadAction(Dead);
        parSystem.GetComponent<ParticleSystem>().gameObject.SetActive(false);
        health.RegisterRepelAction(Repel);
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        isDead = true;
    }

    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    /// <summary>
    /// 移动
    /// </summary>
    protected virtual void Move()
    {
        if (isDead) return;
        if (player == null) return;
        if (isRepelled) return;

        Vector2 dir = (player.transform.position - transform.position).normalized;
        Vector2 vel = dir * moveSpeed;
        rb.velocity = vel;

        Boundary boundary = BoundaryManager.instance.GetBoundary(col.bounds);
        transform.position = BoundaryManager.instance.ClampPosition(transform.position, boundary);
    }

    /// <summary>
    /// 初始化敌人数据
    /// </summary>
    /// <param name="config"></param>
    public void InitData(EnemyConfig config)
    {
        moveSpeed = config.moveSpeed;
        exp = config.exp;
        sr.color = config.color;
        health.SetMaxHp(config.maxHp);
        type = config.behaviorType;
        contactDamage = config.contactDamage;
    }

    /// <summary>
    /// 敌人击退
    /// </summary>
    public void Repel(RepelData repelData)
    {
        StartCoroutine(RepelCoroutine(repelData));
    }

    /// <summary>
    /// 敌人击退协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator RepelCoroutine(RepelData repelData)
    {
        isRepelled = true;
        Vector2 dir = (transform.position - player.transform.position).normalized;
        float repelSpeed = repelData.repelSpeed;
        Vector2 vel = dir * repelSpeed;
        rb.velocity = vel;
        float timer = 0;
        float repelTime = repelData.repelTime;
        while (timer < repelTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        isRepelled = false;
    }

    /// <summary>
    /// 设置追踪玩家
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(PlayerControl player)
    {
        this.player = player;
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
    /// 死亡，处理逻辑以及播放动画
    /// </summary>
    public virtual void Dead()
    {
        if (isDead) return;
        isDead = true;
        WaveManager.instance.DeEnemyCount();
        PlayerExperience.Instance.ChangeExp(exp);
        anim.SetBool("isDead", isDead);
        parSystem.gameObject.SetActive(true);
        parSystem.Play();
        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// 重生
    /// </summary>
    public void Rebirth()
    {
        //gameObject.SetActive(true);
        isDead = false;
        health.RestoreFullHealth();
        anim.SetBool("isDead", isDead);
    }

    /// <summary>
    /// 死亡动画完毕后隐藏
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;
        parSystem.gameObject.SetActive(false);
        parSystem.Stop();
        EnemyManager.Instance.RemoveDeadEnemy(this);
    }

    /// <summary>
    /// 攻击方法（远程使用）
    /// </summary>
    protected virtual void Attack()
    {

    }
}
