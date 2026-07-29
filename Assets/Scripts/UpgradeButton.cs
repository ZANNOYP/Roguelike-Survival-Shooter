using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 强化按钮
/// </summary>
public class UpgradeButton : MonoBehaviour
{
    public Button btn;
    public TextMeshProUGUI textUpgradeName;
    public TextMeshProUGUI textDescription;

    /// <summary>
    /// 初始化按钮
    /// </summary>
    /// <param name="data"></param>
    public void InitButton(UpgradeData data)
    {
        btn.onClick.AddListener(() => UpgradeManager.instance.SelectUpgrade(data));
        textUpgradeName.text = data.upgradeName;
        textDescription.text = data.description;
    }
}
