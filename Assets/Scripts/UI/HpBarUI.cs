using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 血条
/// </summary>
public class HpBarUI : MonoBehaviour
{
    // 血量滑动条
    public Slider sliderHp;

    private void OnEnable()
    {
        // 改变血量事件注册
        PlayerHpSystem.OnHpChanged += OnChangeHp;
    }

    private void OnDisable()
    {
        // 改变血量事件反注册
        PlayerHpSystem.OnHpChanged -= OnChangeHp;
    }
    /// <summary>
    /// 改变血量事件
    /// </summary>
    /// <param name="hp">当前血量</param>
    /// <param name="maxHp">最大血量</param>
    private void OnChangeHp(int hp, int maxHp)
    {
        // 更新血量滑动条值
        sliderHp.value = (float)hp / maxHp;
    }
}
