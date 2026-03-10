using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 波次数据
/// </summary>
[CreateAssetMenu(menuName ="WaveData")]
public class WaveData : ScriptableObject
{
    // 怪物数量
    public int monsterCount;
    // 刷怪间隔
    public float interval;
    // 怪物权重数据
    public List<WeightedMonster> weightedMonsters;
    // 精英概率
    public float eliteChance;
    /// <summary>
    /// 选择要生成的怪物（是否为精英）
    /// </summary>
    /// <returns></returns>
    public MonsterConfig PickMonster()
    {
        MonsterConfig m = WeightedPick();
        if (Random.value < eliteChance)
            m = MonsterDatabase.GetElite(m);
        return m;
    }
    /// <summary>
    /// 根据权重选择怪物数据
    /// </summary>
    /// <returns></returns>
    private MonsterConfig WeightedPick()
    {
        int total = 0;
        foreach (var weightedMonster in weightedMonsters)
        {
            total += weightedMonster.weight;
        }
        int roll = Random.Range(0, total);
        foreach (var weightedMonster in weightedMonsters)
        {
            roll -= weightedMonster.weight;
            if (roll < 0)
                return weightedMonster.monsterConfig;
        }
        return weightedMonsters[0].monsterConfig;
    }
}
