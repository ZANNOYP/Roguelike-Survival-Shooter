using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人管理器
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    // 追踪玩家
    public PlayerControl player;
    // 敌人预设体
    public GameObject enemyPrefab;
    // 敌人生成角度范围与半径距离
    public float minAngle = 0;
    public float maxAngle = 360f;
    public float minRadius = 10f;
    public float maxRadius = 15f;
    // 敌人池子最大数量
    public int maxEnemyCount = 30;
    // 敌人列表
    private List<EnemyControl> enemies = new List<EnemyControl>();
    // 池子满后要取的敌人当前索引
    private int nowIndex;
    
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 生成敌人
    /// </summary>
    public void RealGenerate()
    {
        // 得到一个敌人
        EnemyControl enemy;
        if (enemies.Count < maxEnemyCount)
        {
            GameObject enemyObj = GameObject.Instantiate(enemyPrefab);
            enemy = enemyObj.GetComponent<EnemyControl>();
            enemies.Add(enemy);
        }
        else
        {
            enemy = enemies[nowIndex];
            nowIndex++;
            if (nowIndex >= maxEnemyCount)
            {
                nowIndex = 0;
            }
        }
        // 初始化 设置位置 设置玩家引用
        enemy.SetPlayer(player);
        float angle = Random.Range(minAngle, maxAngle);
        float radius = Random.Range(minRadius, maxRadius);
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector2 pos = player.transform.position + dir * radius;
        enemy.SetPos(pos);
        enemy.Rebirth();
    }

    /// <summary>
    /// 获取离玩家最近敌人
    /// </summary>
    /// <returns></returns>
    public EnemyControl GetNearestEnemy()
    {
        float minDistance = float.MaxValue;
        EnemyControl enemy = null;
        for (int i = 0; i < enemies.Count; i++) 
        {
            if (enemies[i].isDead) continue;

            float nowDistance = Vector3.Distance(enemies[i].transform.position, player.transform.position);
            if (nowDistance < minDistance) 
            {
                minDistance = nowDistance;
                enemy = enemies[i];
            }
        }
        return enemy;
    }

    /// <summary>
    /// 获取离玩家最近可攻击敌人
    /// </summary>
    /// <returns></returns>
    public EnemyControl GetAtkEnemy(WeaponConfig weaponConfig)
    {
        EnemyControl enemy = GetNearestEnemy();
        if (enemy == null) return enemy;

        float nowDistance = Vector3.Distance(enemy.transform.position, player.transform.position);
        if (nowDistance > weaponConfig.range)
        {
            enemy = null;
        }
        return enemy;
    }

    /// <summary>
    /// 杀死所有敌人
    /// </summary>
    public void DieAll()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].isDead)
                enemies[i].Dead();
        }
    }

    /// <summary>
    /// 得到存活敌人数量
    /// </summary>
    /// <returns></returns>
    public int GetAliveEnemyCount()
    {
        int aliveCount = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].isDead)
            {
                aliveCount++;
            }
        }
        return aliveCount;
    }
}
