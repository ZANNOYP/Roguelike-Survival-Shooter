using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 选择武器按钮
/// </summary>
public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 武器名字
    public TextMeshProUGUI textWeaponName;
    // 武器描述
    public TextMeshProUGUI textDescription;
    // 选择按钮
    public Button selectButton;
    // 图片描述
    public Image imgDescription;
    // 武器图片
    public Image imgWeapon;

    private void Awake()
    {
        imgDescription.gameObject.SetActive(false);
    }

    public void Init(WeaponConfig config)
    {
        textWeaponName.text = config.weaponName;
        string str = config.description;
        str = str.Replace(",", "\n");
        textDescription.text = str;
        selectButton.onClick.AddListener(() => SelectManager.instance.SelectWeapon(config));

        imgWeapon.sprite = config.weaponSprite;
        float width = config.weaponSprite.rect.width;
        float height = config.weaponSprite.rect.height;
        float maxSize = 90f;
        float scale = maxSize / Mathf.Max(width, height);
        Vector2 size = new Vector2(width * scale, height * scale);
        imgWeapon.rectTransform.sizeDelta = size;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imgDescription.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imgDescription.gameObject.SetActive(false);
    }
}
