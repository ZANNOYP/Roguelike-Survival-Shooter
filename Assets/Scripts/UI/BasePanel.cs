using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 面板基类
/// </summary>
public class BasePanel : MonoBehaviour
{
    public bool isShow;
    // 渐变速度
    public float fadeSpeed = 5f;
    private bool isFade;
    protected CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isFade) 
            Fade();
    }

    /// <summary>
    /// 渐显隐
    /// </summary>
    protected void Fade()
    {
        if (isShow && canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            if (canvasGroup.alpha >= 1f)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                OnShowComplete();
            }
        }

        if (!isShow && canvasGroup.alpha > 0)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                gameObject.SetActive(false);
                OnHideComplete();
            }
        }
    }
    /// <summary>
    /// 显示
    /// </summary>
    /// <param name="isFade"></param>
    public virtual void Show(bool isFade = true)
    {
        isShow = true;
        this.isFade = isFade;
        gameObject.SetActive(true);
        if (!isFade)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    /// <param name="isFade"></param>
    public virtual void Hide(bool isFade = true)
    {
        isShow = false;
        this.isFade = isFade;
        if (!isFade)
        {
            gameObject.SetActive(false);
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    /// <summary>
    /// 完全显示调用
    /// </summary>
    public virtual void OnShowComplete()
    {

    }

    /// <summary>
    /// 完全隐藏调用
    /// </summary>
    public virtual void OnHideComplete()
    {
        
    }
}
