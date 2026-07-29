using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 选择武器按钮
/// </summary>
public class SelectButton : MonoBehaviour
{
    public TextMeshProUGUI textWeaponName;
    public TextMeshProUGUI textDescription;
    public Button selectButton;

    public void Init(WeaponConfig config)
    {
        textWeaponName.text = config.weaponName;
        string str = config.description;
        str = str.Replace(",", "\n");
        textDescription.text = str;
        selectButton.onClick.AddListener(() => SelectManager.instance.SelectWeapon(config));
    }
}
