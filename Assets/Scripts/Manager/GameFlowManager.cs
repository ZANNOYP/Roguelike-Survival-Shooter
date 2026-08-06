using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏状态
/// </summary>
public enum GameState
{
    Menu,
    SelectWeapon,
    Playing,
    Over,
}
/// <summary>
/// 游戏流程管理器
/// </summary>
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager instance;

    // 玩家
    public PlayerControl player;
    // 是否胜利
    private bool isVic;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SelectManager.instance.OnWeaponSelected += GameStart;
    }

    /// <summary>
    /// 准备游戏
    /// </summary>
    public void GameReady()
    {
        ChangeState(GameState.Menu);
    }

    /// <summary>
    /// 选择武器
    /// </summary>
    public void WeaponSelect()
    {
        ChangeState(GameState.SelectWeapon);
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    public void GameStart()
    {
        ChangeState(GameState.Playing);
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver(bool isVic)
    {
        this.isVic = isVic;
        ChangeState(GameState.Over);
    }

    /// <summary>
    /// 改变游戏状态
    /// </summary>
    /// <param name="state"></param>
    private void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                EnterReady();
                break;
            case GameState.SelectWeapon:
                EnterSelectWeapon();
                break;
            case GameState.Playing:
                EnterPlaying();
                break;
            case GameState.Over:
                EnterOver();
                break;
        }
    }

    private void EnterReady()
    {
        UIManager.instance.HidePanel<EndPanel>();
        UIManager.instance.ShowPanel<StartPanel>();
        player.Rebirth();
    }

    private void EnterSelectWeapon()
    {
        UIManager.instance.HidePanel<StartPanel>();
        List<WeaponConfig> configs = SelectManager.instance.GetWeaponConfigs();
        UIManager.instance.ShowPanel<SelectPanel>();
        UIManager.instance.GetPanel<SelectPanel>().CreateButtons(configs);
    }

    private void EnterPlaying()
    {
        UIManager.instance.HidePanel<SelectPanel>();
        UIManager.instance.HidePanel<EmptyPanel>(false);
        UIManager.instance.GetPanel<SelectPanel>().ClearButtons();
        UIManager.instance.ShowPanel<GamePanel>();
        player.StartControl();
        WaveManager.instance.StartWaveLoop();
    }

    private void EnterOver()
    {
        UIManager.instance.HidePanel<GamePanel>();
        UIManager.instance.ShowPanel<EndPanel>();
        UIManager.instance.ShowPanel<EmptyPanel>(false);
        UIManager.instance.GetPanel<EndPanel>().UpdataTitle(isVic);
        BulletManager.instance.KillAllBullets();
        WeaponSystem.instance.UnEquipAll();
        PlayerExperience.Instance.ResetStrengthenCount();
        if (!isVic)
        {
            WaveManager.instance.StopWaveLoop();
            player.Dead();
        }
    }
}
