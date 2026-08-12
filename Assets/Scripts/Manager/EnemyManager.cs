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
    // 敌人生成角度范围与半径距离
    public float minAngle = 0;
    public float maxAngle = 360f;
    public float minRadius = 10f;
    public float maxRadius = 15f;

    public List<EnemyConfig> enemyConfigs = new List<EnemyConfig>(); 
    public List<EnemyType> enemyTypes = new List<EnemyType>(); 
    // 敌人列表
    private List<EnemyBase> activeEnemies = new List<EnemyBase>();
    // 池子满后要取的敌人当前索引
    private int nowIndex;

    private Dictionary<EnemyType,EnemyConfig> enemyConfigDic = new Dictionary<EnemyType,EnemyConfig>();
    private Dictionary<EnemyType, ObjectPool<EnemyBase>> pools = new Dictionary<EnemyType, ObjectPool<EnemyBase>>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (EnemyConfig config in enemyConfigs)
        {
            pools.Add(config.type, new ObjectPool<EnemyBase>(() =>
            {
                return GameObject.Instantiate(config.prefab).GetComponent<EnemyBase>();
            }, config.initialSize, config.maxSize));

            enemyConfigDic.Add(config.type, config);
            enemyTypes.Add(config.type);
        }
    }

    /// <summary>
    /// 生成敌人
    /// </summary>
    public void RealGenerate(EnemyType type)
    {
        EnemyBase enemy = pools[type].Get();
        if (enemy == null) return ;

        // 初始化 设置位置 设置玩家引用
        enemy.SetPlayer(player);
        float angle = Random.Range(minAngle, maxAngle);
        float radius = Random.Range(minRadius, maxRadius);
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector2 pos = player.transform.position + dir * radius;
        enemy.SetPos(pos);

        EnemyConfig config = enemyConfigDic[type];
        enemy.InitData(config);
        enemy.Rebirth();
        activeEnemies.Add(enemy);
    }


    /// <summary>
    /// 获取离玩家最近敌人
    /// </summary>
    /// <returns></returns>
    public EnemyBase GetNearestEnemy()
    {
        float minDistance = float.MaxValue;
        EnemyBase enemy = null;
        for (int i = 0; i < activeEnemies.Count; i++) 
        {
            if (activeEnemies[i].isDead) continue;

            float nowDistance = Vector3.Distance(activeEnemies[i].transform.position, player.transform.position);
            if (nowDistance < minDistance) 
            {
                minDistance = nowDistance;
                enemy = activeEnemies[i];
            }
        }
        return enemy;
    }

    /// <summary>
    /// 获取离玩家最近可攻击敌人
    /// </summary>
    /// <returns></returns>
    public EnemyBase GetAtkEnemy(WeaponConfig weaponConfig)
    {
        EnemyBase enemy = GetNearestEnemy();
        if (enemy == null) return enemy;

        float nowDistance = Vector3.Distance(enemy.transform.position, player.transform.position);
        if (nowDistance > weaponConfig.range)
        {
            enemy = null;
        }
        return enemy;
    }

    /// <summary>
    /// 移除死亡敌人
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveDeadEnemy(EnemyBase enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            pools[enemy.type].Release(enemy);
            activeEnemies.Remove(enemy);
        }
    }

    /// <summary>
    /// 杀死所有敌人
    /// </summary>
    public void DieAll()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (!activeEnemies[i].isDead)
                activeEnemies[i].Dead();
        }
        activeEnemies.Clear();
    }

    /// <summary>
    /// 得到存活敌人数量
    /// </summary>
    /// <returns></returns>
    public int GetAliveEnemyCount()
    {
        int aliveCount = 0;
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (!activeEnemies[i].isDead)
            {
                aliveCount++;
            }
        }
        return aliveCount;
    }
}
