using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 强化面板
/// </summary>
public class UpgradePanel : BasePanel
{
    // 强化按钮父对象
    public Transform buttonRoot;
    // 强化按钮预设体
    public GameObject btnPrefab;

    public PlayerControl player;

    public override void OnShowComplete()
    {
        player.ResetPos();
    }

    /// <summary>
    /// 创建强化按钮
    /// </summary>
    /// <param name="datas"></param>
    public void CreateButtons(List<UpgradeData> datas)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            GameObject btnObj = GameObject.Instantiate(btnPrefab, buttonRoot);
            UpgradeButton upgradeButton = btnObj.GetComponent<UpgradeButton>();
            upgradeButton.InitButton(datas[i]);
        }
    }

    /// <summary>
    /// 清理强化按钮
    /// </summary>
    public void ClearButtons()
    {
        foreach (Transform child in buttonRoot)
        {
            Destroy(child.gameObject);
        }
    }
}
