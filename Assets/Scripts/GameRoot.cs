using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏根类
/// </summary>
public class GameRoot : MonoBehaviour
{
    private static GameRoot instance;
    public static GameRoot Instance => instance;
    // 玩家
    public Player player;
    // 怪物管理器
    private MonsterMgr monsterMgr;
    public MonsterMgr MonsterMgr => monsterMgr;
    // 子弹管理器
    private BulletMgr bulletMgr;
    public BulletMgr BulletMgr => bulletMgr;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初始化 怪物管理器、子弹管理器
        monsterMgr = new MonsterMgr(player, this);
        bulletMgr = new BulletMgr();

        //foreach (var emitter in FindObjectsOfType<BulletEmitter>())
        //{
        //    emitter.Init(bulletMgr, monsterMgr, player);
        //}

        //player.SetWeapon(weapon);

        //player.SetWeapon(new ProjectileWeapon(data, bulletMgr, monsterMgr, player));
        Application.targetFrameRate = 144;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {

    }
}
