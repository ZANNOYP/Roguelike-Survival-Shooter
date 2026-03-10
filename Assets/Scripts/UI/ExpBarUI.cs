using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 经验条
/// </summary>
public class ExpBarUI : MonoBehaviour
{
    // 经验滑动条
    public Slider sliderExp;

    private void OnEnable()
    {
        // 经验改变事件注册
        PlayerExpSystem.OnExpChanged += OnChangeExp;
    }

    private void OnDisable()
    {
        // 经验改变事件反注册
        PlayerExpSystem.OnExpChanged -= OnChangeExp;
    }
    /// <summary>
    /// 经验改变事件
    /// </summary>
    /// <param name="cur">当前经验</param>
    /// <param name="max">升级所需经验</param>
    private void OnChangeExp(int cur,int max)
    {
        // 改变经验滑动条的值
        sliderExp.value = (float)cur / max;
    }
}
