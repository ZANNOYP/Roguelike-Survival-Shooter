using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人血量
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    // 当前血量
    public float nowHp;
    // 最大血量
    public float maxHp = 5;
    // 敌人控制
    private EnemyControl enemy;
    // 敌人死亡委托
    private Action deadAction;

    private void Awake()
    {
        enemy = GetComponent<EnemyControl>();
        Rebirth();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 改变血量
    /// </summary>
    /// <param name="value"></param>
    public void ChangeHp(float value)
    {
        nowHp = Mathf.Clamp(nowHp + value, 0, maxHp);

        if (nowHp <= 0)
        {
            OnDead();
        }
    }

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
    public void OnDead()
    {
        deadAction?.Invoke();
    }

    /// <summary>
    /// 重生
    /// </summary>
    public void Rebirth()
    {
        ChangeHp(maxHp);
    }
}
