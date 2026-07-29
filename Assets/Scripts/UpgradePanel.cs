using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 强化面板
/// </summary>
public class UpgradePanel : MonoBehaviour
{

    public bool isShow;
    public float fadeInOutSpeed = 5f;
    public Transform buttonRoot;
    public GameObject btnPrefab;
    public PlayerControl player;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
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

    public void Show(List<UpgradeData> datas, bool isFade = true)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            GameObject btnObj = GameObject.Instantiate(btnPrefab, buttonRoot);
            UpgradeButton upgradeButton = btnObj.GetComponent<UpgradeButton>();
            upgradeButton.InitButton(datas[i]);
        }
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
        foreach (Transform child in buttonRoot)
        {
            Destroy(child.gameObject);
        }
        isShow = false;
        if (!isFade)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
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
}
