using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏结束重置按钮
/// </summary>
public class GameOverButton : MonoBehaviour
{
    /// <summary>
    /// 按钮点击事件
    /// </summary>
    public void OnClick()
    {
        GameFlow.Instance.ResetGame();
    }
}
