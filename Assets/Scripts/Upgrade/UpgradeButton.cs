using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 武器升级按钮
/// </summary>
public class UpgradeButton : MonoBehaviour
{
    // 升级名字文本
    [SerializeField]
    private TextMeshProUGUI textName;
    // 升级描述文本
    [SerializeField]
    private TextMeshProUGUI textDescription;
    // 升级数据
    private UpgradeData data;
    // 按钮点击事件
    private Action<UpgradeData> onClick;
    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="data">升级数据</param>
    /// <param name="callback">按钮点击事件</param>
    public void Init(UpgradeData data, Action<UpgradeData> callback)
    {
        this.data = data;
        onClick = callback;
        textName.text = data.Name;
        textDescription.text = data.Description;
    }
    /// <summary>
    /// 调用按钮点击事件
    /// </summary>
    public void OnClick()
    {
        onClick?.Invoke(data);
    }
}
