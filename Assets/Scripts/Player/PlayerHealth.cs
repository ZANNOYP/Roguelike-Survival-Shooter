using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 玩家血量管理
/// </summary>
public class PlayerHealth : Health
{
    // 血量文本
    public TextMeshProUGUI textHp;
    // 血条填充
    public Image hpFill;
    // 血条延迟条
    public Image delayFill;
    // 延迟条变化速度
    public float delaySpeed = 5f;
    // 玩家死亡事件
    private Action<bool> deadAction;
    // 玩家运行时数据
    private PlayerData playerData;

    protected override float MaxHp => playerData.maxHp;

    // Start is called before the first frame update
    protected override void Start()
    {
        playerData = DataManager.instance.playerRuntimeData;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        ChangeDelayFill();
    }

    /// <summary>
    /// 改变延迟血条
    /// </summary>
    public void ChangeDelayFill()
    {
        if (Mathf.Abs(delayFill.fillAmount - hpFill.fillAmount) < 0.01f)
        {
            delayFill.fillAmount = hpFill.fillAmount;
            return;
        }
        delayFill.fillAmount = Mathf.Lerp(delayFill.fillAmount, hpFill.fillAmount, Time.deltaTime * delaySpeed);
        delayFill.fillAmount = Mathf.Abs(delayFill.fillAmount - hpFill.fillAmount) < 0.01f ? hpFill.fillAmount : delayFill.fillAmount;
    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    protected override void RefreshUI()
    {
        textHp.text = nowHp + "/" + MaxHp;
        hpFill.fillAmount = nowHp / MaxHp;
    }

    /// <summary>
    /// 注册玩家死亡事件
    /// </summary>
    /// <param name="action"></param>
    public void RegisterDeadAction(Action<bool> action)
    {
        deadAction += action;
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    public override void OnDead()
    {
        deadAction?.Invoke(false);
    }
}
