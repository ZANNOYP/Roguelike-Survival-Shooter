using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 返回按钮
/// </summary>
public class ReturnButton : MonoBehaviour
{
    public void ReturnMenu()
    {
        MusicManager.instance.PlayEff(Eff_Type.Button);
        GameFlowManager.instance.GameReady();
    }
}
