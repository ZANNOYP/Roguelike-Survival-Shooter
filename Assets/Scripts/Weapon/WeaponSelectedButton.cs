using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 武器选择按钮
/// </summary>
public class WeaponSelectedButton : MonoBehaviour
{
    // 武器名字文本
    [SerializeField]
    private TextMeshProUGUI textName;
    // 武器描述文本
    [SerializeField]
    private TextMeshProUGUI textDescription;

    // 武器选项
    private WeaponData data;
    // 武器选项点击事件
    private Action<WeaponData> callback;
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="data">武器选项</param>
    /// <param name="callback">点击事件</param>
    public void Init(WeaponData data, Action<WeaponData> callback)
    {
        this.data = data;
        this.callback = callback;
        textName.text = data.Name;
        textDescription.text = data.Description.Replace("\\n", "\n");
    }

    /// <summary>
    /// 按钮点击方法
    /// </summary>
    public void OnClick()
    {
        callback?.Invoke(data);
    }
}
