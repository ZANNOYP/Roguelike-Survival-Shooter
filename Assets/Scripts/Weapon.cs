using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    // 武器运行时数据
    public WeaponData data;
    // 玩家
    public Transform player;
    // 攻击协程
    public Coroutine attackCoroutine;

    // 玩家数据
    protected PlayerData playerData;

    private void Awake()
    {
        data = new WeaponData();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerData = DataManager.instance.playerRuntimeData;
    }
    /// <summary>
    /// 攻击
    /// </summary>
    public abstract void Atk();
}
