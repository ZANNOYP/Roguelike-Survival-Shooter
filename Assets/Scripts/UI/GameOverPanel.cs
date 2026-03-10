using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 结束面板
/// </summary>
public class GameOverPanel : MonoBehaviour
{
    private void Awake()
    {
        // 初始隐藏自己
        Hide();
    }

    private void OnEnable()
    {
        // 游戏流程事件注册
        GameFlowEvents.OnGameReset += Hide;

        GameFlowEvents.OnGameOver += Show;
    }

    private void OnDisable()
    {
        // 游戏流程事件反注册
        GameFlowEvents.OnGameReset -= Hide;

        GameFlowEvents.OnGameOver -= Show;
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    public void Show()
    {
        // 依次激活子对象
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    public void Hide()
    {
        // 依次失活子对象
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
}
