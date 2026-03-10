using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏流程事件
/// </summary>
public static class GameFlowEvents
{
    public static Action OnGameStart;// 游戏开始事件
    public static Action OnWeaponChoose;// 选择武器事件
    public static Action OnGameOver;// 游戏结束事件
    public static Action OnGameReset;// 游戏重置事件
}
