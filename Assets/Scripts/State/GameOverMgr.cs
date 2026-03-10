using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏结束管理器
/// </summary>
public class GameOverMgr : MonoBehaviour
{
    private void OnEnable()
    {
        // 玩家死亡事件注册
        PlayerHpSystem.OnPlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        // 玩家死亡事件反注册
        PlayerHpSystem.OnPlayerDead -= OnPlayerDead;
    }
    /// <summary>
    /// 玩家死亡事件
    /// </summary>
    private void OnPlayerDead()
    {
        GameFlow.Instance.EndGame();
    }
}
