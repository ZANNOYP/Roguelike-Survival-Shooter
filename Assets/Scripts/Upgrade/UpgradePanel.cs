using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 更新面板
/// </summary>
public class UpgradePanel : MonoBehaviour
{
    // 按钮预设体
    [SerializeField]
    private UpgradeButton buttonPrefab;
    // 按钮父对象
    [SerializeField]
    private Transform buttonRoot;
    // 更新管理器
    [SerializeField]
    private UpgradeMgr upgradeMgr;

    private void Awake()
    {
        // 一开始隐藏自己
        Hide();
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <param name="upgrades">升级选项列表</param>
    public void Show(List<UpgradeData> upgrades)
    {
        // 对象激活
        gameObject.SetActive(true);

        int btnIndex = 0;

        int totalButtons = Mathf.Max(upgrades.Count, buttonRoot.childCount);

        for (int i = 0; i < upgrades.Count; i++)
        {
            var data = upgrades[i];

            if (data.GetType() == typeof(BulletCountUpgrade) && upgradeMgr.weaponManager.CurrentWeapon.MaxBulletCount()) 
            {
                continue;
            }
            UpgradeButton btn;
            if (btnIndex < buttonRoot.childCount)
            {
                // 复用已有按钮
                btn = buttonRoot.GetChild(btnIndex).GetComponent<UpgradeButton>();
                btn.gameObject.SetActive(true);
            }
            else
            {
                // 创建新按钮
                btn = Instantiate(buttonPrefab, buttonRoot);
            }
            btn.Init(data, upgradeMgr.OnUpgradeSelected);
            btnIndex++;
        }

        for (int i = btnIndex; i < buttonRoot.childCount; i++)
        {
            // 超出升级选项数，隐藏多余按钮
            buttonRoot.GetChild(i).gameObject.SetActive(false);
        }

        //int i = 0;
        //// 复用已有按钮
        //for (; i < upgrades.Count && i < buttonRoot.childCount; i++)
        //{
        //    var btn = buttonRoot.GetChild(i).GetComponent<UpgradeButton>();
        //    btn.gameObject.SetActive(true);
        //    btn.Init(upgrades[i], upgradeMgr.OnUpgradeSelected);
        //}

        //// 如果升级选项多，实例化额外按钮
        //for (; i < upgrades.Count; i++)
        //{
        //    var btn = Instantiate(buttonPrefab, buttonRoot);
        //    btn.Init(upgrades[i], upgradeMgr.OnUpgradeSelected);
        //}

        //// 如果多余按钮，隐藏它们
        //for (; i < buttonRoot.childCount; i++)
        //{
        //    buttonRoot.GetChild(i).gameObject.SetActive(false);
        //}

        //// 销毁所有按钮
        //foreach(Transform child in buttonRoot)
        //{
        //    Destroy(child.gameObject);
        //}
        //// 根据升级选项列表生成升级按钮
        //foreach (var data in upgrades)
        //{
        //    //float rad = Random.Range(0f, 1f);
        //    // 不是增加子弹数量升级 直接创建按钮
        //    if (data.GetType() != typeof(BulletCountUpgrade))
        //    {
        //        var btn = Instantiate(buttonPrefab, buttonRoot);
        //        btn.Init(data, upgradeMgr.OnUpgradeSelected);
        //    }
        //    // 如果是增加子弹数量的升级 先判断是否超过设定子弹数量 没超过就生成升级按钮
        //    else if (/*rad <= 0.3f && */!upgradeMgr.weaponManager.CurrentWeapon.MaxBulletCount()) 
        //    {
        //        var btn = Instantiate(buttonPrefab, buttonRoot);
        //        btn.Init(data, upgradeMgr.OnUpgradeSelected);
        //    }
        //}    
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    public void Hide()
    {
        // 对象失活
        gameObject.SetActive(false);
    }
}
