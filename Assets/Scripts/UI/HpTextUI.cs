using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 血条文本类
/// </summary>
public class HpTextUI : MonoBehaviour
{
    // 血条文本
    public TextMeshProUGUI textHP;

    private void OnEnable()
    {
        // 血条文本改变事件注册
        PlayerHpSystem.OnHpChanged += OnChangeHp;
    }

    private void OnDisable()
    {
        // 血条文本改变事件反注册
        PlayerHpSystem.OnHpChanged -= OnChangeHp;
    }

    /// <summary>
    /// 血条文本改变事件
    /// </summary>
    /// <param name="hp">当前血量</param>
    /// <param name="maxHp">最大血量</param>
    private void OnChangeHp(int hp, int maxHp)
    {
        // 更新血条文本
        textHP.text = hp.ToString() + "/" + maxHp.ToString();
    }
}
