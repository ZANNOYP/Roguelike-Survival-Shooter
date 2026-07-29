using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 强化管理器
/// </summary>
public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public UpgradePanel upgradePanel;
    public List<UpgradeData> upgradeDatas = new List<UpgradeData>();
    public bool IsComplete => isComplete;
    private bool isComplete;
    private PlayerData playerData;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        playerData = DataManager.instance.playerRuntimeData;
    }
    /// <summary>
    /// 开启强化界面
    /// </summary>
    public void Show()
    {
        isComplete = false;
        // 得到一个强化数据列表
        List<UpgradeData> newUpgradeDatas = new List<UpgradeData>();
        int count = Random.Range(3, upgradeDatas.Count + 1);
        List<int> indexs = new List<int>();
        int index;
        for (int i = 0; i < count; i++)
        {
            do
            {
                index = Random.Range(0, upgradeDatas.Count);
            }
            while (indexs.Contains(index));
            indexs.Add(index);
            newUpgradeDatas.Add(upgradeDatas[index]);
        }
        // 显示强化面板
        upgradePanel.Show(newUpgradeDatas);
    }
    /// <summary>
    /// 隐藏强化界面
    /// </summary>
    public void Hide()
    {
        upgradePanel.Hide();
    }
    /// <summary>
    /// 选择强化
    /// </summary>
    public void SelectUpgrade(UpgradeData data)
    {
        data.Apply();

        isComplete = true;
    }
}
