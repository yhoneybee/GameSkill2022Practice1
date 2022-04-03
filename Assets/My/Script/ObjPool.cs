using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PoolType
{
    NormalBullet,
    BezierBullet,
    Bacteria,
    Germ,
    Virus,
    Cancer,
    Covid_19,
    Covid_19_Mutant,
    AttackAddon,
    ShieldAddon,
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
            if (go.isEnemy)
            {
                BaseEnemy be = go.GetComponent<BaseEnemy>();
                if (be) K.enemies.Add(be);
            }
            BaseBullet bb = go.GetComponent<BaseBullet>();
            if (bb) K.bullets.Add(bb);
        }

        go.transform.position = pos;
        go.transform.localScale = origin.transform.localScale;
        go.transform.localRotation = origin.transform.localRotation;
        go.gameObject.SetActive(true);
        go.Get();

        return go;
    }

    public T Get<T>(PoolType type, Vector3 pos)
        where T : MonoBehaviour
    {
        return Get(type, pos).GetComponent<T>();
    }

    public void Return(PoolType type, BaseAll ba)
    {
        ba.Return();
        ba.gameObject.SetActive(false);
        pool[type].Enqueue(ba);
    }

    public void WaitReturn(PoolType type, BaseAll ba, float waitTime)
        => StartCoroutine(EWaitReturn(type, ba, waitTime));

    public IEnumerator EWaitReturn(PoolType type, BaseAll ba, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Return(type, ba);
    }
}
