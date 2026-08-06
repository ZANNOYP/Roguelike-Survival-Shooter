using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 数据管理器
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerConfig playerConfig;
    public PlayerData playerRuntimeData = new PlayerData();


    private void Awake()
    {
        instance = this;
        ResetData();
    }

    public void ResetData()
    {
        playerRuntimeData.Init(playerConfig);
    }
}
