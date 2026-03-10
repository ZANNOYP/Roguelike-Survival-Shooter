using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池管理器
/// </summary>
public class PoolManager
{
    private static PoolManager instance = new PoolManager();
    public static PoolManager Instance => instance;
    private PoolManager() { }

    private Dictionary<GameObject, Stack<GameObject>> dicts = new Dictionary<GameObject, Stack<GameObject>>();

    /// <summary>
    /// 取对象
    /// </summary>
    /// <param name="prefab">预设体</param>
    /// <returns></returns>
    public GameObject Pop(GameObject prefab)
    {
        if (!dicts.TryGetValue(prefab, out Stack<GameObject> stack))
        {
            stack = new Stack<GameObject>();
            dicts[prefab] = stack;
        }
        GameObject obj;
        if (stack.Count <= 0)  
        {
            obj = GameObject.Instantiate(prefab);
        }
        else
        {
            obj = stack.Pop();
        }
        obj.SetActive(true);

        return obj;
    }

    /// <summary>
    /// 放对象
    /// </summary>
    /// <param name="obj">放入的对象</param>
    /// <param name="prefab">预设体</param>
    public void Push(GameObject obj, GameObject prefab)
    {
        if (!dicts.TryGetValue(prefab, out Stack<GameObject> stack))
        {
            stack = new Stack<GameObject>();
            dicts[prefab] = stack;
        }

        obj.SetActive(false);

        stack.Push(obj);
    }
}
