using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PoolType
{
    NormalBullet,
    None,
    End,
}

public class ObjPool : Singletone<ObjPool>
{
    public PoolType type;

    public BaseAll[] originObjects;

    public Dictionary<PoolType, Queue<BaseAll>> pool = new Dictionary<PoolType, Queue<BaseAll>>();

    public BaseAll Get(PoolType type, Vector3 pos)
    {
        BaseAll go = null;

        if (!pool.ContainsKey(type)) pool.Add(type, new Queue<BaseAll>());

        Queue<BaseAll> queue = pool[type];

        BaseAll origin = originObjects[((int)type)];

        if (queue.Count > 0)
        {
            go = queue.Dequeue();
        }
        else
        {
            go = Instantiate(origin);
        }

        go.gameObject.SetActive(true);
        go.transform.position = pos;
        go.transform.localScale = origin.transform.localScale;
        go.transform.localRotation = origin.transform.localRotation;
        go.Get();

        return go;
    }

    public T Get<T>(PoolType type, Vector3 pos)
        where T : MonoBehaviour
    {
        return Get(type, pos).GetComponent<T>();
    }

    public void Return(PoolType type, BaseAll go)
    {
        go.Return();
        go.gameObject.SetActive(false);
        pool[type].Enqueue(go);
    }
}
