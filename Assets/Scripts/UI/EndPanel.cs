using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 结束面板
/// </summary>
public class EndPanel : BasePanel
{
    // 结束标题
    public TextMeshProUGUI textTitle;

    public PlayerControl player;

    public void UpdataTitle(bool isVic)
    {
        if (isVic)
            textTitle.text = "胜\t利";
        else
            textTitle.text = "失\t败";
    }

    public override void OnShowComplete()
    {
        player.ResetPos();
    }
}
