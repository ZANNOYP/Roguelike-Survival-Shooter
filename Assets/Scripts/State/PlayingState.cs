using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ÓÎÏ·ÖÐ×´Ì¬
/// </summary>
public class PlayingState : IGameState
{
    public void Enter()
    {
        Time.timeScale = 1.0f;
        GameFlowEvents.OnGameStart?.Invoke();
    }

    public void Exit()
    {
        
    }
}
