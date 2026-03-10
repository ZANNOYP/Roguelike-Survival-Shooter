using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器类
/// </summary>
public class Weapon : MonoBehaviour 
{
    // 武器默认数据
    [SerializeField]
    private WeaponConfig data;
    // 武器运行时数据
    private WeaponRuntimeData runtime = new WeaponRuntimeData();
    // 发射器
    public BulletEmitter emitter;
    // 升级选项
    public List<UpgradeData> upgrades;
    // 初始化数据是否完成
    private bool isReady = false;

    private void Awake()
    {
        // 游戏流程事件注册
        GameFlowEvents.OnGameStart += OnGameStart;
        GameFlowEvents.OnGameReset += Init;
        GameFlowEvents.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        // 游戏流程事件反注册
        GameFlowEvents.OnGameStart -= OnGameStart;
        GameFlowEvents.OnGameReset -= Init;
        GameFlowEvents.OnGameOver -= OnGameOver;
    }

    private void Start()
    {
        // 初始化数据
        Init();
        Set(runtime);
        emitter.Init(GameRoot.Instance.BulletMgr, GameRoot.Instance.MonsterMgr, GameRoot.Instance.player);
        isReady = true;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    /// <summary>
    /// 开始游戏事件
    /// </summary>
    private void OnGameStart()
    {
        // 没初始化完成就开启协程
        if (!isReady)
        {
            StartCoroutine(DelayStart());
            return;
        }
        // 初始化完成发射器开始发射子弹
        emitter.StartEmitter();
    }

    /// <summary>
    /// 延迟开始协程 等待一帧发射子弹
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayStart()
    {
        yield return null;
        emitter.StartEmitter();
    }

    /// <summary>
    /// 游戏结束事件
    /// </summary>
    private void OnGameOver()
    {
        emitter.StopEmitter();
    }

    /// <summary>
    /// 提供给外界判断当前子弹数是否超过设定子弹数
    /// </summary>
    /// <returns></returns>
    public bool MaxBulletCount()
    {
        return runtime.bulletCount >= data.maxBulletCount;
    }

    /// <summary>
    /// 初始化运行时数据
    /// </summary>
    public void Init()
    {
        runtime.damage = data.defaultDamage;
        runtime.generateInterval = data.defaultGenerateInterval;
        runtime.bulletCount = data.defaultBulletCount;
        runtime.atkRange = data.defaultAtkRange;
        runtime.spreadAngle = data.spreadAngle;
        runtime.moveSpeed = data.defaultMoveSpeed;
        runtime.deadTime = data.defaultDeadTime;
        runtime.rotateSpeed = data.defaultRotateSpeed;
        runtime.radius = data.defaultRadius;
    }

    /// <summary>
    /// 将运行时数据传给发射器
    /// </summary>
    /// <param name="runtime"></param>
    public void Set(WeaponRuntimeData runtime)
    {
        emitter.SetEmitter(runtime);
    }

    /// <summary>
    /// 增加伤害的升级选项
    /// </summary>
    /// <param name="increaseDamage">增加的伤害值</param>
    public void IncreaseDamage(float increaseDamage)
    {
        runtime.damage += increaseDamage;
        // 停止重新发射 用于环绕型子弹升级时 重新生成子弹
        emitter.StopEmitter();
        emitter.StartEmitter();
    }

    /// <summary>
    /// 减小攻击间隔的升级选项
    /// </summary>
    /// <param name="decreaseGenerateInterval">与原攻击间隔的比率</param>
    public void DecreaseGenerateInterval(float decreaseGenerateInterval)
    {
        runtime.generateInterval *= decreaseGenerateInterval;
        // 限制最小间隔
        runtime.generateInterval = Mathf.Max(runtime.generateInterval, 0.15f);
        // 停止重新发射 用于环绕型子弹升级时 重新生成子弹
        emitter.StopEmitter();
        emitter.StartEmitter();
    }

    /// <summary>
    /// 增加子弹数量的升级选项
    /// </summary>
    /// <param name="count">增加的子弹数量</param>
    public void IncreaseCount(int count)
    {
        // 限制最大子弹数量
        runtime.bulletCount = Mathf.Min(runtime.bulletCount + count, data.maxBulletCount);
        // 停止重新发射 用于环绕型子弹升级时 重新生成子弹
        emitter.StopEmitter();
        emitter.StartEmitter();
    }

    /// <summary>
    /// 增加旋转半径的升级选项
    /// </summary>
    /// <param name="radius">与原来旋转半径的比率</param>
    public void IncreaseRadius(float radius)
    {
        runtime.radius *= radius;
        // 停止重新发射 用于环绕型子弹升级时 重新生成子弹
        emitter.StopEmitter();
        emitter.StartEmitter();
    }
}
