using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
/// 血量基类
/// </summary>
public abstract class Health : MonoBehaviour
{
    // 所属阵营
    public abstract Faction Faction { get; }
    // 当前血量
    [SerializeField]
    protected float nowHp;
    // 最大血量
    protected abstract float MaxHp { get; }
    // 击退委托
    private Action<RepelData> repelAction;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        RestoreFullHealth();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    /// <summary>
    /// 血量改变
    /// </summary>
    /// <param name="value"></param>
    /// <param name="repelData"></param>
    public virtual void ChangeHp(float value, RepelData repelData = null)
    {
        nowHp = Mathf.Clamp(nowHp + value, 0, MaxHp);
        RefreshUI();
        if (nowHp <= 0)
        {
            OnDead();
        }
        else if (repelData != null)
        {
            OnRepel(repelData);
        }
    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    protected virtual void RefreshUI()
    {
        
    }

    /// <summary>
    /// 死亡事件调用
    /// </summary>
    public virtual void OnDead()
    {
        
    }

    /// <summary>
    /// 注册击退事件
    /// </summary>
    /// <param name="action"></param>
    public void RegisterRepelAction(Action<RepelData> action)
    {
        repelAction += action;
    }

    /// <summary>
    /// 击退事件调用
    /// </summary>
    public void OnRepel(RepelData repelData)
    {
        repelAction?.Invoke(repelData);
    }

    /// <summary>
    /// 回满血
    /// </summary>
    public virtual void RestoreFullHealth()
    {
        ChangeHp(MaxHp);
    }
}
