using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人控制
/// </summary>
public class EnemyControl : MonoBehaviour
{
    // 移速
    public float moveSpeed = 6f;
    // 是否死亡
    public bool isDead;
    // 刚体
    private Rigidbody2D rb;
    // 追踪玩家
    private PlayerControl player;
    // 敌人生命
    private EnemyHealth health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<EnemyHealth>();
        health.RegisterDeadAction(Dead);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// 移动
    /// </summary>
    public void Move()
    {
        if (isDead) return;
        if (player == null) return;

        Vector2 dir = (player.transform.position - transform.position).normalized;
        Vector2 vel = dir * moveSpeed;
        rb.velocity = vel;
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
    /// 死亡
    /// </summary>
    public void Dead()
    {
        if (isDead) return;
        gameObject.SetActive(false);
        isDead = true;
        WaveManager.instance.DeEnemyCount();
        PlayerExperience.Instance.ChangeExp(1);
    }

    /// <summary>
    /// 重生
    /// </summary>
    public void Rebirth()
    {
        gameObject.SetActive(true);
        isDead = false;
        health.Rebirth();
    }
}
