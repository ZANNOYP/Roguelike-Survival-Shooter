using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 波次生成器
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    // 玩家
    public Player player;
    
    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 生成怪物
    /// </summary>
    public void Spawn(WaveData data)
    {
        MonsterConfig monsterConfig = data.PickMonster();

        GameObject monsterObj = PoolManager.Instance.Pop(monsterConfig.monsterPrefab);
        Monster monster = monsterObj.GetComponent<Monster>();

        float x = UnityEngine.Random.Range(player.MinBound.x, player.MaxBound.x);
        float y = UnityEngine.Random.Range(player.MinBound.y, player.MaxBound.y);
        Vector2 monsterPos = new Vector2(x, y);

        monster.Init(player, monsterPos, GameRoot.Instance.MonsterMgr, monsterConfig);

        GameRoot.Instance.MonsterMgr.Register(monster);
    }
    
}
