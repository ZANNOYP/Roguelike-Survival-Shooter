using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ×¼±¸ÖÐ×´Ì¬
/// </summary>
public class ReadyState : IGameState
{
    public void Enter()
    {
        GameFlowEvents.OnGameReset?.Invoke();
    }

    public void Exit()
    {
        
    }
}
