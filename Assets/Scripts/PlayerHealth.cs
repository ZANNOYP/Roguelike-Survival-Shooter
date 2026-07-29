using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 玩家血量管理
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    // 最大血量
    public float maxHp = 10;
    // 血量文本
    public TextMeshProUGUI textHp;
    // 血条填充
    public Image hpFill;
    // 血条延迟条
    public Image delayFill;
    // 延迟条变化速度
    public float delaySpeed = 5f;
    // 当前血量
    private float nowHp;
    // 玩家死亡事件
    private Action deadAction;
    // 玩家运行时数据
    private PlayerData playerData;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerData = DataManager.instance.playerRuntimeData;
        Rebirth();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDelayFill();
    }

    /// <summary>
    /// 改变延迟血条
    /// </summary>
    public void ChangeDelayFill()
    {
        delayFill.fillAmount = Mathf.Lerp(delayFill.fillAmount, hpFill.fillAmount, Time.deltaTime * delaySpeed);
        delayFill.fillAmount = Mathf.Abs(delayFill.fillAmount - hpFill.fillAmount) < 0.01f ? hpFill.fillAmount : delayFill.fillAmount;
    }

    /// <summary>
    /// 血量改变
    /// </summary>
    /// <param name="value"></param>
    public void ChangeHp(float value)
    {
        nowHp = Mathf.Clamp(nowHp + value, 0, playerData.maxHp);
        RefreshUI();

        if (nowHp <= 0)
        {
            OnDead();
        }
    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    public void RefreshUI()
    {
        textHp.text = nowHp + "/" + playerData.maxHp;
        hpFill.fillAmount = nowHp / playerData.maxHp;
    }

    /// <summary>
    /// 注册玩家死亡事件
    /// </summary>
    /// <param name="action"></param>
    public void RegisterDeadAction(Action action)
    {
        deadAction += action;
    }

    /// <summary>
    /// 死亡事件调用
    /// </summary>
    public void OnDead()
    {
        deadAction?.Invoke();
    }

    /// <summary>
    /// 重生
    /// </summary>
    public void Rebirth()
    {
        ChangeHp(playerData.maxHp);
        delayFill.fillAmount = 1f;
    }
}
