using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹管理器
/// </summary>
public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;
    // 玩家
    public PlayerControl player;
    // 子弹预设体
    public GameObject bulletPrefab;
    // 子弹生成间隔
    public float generateInterval = 0.5f;
    // 子弹池子最大数量
    public int maxBulletCount = 30;
    // 子弹伤害
    public int damage = 1;
    // 子弹穿透次数
    public int penetrateCount = 3;
    // 子弹列表
    private List<BulletControl> bullets = new List<BulletControl>();
    // 池子满后要取的子弹当前索引
    private int nowIndex;
    // 子弹生成协程
    private Coroutine generateCoroutine;
    // 玩家运行时数据
    private PlayerData playerData;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerData = DataManager.instance.playerRuntimeData;
    }

    /// <summary>
    /// 得到一颗子弹
    /// </summary>
    /// <returns></returns>
    public BulletControl GetBullet(ProjectileConfig projectileConfig)
    {
        BulletControl bullet;
        if (bullets.Count < maxBulletCount)
        {
            GameObject bulletObj = GameObject.Instantiate(projectileConfig.prefab);
            bullet = bulletObj.GetComponent<BulletControl>();
            bullets.Add(bullet);
        }
        else
        {
            bullet = bullets[nowIndex];
            nowIndex++;
            if (nowIndex >= maxBulletCount)
            {
                nowIndex = 0;
            }
        }
        return bullet;
    }

    /// <summary>
    /// 杀死所有子弹
    /// </summary>
    public void KillAllBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].isDead)
                bullets[i].Dead();
        }
    }
}
