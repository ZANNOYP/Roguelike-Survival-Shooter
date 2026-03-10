using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ÓÎÏ·½áÊø×´Ì¬
/// </summary>
public class GameOverState : IGameState
{
    public void Enter()
    {
        GameFlowEvents.OnGameOver?.Invoke();
        Time.timeScale = 0f;
    }

    public void Exit()
    {
        
    }
}
