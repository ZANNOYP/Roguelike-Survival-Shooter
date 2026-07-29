using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 开始面板
/// </summary>
public class StartPanel : MonoBehaviour
{

    public bool isShow;
    public float fadeInOutSpeed = 5f;
    public PlayerControl player;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Show(false);
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
                player.ResetPos();
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
            player.ResetPos();
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
