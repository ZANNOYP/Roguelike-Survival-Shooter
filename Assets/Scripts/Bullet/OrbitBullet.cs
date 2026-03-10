using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 环绕子弹
/// </summary>
public class OrbitBullet : Bullet
{
    // 环绕中心
    private Transform center;
    // 子弹所在圆的角度
    private float angle;
    // 旋转速度
    private float rotateSpeed;
    // 旋转半径
    private float radius;
    // 刚体
    //private Rigidbody2D rb;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
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
    /// <param name="center"></param>
    /// <param name="rotateSpeed"></param>
    /// <param name="radius"></param>
    public void Init(BulletMgr bulletMgr, Transform center, WeaponRuntimeData runtime, GameObject bulletPrefab)
    {
        base.Init(bulletMgr, runtime.damage);
        this.center = center;
        this.rotateSpeed = runtime.rotateSpeed;
        this.radius = runtime.radius;
        this.bulletPrefab = bulletPrefab;
    }

    protected override void Move()
    {
        angle += rotateSpeed * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }
    /// <summary>
    /// 设置子弹所在圆的初始角度
    /// </summary>
    /// <param name="angle"></param>
    public void SetInitialAngle(float angle)
    {
        this.angle = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().Wound(damage);
        }
    }
}
