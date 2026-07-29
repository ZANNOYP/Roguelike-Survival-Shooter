using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 结束面板
/// </summary>
public class EndPanel : MonoBehaviour
{
    public static EndPanel instance;

    public bool isShow;
    public float fadeInOutSpeed = 5f;
    public TextMeshProUGUI textTitle;

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
        PlayerHealth.instance.RegisterDeadAction(DeadShow);
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

    public void Show(bool isVic = true, bool isFade = true)
    {
        if (isVic)
            textTitle.text = "胜\t利";
        else
            textTitle.text = "失\t败";

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

    public void DeadShow()
    {
        GamePanel.instance.Hide();
        Show(false);
    }
}
