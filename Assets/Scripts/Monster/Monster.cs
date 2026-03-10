using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 3f;
    public static event Action<Monster> OnMonsterDead;
    public int exp = 5;
    public int contactDamage = 1;
    private float hp;
    private float maxHp;
    private Player player;
    private MonsterMgr monsterMgr;
    private GameObject monsterPrefab;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        Vector3 toPlayer = player.transform.position - transform.position;
        float dist = toPlayer.magnitude;
        if (dist < 0.05)
        {
            return;
        }
        Vector3 dir = toPlayer / dist;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void Init(Player player, Vector2 pos, MonsterMgr monsterMgr, MonsterConfig config)
    {
        SetPlayer(player);
        SetPos(pos);
        SetMonsterMgr(monsterMgr);
        SetMaxHp(config.hp);
        ResetHp();
        ChangeSpeed(config.moveSpeed);
        this.monsterPrefab = config.monsterPrefab;
        isDead = false;
    }

    private void SetPlayer(Player player)
    {
        this.player = player;
    }

    private void SetPos(Vector2 pos)
    {
        transform.position = pos;
    }

    private void SetMonsterMgr(MonsterMgr monsterMgr)
    {
        this.monsterMgr = monsterMgr;
    }

    private void ResetHp()
    {
        hp = maxHp;
    }

    public void Wound(float demage)
    {
        hp -= demage;
        if (hp <= 0)
        {
            hp = 0;
            Dead();
        }
    }

    private void Dead()
    {
        if (isDead) return;
        isDead = true;
        OnMonsterDead?.Invoke(this);
        monsterMgr.Remove(this); 
        ResetMonster();
        PoolManager.Instance.Push(gameObject, monsterPrefab);
    }

    public void ChangeSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetMaxHp(int maxHp)
    {
        this.maxHp = maxHp;
    }

    public void ResetMonster()
    {
        this.player = null;
        this.monsterMgr = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Wound(contactDamage);
        }
    }
}
