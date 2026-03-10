using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 开始面板
/// </summary>
public class BeginPanel : MonoBehaviour
{
    private void OnEnable()
    {
        // 游戏流程事件注册
        GameFlowEvents.OnGameReset += Show;

        GameFlowEvents.OnWeaponChoose += Hide;
    }

    private void OnDisable()
    {
        // 游戏流程事件反注册
        GameFlowEvents.OnGameReset -= Show;

        GameFlowEvents.OnWeaponChoose -= Hide;
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    public void Show()
    {
        // 将子对象依次激活
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }
    /// <summary>
    /// 隐藏面板
    /// </summary>
    public void Hide()
    {
        // 将子对象依次失活
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
}
