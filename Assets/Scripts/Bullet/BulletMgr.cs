using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹管理器
/// </summary>
public class BulletMgr
{
    // 子弹容器
    private List<Bullet> bullets = new List<Bullet>();

    public BulletMgr()
    {
        // 重置游戏清空子弹
        GameFlowEvents.OnGameReset += ClearAllBullets;
    }
    /// <summary>
    /// 添加子弹进容器
    /// </summary>
    /// <param name="bullet"></param>
    public void Register(Bullet bullet)
    {
        bullets.Add(bullet);
    }
    /// <summary>
    /// 移除子弹出容器
    /// </summary>
    /// <param name="bullet"></param>
    public void Remove(Bullet bullet)
    {
        bullets.Remove(bullet);
    }
    /// <summary>
    /// 清空子弹
    /// </summary>
    public void ClearAllBullets()
    {
        foreach (Bullet bullet in bullets.ToArray())
        {
            bullet.ForceDestroy();
        }
        bullets.Clear();
    }
}
