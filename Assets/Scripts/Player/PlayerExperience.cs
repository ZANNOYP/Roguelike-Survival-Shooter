using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 玩家经验
/// </summary>
public class PlayerExperience : MonoBehaviour
{
    public static PlayerExperience Instance;
    
    public int defaultMaxExp = 5;
    public int addMaxExp = 5;

    public int currentExp;
    public int maxExp;
    public int currentLevel;

    public Image expFill;
    public TextMeshProUGUI textExp;
    public int strengthenCount;
    private void Awake()
    {
        Instance = this;
        ResetExp();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeExp(int value)
    {
        currentExp += value;
        if (currentExp >= maxExp)
        {
            // 升级
            LevelUp();
        }
        else
        {
            expFill.fillAmount = (float)currentExp / maxExp;
        }
    }

    public void RefreshUI()
    {
        expFill.fillAmount = (float)currentExp / maxExp;
        textExp.text = currentLevel.ToString();
    }

    /// <summary>
    /// 升级
    /// </summary>
    public void LevelUp()
    {
        strengthenCount++;
        currentExp -= maxExp;
        maxExp += addMaxExp;
        currentLevel++;
        // 更新UI
        RefreshUI();
    }

    /// <summary>
    /// 重置经验值
    /// </summary>
    public void ResetExp()
    {
        currentExp = 0;
        maxExp = defaultMaxExp;
        currentLevel = 1;
        RefreshUI();
    }

    /// <summary>
    /// 重置强化次数
    /// </summary>
    public void ResetStrengthenCount()
    {
        strengthenCount = 0;
    }
}
