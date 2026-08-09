using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
/// <summary>
/// 音效类型
/// </summary>
public enum Eff_Type
{
    /// <summary>
    /// 按钮
    /// </summary>
    Button,
    /// <summary>
    /// 球棒打击
    /// </summary>
    Hit,
    /// <summary>
    /// 枪声
    /// </summary>
    Gun,
}

/// <summary>
/// 音效类型
/// </summary>
public enum Bgm_Type
{
    /// <summary>
    /// 主菜单
    /// </summary>
    Menu,
    /// <summary>
    /// 游戏内
    /// </summary>
    Battle,
    /// <summary>
    /// 胜利
    /// </summary>
    Victory,
    /// <summary>
    /// 失败
    /// </summary>
    Defeat,
}

/// <summary>
/// 音乐管理器
/// </summary>
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    // 音效片段
    public List<AudioClip> clips = new List<AudioClip>();
    // 音乐片段
    public List<AudioClip> bgmclips = new List<AudioClip>();
    // 最大音效数量
    public int maxSourceCount = 3;
    // 所有音效对象
    private List<AudioSource> sources = new List<AudioSource>();
    // 当前音效索引
    private int currentSourceIndex = 0;
    // 背景音乐
    private AudioSource bgmSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayBgm(Bgm_Type.Menu, 0.8f);
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="type"></param>
    /// <param name="volume"></param>
    public void PlayEff(Eff_Type type, float volume = 1f)
    {
        AudioSource source;
        if (sources.Count >= maxSourceCount)
        {
            source = sources[currentSourceIndex];
            currentSourceIndex++;
            if (currentSourceIndex >= maxSourceCount) 
            {
                currentSourceIndex = 0;
            }
        }
        else
        {
            GameObject obj = new GameObject();
            obj.name = "Eff";
            source = obj.AddComponent<AudioSource>();
            sources.Add(source);
        }
        source.Stop();


        source.clip = clips[(int)type];
        source.volume = volume;
        source.Play();
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="type"></param>
    /// <param name="volume"></param>
    public void PlayBgm(Bgm_Type type, float volume = 1f)
    {
        if (bgmSource == null)
        {
            GameObject obj = new GameObject();
            obj.name = "Bgm";
            bgmSource = obj.AddComponent<AudioSource>();
        }
        bgmSource.Stop();


        bgmSource.clip = bgmclips[(int)type];
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
