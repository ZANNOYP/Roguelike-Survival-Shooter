using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 开始按钮 进入选择武器界面
/// </summary>
public class StartButton : MonoBehaviour
{
    // 按钮点击事件
    public void OnClick()
    {
        GameFlow.Instance.ChooseWeapon();
    }
}
