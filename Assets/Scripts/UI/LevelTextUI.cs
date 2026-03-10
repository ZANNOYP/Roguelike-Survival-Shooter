using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 等级文本类
/// </summary>
public class LevelTextUI : MonoBehaviour
{
    // 等级文本
    public TextMeshProUGUI textLevel;

    private void OnEnable()
    {
        // 升级事件注册
        PlayerExpSystem.OnLevelChanged += OnLevelUp;
    }

    private void OnDisable()
    {
        // 升级事件反注册
        PlayerExpSystem.OnLevelChanged -= OnLevelUp;
    }
    /// <summary>
    /// 升级事件
    /// </summary>
    /// <param name="level">当前等级</param>
    private void OnLevelUp(int level)
    {
        // 更新等级文本
        textLevel.text = "Lv" + level.ToString();
    }
}
