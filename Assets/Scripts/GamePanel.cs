using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 游戏中面板
/// </summary>
public class GamePanel : MonoBehaviour
{
    public static GamePanel instance;

    public bool isShow;
    public float fadeInOutSpeed = 5f;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        Hide(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FadeInOut();
    }

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
                canvasGroup.alpha = 0;
            }
        }
    }

    public void Show(bool isFade = true)
    {
        isShow = true;

        if (!isFade)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Hide(bool isFade = true)
    {
        isShow = false;

        if (!isFade)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
