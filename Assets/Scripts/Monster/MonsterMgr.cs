using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMgr
{
    private List<Monster> monsters = new List<Monster>();
    private Player player;
    private float generateInterval;
    private float defaultGenerateInterval = 1f;
    private int maxHp;
    private int defaultMaxHp = 6;
    private float nowTime;
    private float defaultNowTime = 0f;
    //private MonoBehaviour mono;
    //private Coroutine generateCoroutine;
    //private int maxMonsterCount = 10;

    public MonsterMgr(Player player, MonoBehaviour mono)
    {
        this.player = player;
        //this.mono = mono;
        //Init();

        //GameFlowEvents.OnGameReset += Init;
        //GameFlowEvents.OnGameReset += StopGenerate;

        //GameFlowEvents.OnGameStart += StartGenerate;
    }

    //public void StartGenerate()
    //{
    //    StopGenerate();
    //    generateCoroutine = mono.StartCoroutine(GenerateCoroutine());
    //}

    //public void StopGenerate()
    //{
    //    if (generateCoroutine != null)
    //    {
    //        mono.StopCoroutine(generateCoroutine);
    //        generateCoroutine = null;
    //    }
    //}

    //private IEnumerator GenerateCoroutine()
    //{
    //    while (true)
    //    {
    //        if (maxMonsterCount > 0 && monsters.Count >= maxMonsterCount) 
    //        {
    //            yield return null;
    //            continue;
    //        }
    //        GameObject monsterObj = GameObject.Instantiate(Resources.Load<GameObject>("Monster"));
    //        Monster monster = monsterObj.GetComponent<Monster>();
    //        float x = Random.Range(-19.25f, 19.25f);
    //        float y = Random.Range(-19.25f, 19.25f);
    //        Vector2 pos = new Vector2(x, y);
    //        monster.Init(player, pos, this, 1 - generateInterval + 3, maxHp);
    //        monsters.Add(monster);
    //        yield return new WaitForSeconds(generateInterval);
    //        nowTime += generateInterval;
    //        if (nowTime >= 30f)
    //        {
    //            nowTime = 0;
    //            ChangeGenerateInterval(generateInterval * 0.8f);
    //            ChangeMaxHp(3);
    //        }
    //    }
    //}

    public bool GetNearestMonster(out Vector2 monsterPos, out float minDist)
    {
        monsterPos = Vector2.zero;
        minDist = float.MaxValue;
        bool hasMonster = false;
        foreach (Monster monster in monsters)
        {
            if (monster == null) continue;
            float dist = (monster.transform.position - player.transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                monsterPos = monster.transform.position;
                hasMonster = true;
            }
        }
        return hasMonster;
    }

    public void Register(Monster monster)
    {
        monsters.Add(monster);
    }

    public void Remove(Monster monster)
    {
        monsters.Remove(monster);
    }

    public void ClearAllMonsters()
    {
        foreach (Monster monster in monsters.ToArray())
        {
            GameObject.Destroy(monster.gameObject);
        }
        monsters.Clear();
    }

    private void ChangeGenerateInterval(float generateInterval)
    {
        this.generateInterval = Mathf.Max(generateInterval, 0.3f);
    }

    private void ChangeMaxHp(int addHp)
    {
        maxHp += addHp;
    }

    public void Init()
    {
        ClearAllMonsters();
        generateInterval = defaultGenerateInterval;
        maxHp = defaultMaxHp;
        nowTime = defaultNowTime;
    }
}
