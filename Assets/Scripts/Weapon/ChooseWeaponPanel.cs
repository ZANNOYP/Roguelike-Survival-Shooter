using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 选择武器面板
/// </summary>
public class ChooseWeaponPanel : MonoBehaviour
{
    // 武器选择按钮预设体
    [SerializeField]
    private WeaponSelectedButton buttonPrefab;
    // 按钮父对象
    [SerializeField]
    private Transform buttonRoot;
    // 武器选择管理器
    [SerializeField]
    private WeaponSelectedManager weaponSelectedManager;

    // 显示面板
    public void Show(List<WeaponData> weapons)
    {
        // 激活面板
        gameObject.SetActive(true);
        // 武器选项索引
        int weaponIndex = 0;

        for (int i = 0; i < weapons.Count; i++)
        {
            // 武器选择数据
            var data = weapons[i];
            // 武器选择按钮
            WeaponSelectedButton btn;
            // 武器选项索引小于 已有按钮时 激活按钮
            if (weaponIndex < buttonRoot.childCount)
            {
                btn = buttonRoot.GetChild(weaponIndex).GetComponent<WeaponSelectedButton>();
                btn.gameObject.SetActive(true);
            }
            // 新创建一个按钮
            else
            {
                btn = Instantiate(buttonPrefab, buttonRoot);
            }
            // 初始化按钮
            btn.Init(data,weaponSelectedManager.OnWeaponSelected);
            // 武器选项索引+1
            weaponIndex++;
        }
        // 将多余的按钮隐藏
        for (int i = weaponIndex; i < buttonRoot.childCount; i++)
        {
            buttonRoot.GetChild(i).gameObject.SetActive(false);
        }
    }

    // 隐藏面板
    public void Hide()
    {
        // 失活面板
        gameObject.SetActive(false);
    }
}
