using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 基础的升级数据
/// </summary>
[CreateAssetMenu(menuName = "Upgrade/Basic")]
public class UpgradeData : ScriptableObject
{
    // 升级名字
    public string Name;
    // 升级描述
    public string Description;
    // 升级更新武器数据
    public virtual void Apply(Weapon weapon)
    {

    }
}
