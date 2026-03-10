using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 怪物数量文本类
/// </summary>
public class MonsterCountTextUI : MonoBehaviour
{
    // 怪物数量文本
    public TextMeshProUGUI txtMonsterInWave;

    private void OnEnable()
    {
        WaveManager.monsterCountOnChanged += MonsterCountOnChanged;
    }

    private void OnDisable()
    {
        WaveManager.monsterCountOnChanged -= MonsterCountOnChanged;
    }

    /// <summary>
    /// 怪物数量改变
    /// </summary>
    /// <param name="monsterCount">怪物数量</param>
    private void MonsterCountOnChanged(int monsterCount)
    {
        txtMonsterInWave.text = "还剩" + monsterCount + "只怪";
    }
}
