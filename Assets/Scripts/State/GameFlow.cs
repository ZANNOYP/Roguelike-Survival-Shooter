using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏状态枚举
/// </summary>
public enum GameState
{
    Ready,// 准备状态
    ChooseWeapon,// 选择武器状态
    Playing,// 游戏中状态
    GameOver,// 游戏结束状态
}

/// <summary>
/// 游戏管理器
/// </summary>
public class GameFlow : MonoBehaviour ,IGameFlow
{
    private static GameFlow instance;
    public static GameFlow Instance => instance;
    // 当前游戏状态
    public GameState State { get; private set; }
    private IGameState currentState;
    // 游戏状态字典
    private Dictionary<GameState, IGameState> states;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(this.gameObject);

        //Application.targetFrameRate = 60;
    }

    private void Start()
    {
        // 初始化游戏状态字典
        states = new Dictionary<GameState, IGameState>()
        {
            { GameState.Ready,new ReadyState()},
            { GameState.ChooseWeapon,new ChooseWeaponState()},
            { GameState.Playing,new PlayingState()},
            { GameState.GameOver,new GameOverState()}
        };

        // 初始设置当前状态为准备状态
        currentState = states[GameState.Ready];
        State = GameState.Ready;
        currentState.Enter();
        // 时间暂停
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 改变游戏状态
    /// </summary>
    /// <param name="newState">新状态</param>
    private void ChangeState(GameState newState)
    {
        if (State == newState) return;
        currentState?.Exit();
        State = newState;
        currentState = states[newState];
        currentState?.Enter();
    }

    public void ChooseWeapon()
    {
        ChangeState(GameState.ChooseWeapon);
    }

    public void StartGame()
    {
        ChangeState(GameState.Playing);
    }

    public void EndGame()
    {
        ChangeState(GameState.GameOver);
    }

    public void ResetGame()
    {
        ChangeState(GameState.Ready);
    }
}
