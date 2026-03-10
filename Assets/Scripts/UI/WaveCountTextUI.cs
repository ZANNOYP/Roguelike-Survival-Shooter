using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 波次数量文本类
/// </summary>
public class WaveCountTextUI : MonoBehaviour
{
    // 当前波次文本
    public TextMeshProUGUI txtCurrentWave;
    // 总波次文本
    public TextMeshProUGUI txtTotalWave;

    private void OnEnable()
    {
        WaveManager.nowWaveOnChanged += NowWaveOnChanged;
        WaveManager.totalWaveOnChanged += TotalWaveOnChanged;
    }

    private void OnDisable()
    {
        WaveManager.nowWaveOnChanged -= NowWaveOnChanged;
        WaveManager.totalWaveOnChanged -= TotalWaveOnChanged;
    }
    /// <summary>
    /// 当前波次数量改变方法
    /// </summary>
    /// <param name="currentWave"></param>
    private void NowWaveOnChanged(int currentWave)
    {
        txtCurrentWave.text = "第" + currentWave + "波";
    }
    /// <summary>
    /// 总波次数量设置方法
    /// </summary>
    /// <param name="totalWave"></param>
    private void TotalWaveOnChanged(int totalWave)
    {
        txtTotalWave.text = "共" + totalWave + "波";
    }
}
