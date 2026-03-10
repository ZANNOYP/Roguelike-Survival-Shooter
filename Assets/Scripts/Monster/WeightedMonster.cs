using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物权重数据
/// </summary>
[Serializable]
public class WeightedMonster
{
    public MonsterConfig monsterConfig;// 怪物数据
    public int weight;// 权重
}
