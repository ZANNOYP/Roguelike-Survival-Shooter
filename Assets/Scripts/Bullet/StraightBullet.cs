using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 直线子弹
/// </summary>
public class StraightBullet : Bullet
{
    // 移动速度
    private float moveSpeed;
    // 存活时间
    private float deadTime;
    // 刚体
    //private Rigidbody2D rb;
    // 移动方向
    private Vector3 moveDir;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="bulletMgr"></param>
    /// <param name="damage"></param>
    /// <param name="startPos"></param>
    /// <param name="direction"></param>
    /// <param name="moveSpeed"></param>
    /// <param name="deadTime"></param>
    /// <param name="bulletPrefab"></param>
    /// <param name="angle"></param>
    public void Init(BulletMgr bulletMgr, float damage, Vector3 startPos, Vector3 direction, float moveSpeed, float deadTime, GameObject bulletPrefab, float angle = 0)
    {
        base.Init(bulletMgr, damage);
        transform.position = startPos;
        this.moveDir = Quaternion.Euler(0, 0, angle) * direction;
        this.moveSpeed = moveSpeed;
        this.deadTime = deadTime;
        this.bulletPrefab = bulletPrefab;
        CancelInvoke("Die");
        Invoke("Die", deadTime);
    }

    protected override void Move()
    {
        //rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        transform.position = transform.position + moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().Wound(damage);
            Die();
        }
    }
}
