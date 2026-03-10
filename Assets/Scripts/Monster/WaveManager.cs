using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 波次怪物数量管理器
/// </summary>
public class WaveManager : MonoBehaviour
{
    // 当前波次总怪物量
    private int totalMonsterCount;
    // 当前波次已生成怪物数量
    private int monsterSpawn;
    // 当前波次怪物死亡数量
    private int monsterDeadCount;
    // 当前波次索引
    private int currentWave;

    // 波次数据
    [SerializeField]
    private List<WaveData> waveDatas = new List<WaveData>();

    // 等待下一波 状态
    private bool waitingNextWave = false;

    // 波次怪物生成器
    public WaveSpawner waveSpawner;

    // 计时
    private float timer;

    // 怪物数量改变事件
    public static Action<int> monsterCountOnChanged;
    // 当前波次改变事件
    public static Action<int> nowWaveOnChanged;
    // 总波次初始化事件
    public static Action<int> totalWaveOnChanged;
    // 开始波次
    private bool isWaving = false;

    #region 内部方法
    /// <summary>
    /// 怪物生成数量增加
    /// </summary>
    private void MonsterSpawn()
    {
        monsterSpawn++;
    }
    /// <summary>
    /// 怪物死亡数量增加
    /// </summary>
    private void MonsterDead(Monster monster)
    {
        monsterDeadCount++;
    }
    /// <summary>
    /// 波次数+1
    /// </summary>
    private void AddWave()
    {
        currentWave++;
    }

    /// <summary>
    /// 还需要杀死的怪物数量
    /// </summary>
    /// <returns></returns>
    private int NeedAttack()
    {
        return totalMonsterCount - monsterDeadCount;
    }
    /// <summary>
    /// 波次是否需要生成怪物
    /// </summary>
    /// <returns></returns>
    private bool NeedSpawn()
    {
        return monsterSpawn < totalMonsterCount;
    }
    /// <summary>
    /// 所有波次是否已经结束
    /// </summary>
    /// <returns></returns>
    private bool AllWavesEnd()
    {
        return currentWave >= waveDatas.Count;
    }
    /// <summary>
    /// 得到当前波次数据
    /// </summary>
    /// <returns></returns>
    private WaveData GetData()
    {
        WaveData data = waveDatas[currentWave];
        totalMonsterCount = data.monsterCount;
        return data;
    }
    /// <summary>
    /// 初始化生成怪物数和怪物死亡数
    /// </summary>
    private void Init()
    {
        monsterSpawn = 0;
        monsterDeadCount = 0;
    }
    #endregion

    private void OnEnable()
    {
        Monster.OnMonsterDead += MonsterDead;
        GameFlowEvents.OnGameStart += StartWave;
    }

    private void OnDisable()
    {
        Monster.OnMonsterDead -= MonsterDead;
        GameFlowEvents.OnGameStart -= StartWave;
    }

    void Update()
    {
        // 没开始就返回
        if (!isWaving) return;
        // 正在等待下一波 返回
        if (waitingNextWave) return;
        
        // 得到当前波次数据
        WaveData data = GetData();
        // 怪物数文本改变
        monsterCountOnChanged?.Invoke(NeedAttack());
        // 总波次数文本设置
        totalWaveOnChanged?.Invoke(waveDatas.Count);
        // 更新当前波次
        nowWaveOnChanged?.Invoke(currentWave + 1);
        // 计时
        timer += Time.deltaTime;
        // 当时间大于生成间隔 且 当前波次怪物数量小于波次数据的怪物数量 生成怪物
        if (timer > data.interval && NeedSpawn())
        {
            // 怪物数+1
            MonsterSpawn();
            // 计时重置
            timer = 0;
            // 生成怪物
            waveSpawner.Spawn(data);
        }
        // 当前波次怪物数量大于波次数据的怪物数量 转到下一波
        if (!NeedSpawn() && !waitingNextWave) 
        {
            // 开启下一波协程
            waitingNextWave = true;
            StartCoroutine(StartNextWave());
        }
    }

    /// <summary>
    /// 开始下一波协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartNextWave()
    {
        while (totalMonsterCount != monsterDeadCount)  
        {
            // 怪物数文本改变
            monsterCountOnChanged?.Invoke(NeedAttack());
            yield return null;
        }
        // 怪物数文本改变
        monsterCountOnChanged?.Invoke(NeedAttack());
        // 波次索引+1
        AddWave();
        // 当全部波次结束时 不再生成怪物
        if (AllWavesEnd())
        {
            EndWave();
            // 怪物数重置
            Init();
            // 计时重置
            timer = 0;
            // 当前波次数重置
            currentWave = 0;
            // 等待下一波状态重置
            waitingNextWave = false;
            // 游戏结束
            GameFlow.Instance.EndGame();
            yield break;
        }
        yield return new WaitForSeconds(5f);
        // 怪物数重置
        Init();
        // 计时重置
        timer = 0;
        // 等待下一波状态重置
        waitingNextWave = false;
    }
    /// <summary>
    /// 开始波次
    /// </summary>
    public void StartWave()
    {
        isWaving = true;
    }

    /// <summary>
    /// 结束波次
    /// </summary>
    private void EndWave()
    {
        isWaving = false;
    }
}
