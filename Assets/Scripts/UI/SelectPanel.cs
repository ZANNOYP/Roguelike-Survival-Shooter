using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 选择武器面板
/// </summary>
public class SelectPanel : BasePanel
{
    // 按钮父对象
    public Transform buttonRoot;
    // 按钮预设体
    public GameObject prefab;

    /// <summary>
    /// 创建按钮
    /// </summary>
    /// <param name="configs"></param>
    public void CreateButtons(List<WeaponConfig> configs)
    {
        for (int i = 0; i < configs.Count; i++)
        {
            GameObject buttonObj = GameObject.Instantiate(prefab, buttonRoot);
            SelectButton selectButton = buttonObj.GetComponent<SelectButton>();
            selectButton.Init(configs[i]);
        }
    }

    /// <summary>
    /// 清理按钮
    /// </summary>
    public void ClearButtons()
    {
        foreach (Transform child in buttonRoot)
        {
            Destroy(child.gameObject);
        }
    }
}
