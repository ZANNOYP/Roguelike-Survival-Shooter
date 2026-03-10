using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹发射器
/// </summary>
public abstract class BulletEmitter : MonoBehaviour
{
    // 子弹预设体
    public GameObject bulletPrefab;
    /// <summary>
    /// 开始发射
    /// </summary>
    public abstract void StartEmitter();
    /// <summary>
    /// 停止发射
    /// </summary>
    public abstract void StopEmitter();
    /// <summary>
    /// 初始化武器运行时数据
    /// </summary>
    /// <param name="runtime"></param>
    public abstract void SetEmitter(WeaponRuntimeData runtime);
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="bulletMgr"></param>
    /// <param name="monsterMgr"></param>
    /// <param name="player"></param>
    public abstract void Init(BulletMgr bulletMgr, MonsterMgr monsterMgr, Player player);

    private void Start()
    {
        // 开始时自动停止发射
        StopEmitter();
    }
}
