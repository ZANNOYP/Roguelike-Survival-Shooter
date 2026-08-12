using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectPool<T> where T : Component
{
    // 当前对象总数量
    private int totalCount;
    // 最大容量
    private int maxSize;
    // 创建方法
    private Func<T> createFunc;
    // 池子
    private Queue<T> pool = new Queue<T>();

    public ObjectPool(Func<T> createFunc, int initialSize, int maxSize)
    {
        this.createFunc = createFunc;
        this.maxSize = maxSize;

        for (int i = 0; i < initialSize; i++) 
        {
            T obj = Create();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        if (totalCount < maxSize) 
        {
            T obj = Create();
            obj.gameObject.SetActive(true);
            return obj;
        }
        
        return null;
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="obj"></param>
    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <returns></returns>
    private T Create()
    {
        totalCount++;
        return createFunc();
    }
}
