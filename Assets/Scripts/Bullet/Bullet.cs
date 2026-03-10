using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹基类
/// </summary>
public abstract class Bullet : MonoBehaviour
{
    // 子弹管理器
    protected BulletMgr bulletMgr;
    // 子弹伤害
    protected float damage;
    // 子弹是否死亡
    private bool isDead = false;
    // 子弹预设体用于对象池查找
    protected GameObject bulletPrefab;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="bulletMgr"></param>
    /// <param name="damage"></param>
    public void Init(BulletMgr bulletMgr, float damage)
    {
        this.bulletMgr = bulletMgr;
        this.damage = damage;
        isDead = false;
    }

    /// <summary>
    /// 移动
    /// </summary>
    protected abstract void Move();

    /// <summary>
    /// 死亡
    /// </summary>
    protected virtual void Die()
    {
        // 已经死亡 直接返回
        if (isDead) return;
        // 改变死亡状态
        isDead = true;
        // 子弹管理器移除子弹
        bulletMgr.Remove(this);
        // 销毁子弹
        PoolManager.Instance.Push(gameObject,bulletPrefab);
    }


    /// <summary>
    /// 强制死亡
    /// </summary>
    public virtual void ForceDestroy()
    {
        // 已经死亡 直接返回
        if (isDead) return;
        // 改变死亡状态
        isDead = true;
        // 子弹管理器移除子弹
        bulletMgr?.Remove(this);
        // 反注册事件
        UnRegisterEvents();
        // 销毁子弹
        PoolManager.Instance.Push(gameObject, bulletPrefab);
    }
    /// <summary>
    /// 反注册事件
    /// </summary>
    protected virtual void UnRegisterEvents()
    {

    }
}
