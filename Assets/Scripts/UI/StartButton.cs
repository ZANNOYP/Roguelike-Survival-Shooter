using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 开始按钮
/// </summary>
public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        GameFlowManager.instance.WeaponSelect();
    }
}
