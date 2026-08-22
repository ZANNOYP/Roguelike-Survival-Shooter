using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 投射物Id
/// </summary>
public enum ProjectileId
{
    /// <summary>
    /// 手枪子弹
    /// </summary>
    PistolBullet,
    /// <summary>
    /// 冲锋枪子弹
    /// </summary>
    SMGBullet,
    /// <summary>
    /// 霰弹枪子弹
    /// </summary>
    ShotgunBullet,
    /// <summary>
    /// 敌人子弹
    /// </summary>
    EnemyBullet,
}

/// <summary>
/// 投射物行为类型
/// </summary>
public enum ProjectileBehaviorType
{
    /// <summary>
    /// 直线投射物
    /// </summary>
    Straight,
}

/// <summary>
/// 投射物管理器
/// </summary>
public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;
    // 投射物配置数据列表
    public List<ProjectileConfig> projectileConfigs = new List<ProjectileConfig>();
    // 投射物存活列表
    private List<ProjectileBase> activeProjectiles = new List<ProjectileBase>();
    // 投射物Id、数据绑定字典
    private Dictionary<ProjectileId, ProjectileConfig> projectileConfigDic = new Dictionary<ProjectileId, ProjectileConfig>();
    // 投射物行为类型对象池字典
    private Dictionary<ProjectileBehaviorType, ObjectPool<ProjectileBase>> pools = new Dictionary<ProjectileBehaviorType, ObjectPool<ProjectileBase>>();
    // 子弹生成协程
    private Coroutine generateCoroutine;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (ProjectileConfig config in projectileConfigs)
        {
            if (!pools.ContainsKey(config.behaviorType))
            {
                pools.Add(config.behaviorType, new ObjectPool<ProjectileBase>(() =>
                {
                    return GameObject.Instantiate(config.prefab).GetComponent<ProjectileBase>();
                }, config.initialSize, config.maxSize));
            }
            projectileConfigDic.Add(config.id, config);
        }
    }

    /// <summary>
    /// 得到一个投射物
    /// </summary>
    /// <returns></returns>
    public ProjectileBase GetProjectile(ProjectileId id)
    {
        ProjectileConfig config = projectileConfigDic[id];
        ProjectileBehaviorType type = config.behaviorType;
        ProjectileBase projectile = pools[type].Get();
        activeProjectiles.Add(projectile);
        return projectile;
    }
    /// <summary>
    /// 回收投射物
    /// </summary>
    /// <param name="projectile"></param>
    public void RemoveProjectile(ProjectileBase projectile)
    {
        if (activeProjectiles.Contains(projectile))
        {
            pools[projectile.behaviorType].Release(projectile);
            activeProjectiles.Remove(projectile);
        }
    }
    /// <summary>
    /// 清屏投射物
    /// </summary>
    public void KillAllBullets()
    {
        for (int i = activeProjectiles.Count - 1; i >= 0; i--) 
        {
            if (!activeProjectiles[i].isDead)
                activeProjectiles[i].Dead();
        }
        activeProjectiles.Clear();
    }
}


