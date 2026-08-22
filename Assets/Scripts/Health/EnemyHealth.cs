using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人血量
/// </summary>
public class EnemyHealth : Health
{
    // 所属阵营
    public override Faction Faction => Faction.Enemy;
    // 最大血量
    private float maxHp;
    // 敌人死亡委托
    private Action deadAction;

    protected override float MaxHp => maxHp;

    /// <summary>
    /// 注册敌人死亡事件
    /// </summary>
    /// <param name="action"></param>
    public void RegisterDeadAction(Action action)
    {
        deadAction += action;
    }

    /// <summary>
    /// 死亡事件调用
    /// </summary>
    public override void OnDead()
    {
        deadAction?.Invoke();
    }

    /// <summary>
    /// 设置最大血量
    /// </summary>
    /// <param name="value"></param>
    public void SetMaxHp(float value)
    {
        maxHp = value;
    }
}
