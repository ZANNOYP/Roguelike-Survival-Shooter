using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人ID
/// </summary>
public enum EnemyId
{
    /// <summary>
    /// 普通近战
    /// </summary>
    NormalMelee,
    /// <summary>
    /// 快速近战
    /// </summary>
    FastMelee,
    /// <summary>
    /// 坦克近战
    /// </summary>
    TankMelee,
    /// <summary>
    /// 普通远程
    /// </summary>
    NormalRanged,
}

/// <summary>
/// 敌人行为类型
/// </summary>
public enum EnemyBehaviorType
{
    /// <summary>
    /// 普通
    /// </summary>
    Normal,
    /// <summary>
    /// 快速
    /// </summary>
    Fast,
    /// <summary>
    /// 远程
    /// </summary>
    Shooter,
    /// <summary>
    /// 坦克
    /// </summary>
    Tank,

    /// <summary>
    /// 近战
    /// </summary>
    Melee,
    /// <summary>
    /// 远程
    /// </summary>
    Ranged,
}

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
    // 敌人数据
    public List<EnemyConfig> enemyConfigs = new List<EnemyConfig>();
    // 敌人Id列表属性
    public List<EnemyId> EnemyIds => enemyIds;
    // 敌人Id列表
    private List<EnemyId> enemyIds = new List<EnemyId>(); 
    // 敌人存活列表
    private List<EnemyBase> activeEnemies = new List<EnemyBase>();
    // 敌人Id、数据绑定字典
    private Dictionary<EnemyId, EnemyConfig> enemyConfigDic = new Dictionary<EnemyId, EnemyConfig>();
    // 敌人行为类型对象池字典
    private Dictionary<EnemyBehaviorType, ObjectPool<EnemyBase>> pools = new Dictionary<EnemyBehaviorType, ObjectPool<EnemyBase>>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (EnemyConfig config in enemyConfigs)
        {
            if (!pools.ContainsKey(config.behaviorType))
            {
                pools.Add(config.behaviorType, new ObjectPool<EnemyBase>(() =>
                {
                    return GameObject.Instantiate(config.prefab).GetComponent<EnemyBase>();
                }, config.initialSize, config.maxSize));
            }

            enemyConfigDic.Add(config.id, config);
            enemyIds.Add(config.id);
        }
    }

    /// <summary>
    /// 生成敌人
    /// </summary>
    public void RealGenerate(EnemyId id)
    {
        EnemyConfig config = enemyConfigDic[id];
        EnemyBehaviorType type = config.behaviorType;
        EnemyBase enemy = pools[type].Get();
        if (enemy == null) return;

        // 初始化 设置位置 设置玩家引用
        enemy.SetPlayer(player);
        float angle = Random.Range(minAngle, maxAngle);
        float radius = Random.Range(minRadius, maxRadius);
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector2 pos = player.transform.position + dir * radius;
        enemy.SetPos(pos);

        enemy.InitData(config);
        enemy.Rebirth();
        activeEnemies.Add(enemy);
        RangedEnemy rangedEnemy = enemy as RangedEnemy;
        RangedEnemyConfig rangedEnemyConfig = config as RangedEnemyConfig;
        if (rangedEnemy != null)
        {
            rangedEnemy.Init(rangedEnemyConfig);
        }
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
        for (int i = activeEnemies.Count - 1; i >= 0; i--) 
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
