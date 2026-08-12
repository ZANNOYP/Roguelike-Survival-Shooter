using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人数据
/// </summary>
public class EnemyData
{
    public EnemyConfig enemyConfig;

    public void Init(EnemyConfig enemyConfig)
    {
        this.enemyConfig = enemyConfig;
    }
}
