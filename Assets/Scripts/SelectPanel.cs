using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 选择武器面板
/// </summary>
public class SelectPanel : MonoBehaviour
{
    // 当前是否显示
    public bool isShow;
    // 渐显隐速度
    public float fadeInOutSpeed = 5f;
    // 按钮父对象
    public Transform buttonRoot;
    // 按钮预设体
    public GameObject prefab;
    // 面板透明度控制
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide(false);
    }
    // Update is called once per frame
    void Update()
    {
        FadeInOut();
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <param name="configs"></param>
    /// <param name="isFade"></param>
    public void Show(List<WeaponConfig> configs, bool isFade = true)
    {
        for (int i = 0; i < configs.Count; i++)
        {
            GameObject buttonObj = GameObject.Instantiate(prefab, buttonRoot);
            SelectButton selectButton = buttonObj.GetComponent<SelectButton>();
            selectButton.Init(configs[i]);
        }
        if (!isFade)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        isShow = true;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="isFade"></param>
    public void Hide(bool isFade = true)
    {
        foreach (Transform child in buttonRoot)
        {
            Destroy(child.gameObject);
        }
        if (!isFade)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        isShow = false;
    }

    /// <summary>
    /// 渐显隐
    /// </summary>
    public void FadeInOut()
    {
        if (isShow && canvasGroup.alpha < 1f) 
        {
            canvasGroup.alpha += Time.deltaTime * fadeInOutSpeed;
            if (canvasGroup.alpha >= 1f)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }
        if (!isShow && canvasGroup.alpha > 0) 
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha -= Time.deltaTime * fadeInOutSpeed;
            if (canvasGroup.alpha <= 0) 
            {
                canvasGroup.alpha = 0f;
            }
        }
    }
}
