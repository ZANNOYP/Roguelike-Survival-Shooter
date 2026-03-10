using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ñ¡ÔñÎäÆ÷×´Ì¬
/// </summary>
public class ChooseWeaponState : IGameState
{
    public void Enter()
    {
        GameFlowEvents.OnWeaponChoose?.Invoke();
    }

    public void Exit()
    {
        
    }
}
