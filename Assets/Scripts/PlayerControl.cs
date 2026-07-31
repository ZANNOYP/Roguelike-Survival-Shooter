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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isPause = true;
    }

    private void Start()
    {
        PlayerHealth.instance.RegisterDeadAction(Dead);
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
            PlayerHealth.instance.ChangeHp(-1);
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Dead()
    {
        gameObject.SetActive(false);
        isDead = true;
        WeaponSystem.instance.UnEquipAll();
    }

    /// <summary>
    /// 重生
    /// </summary>
    public void Rebirth()
    {
        gameObject.SetActive(true);
        transform.position = rebirthPos;
        DataManager.instance.ResetData();
        PlayerHealth.instance.Rebirth();
        PlayerExperience.Instance.ResetExp();
        isDead = false;
        isPause = false;
    }

    public void StopPause()
    {
        PlayerHealth.instance.Rebirth();
        isPause = false;
    }

    public void Pause()
    {
        rb.velocity = Vector2.zero;
        isPause = true;
    }

    public void ResetPos()
    {
        transform.position = rebirthPos;
    }
}
