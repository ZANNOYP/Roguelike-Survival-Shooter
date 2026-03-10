using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物数据库
/// </summary>
public static class MonsterDatabase
{
    /// <summary>
    /// 得到精英怪数据
    /// </summary>
    /// <param name="baseMonster">原始怪物</param>
    /// <returns></returns>
    public static MonsterConfig GetElite(MonsterConfig baseMonster)
    {
        if (baseMonster.eliteVersion != null)
        {
            return baseMonster.eliteVersion;
        }
        return baseMonster;
    }
}
