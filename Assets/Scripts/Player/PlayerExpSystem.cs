using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家经验值系统
/// </summary>
public class PlayerExpSystem : MonoBehaviour
{
    // 经验文本改变事件
    public static event Action<int, int> OnExpChanged;
    // 等级文本改变事件
    public static event Action<int> OnLevelChanged;
    // 升级事件
    public static event Action<int> OnLevelUp;
    // 当前等级
    private int level;
    // 默认等级
    private int defaultLevel = 1;
    // 当前经验值
    private int currentExp;
    // 默认经验值
    private int defaultCurrentExp = 0;
    // 升级所需经验值
    private int maxExp;
    // 默认升级所需经验值
    private int defaultMaxExp = 12;

    public int Level => level;
    public int CurrentExp => currentExp;
    public int MaxExp => maxExp;

    private void Awake()
    {
        // 初始化
        Init();
    }

    private void OnEnable()
    {
        // 怪物死亡事件注册
        Monster.OnMonsterDead += OnMonsterDead;
        // 游戏流程事件注册
        GameFlowEvents.OnGameReset += Init;
    }

    private void OnDisable()
    {
        // 怪物死亡事件反注册
        Monster.OnMonsterDead -= OnMonsterDead;
        // 游戏流程事件反注册
        GameFlowEvents.OnGameReset -= Init;
    }
    /// <summary>
    /// 怪物死亡事件
    /// </summary>
    /// <param name="monster">死亡的怪物</param>
    private void OnMonsterDead(Monster monster)
    {
        // 增加经验
        AddExp(monster.exp);
    }
    /// <summary>
    /// 增加经验方法
    /// </summary>
    /// <param name="value"></param>
    private void AddExp(int value)
    {
        currentExp += value;
        OnExpChanged?.Invoke(currentExp, maxExp);
        while (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// 升级方法
    /// </summary>
    private void LevelUp()
    {
        currentExp -= maxExp;
        level++;
        maxExp = maxExp + (level - 1) * 5;

        OnExpChanged?.Invoke(currentExp, maxExp);
        OnLevelChanged?.Invoke(level);
        OnLevelUp?.Invoke(level);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        level = defaultLevel;
        currentExp = defaultCurrentExp;
        maxExp = defaultMaxExp;
        OnExpChanged?.Invoke(currentExp, maxExp);
        OnLevelChanged?.Invoke(level);
    }
}
