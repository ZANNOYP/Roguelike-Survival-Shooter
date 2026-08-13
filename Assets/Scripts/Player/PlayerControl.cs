using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家控制
/// </summary>
public class PlayerControl : MonoBehaviour
{
    // 是否死亡
    public bool isDead;
    // 出生位置
    public Vector2 rebirthPos;
    // 刚体
    private Rigidbody2D rb;
    // 玩家运行时数据
    private PlayerData playerData;
    // 是否暂停
    private bool isPause;
    // 玩家血量
    private PlayerHealth playerHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        isPause = true;
    }

    private void Start()
    {
        playerData = DataManager.instance.playerRuntimeData;
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
        if (isPause) return;

        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 vel = dir * playerData.moveSpeed;
        rb.velocity = vel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.GetComponent<EnemyBase>();
            playerHealth.ChangeHp(-enemy.ContactDamage);
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Dead()
    {
        gameObject.SetActive(false);
        isDead = true;
    }

    /// <summary>
    /// 重生
    /// </summary>
    public void Rebirth()
    {
        gameObject.SetActive(true);
        DataManager.instance.ResetData();
        playerHealth.RestoreFullHealth();
        PlayerExperience.Instance.ResetExp();
    }

    /// <summary>
    /// 开始控制
    /// </summary>
    public void StartControl()
    {
        isDead = false;
        isPause = false;
    }

    public void StopPause()
    {
        isPause = false;
        playerHealth.RestoreFullHealth();
    }

    public void Pause()
    {
        isPause = true;
        rb.velocity = Vector2.zero;
    }

    public void ResetPos()
    {
        transform.position = rebirthPos;
    }
}
