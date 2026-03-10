using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
/// <summary>
/// 玩家血量系统
/// </summary>
public class PlayerHpSystem : MonoBehaviour
{
    // 血量改变事件
    public static event Action<int, int> OnHpChanged;
    // 玩家伤害事件
    public static event Action<int> OnPlayerDamaged;
    // 玩家回血事件
    public static event Action<int> OnPlayerHealed;
    // 玩家死亡事件
    public static event Action OnPlayerDead;
    // 当前血量
    private int nowHp;
    // 最大血量
    private int maxHp;
    // 默认最大血量
    private int defaultMaxHp = 20;
    // 是否死亡
    private bool isDead;

    public int NowHp => nowHp;
    public int MaxHp => maxHp;

    private void Awake()
    {
        // 初始化
        Init();
    }

    private void OnEnable()
    {
        // 血量改变、升级事件注册
        Player.OnHpModifyRequested += ModifyHp;
        PlayerExpSystem.OnLevelUp += OnLevelUp;
        // 游戏流程注册事件注册
        GameFlowEvents.OnGameReset += Init;
    }

    private void OnDisable()
    {
        // 血量改变、升级事件反注册
        Player.OnHpModifyRequested -= ModifyHp;
        PlayerExpSystem.OnLevelUp -= OnLevelUp;
        // 游戏流程注册事件反注册
        GameFlowEvents.OnGameReset -= Init;
    }
    /// <summary>
    /// 改变血量事件
    /// </summary>
    /// <param name="amount"></param>
    private void ModifyHp(int amount)
    {
        // 回血量为0 直接返回
        if (amount == 0) return;
        // 回血量小于0 判定为受伤
        if (amount < 0)
        {
            // 调用玩家受伤事件
            OnPlayerDamaged?.Invoke(-amount);
        }
        // 回血量大于0 判定为回血
        else
        {
            // 调用玩家回血事件
            OnPlayerHealed?.Invoke(amount);
        }
        // 设置当前血量
        nowHp = Mathf.Clamp(nowHp + amount, 0, maxHp);
        // 改变血量文本和血条长度
        OnHpChanged?.Invoke(nowHp, maxHp);
        // 当前血量小于0 并且 没有死亡时
        if (nowHp <= 0 && !isDead) 
        {
            // 改为死亡状态
            isDead = true;
            // 调用玩家死亡事件
            OnPlayerDead?.Invoke();
        }
    }
    /// <summary>
    /// 升级事件
    /// </summary>
    /// <param name="level"></param>
    private void OnLevelUp(int level)
    {
        // 升级增加血量，最少5点血
        ModifyHp(Mathf.Max(5, level));
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        maxHp = defaultMaxHp;
        nowHp = maxHp;
        OnHpChanged?.Invoke(nowHp, maxHp);
        isDead = false;
    }
}
