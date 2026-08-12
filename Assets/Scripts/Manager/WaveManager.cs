using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 波次管理器
/// </summary>
public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    // 当前波次
    public int currentWave = 1;
    // 波次数据
    public List<WaveData> waves = new List<WaveData>();
    // 波次文本
    public TextMeshProUGUI textWave;
    // 剩余敌人数量文本
    public TextMeshProUGUI textEnemyCount;
    // 玩家控制
    public PlayerControl player;
    // 波次循环协程
    private Coroutine waveLoopCoroutine;
    // 当前敌人剩余数量
    private int currentEnemyCount;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 开启波次循环
    /// </summary>
    public void StartWaveLoop()
    {
        waveLoopCoroutine = StartCoroutine(WaveLoop());
    }

    /// <summary>
    /// 波次循环协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveLoop()
    {
        while (currentWave <= waves.Count) 
        {
            RefreshUI();
            // 生成敌人
            yield return StartWave();
            // 等待敌人全部死亡
            yield return WaitForClear();

            player.Pause();
            BulletManager.instance.KillAllBullets();
            // 最后一波不进行强化 直接胜利
            if (currentWave == waves.Count)
            {
                // 获胜
                GameFlowManager.instance.GameOver(true);
                break;
            }
            // 等待强化
            yield return WaitForStrengthen();
            // 波次数+1
            currentWave++;

            player.StopPause();
        }

        currentWave = 1;
    }

    /// <summary>
    /// 敌人生成协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartWave()
    {
        WaveData data = waves[currentWave - 1];
        for (int i = 0; i < data.enemyCount; i++) 
        {
            yield return new WaitForSeconds(data.generateInterval);
            EnemyType type = GetEnemyType();
            EnemyManager.Instance.RealGenerate(type);
        }
    }

    /// <summary>
    /// 获取生成敌人的类型
    /// </summary>
    /// <returns></returns>
    private EnemyType GetEnemyType()
    {
        List<EnemyType> enemyTypes = EnemyManager.Instance.enemyTypes;
        int random = Random.Range(0, enemyTypes.Count);
        return enemyTypes[random];
    }

    /// <summary>
    /// 停止波次循环协程
    /// </summary>
    public void StopWaveLoop()
    {
        if (waveLoopCoroutine != null)
        {
            StopCoroutine(waveLoopCoroutine);
            waveLoopCoroutine = null;
        }
        currentWave = 1;
        EnemyManager.Instance.DieAll();
    }

    /// <summary>
    /// 等待敌人全部死亡协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForClear()
    {
        while (EnemyManager.Instance.GetAliveEnemyCount() > 0) 
        {
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// 等待强化
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForStrengthen()
    {
        int strengthenCount = PlayerExperience.Instance.strengthenCount;
        UIManager.instance.ShowPanel<EmptyPanel>(false);
        for (int i = 0; i < strengthenCount; i++)
        {
            // 打开强化界面 初始化
            List<UpgradeData> datas = UpgradeManager.instance.GetDatas();
            UIManager.instance.ShowPanel<UpgradePanel>();
            UIManager.instance.GetPanel<UpgradePanel>().CreateButtons(datas);
            // 等待强化一次结束
            yield return new WaitUntil(() => UpgradeManager.instance.IsComplete);
            // 隐藏强化面板
            UIManager.instance.HidePanel<UpgradePanel>();
            UIManager.instance.GetPanel<UpgradePanel>().ClearButtons();
        }
        UIManager.instance.HidePanel<EmptyPanel>(false);

        PlayerExperience.Instance.ResetStrengthenCount();
    }

    /// <summary>
    /// 波次开始刷新UI
    /// </summary>
    public void RefreshUI()
    {
        currentEnemyCount = waves[currentWave - 1].enemyCount;

        textWave.text = currentWave.ToString();
        textEnemyCount.text = currentEnemyCount.ToString();
    }

    /// <summary>
    /// 减少剩余敌人数量
    /// </summary>
    public void DeEnemyCount()
    {
        currentEnemyCount--;
        textEnemyCount.text = currentEnemyCount.ToString();
    }
}
