using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : BaseAll
{
    public TrailRenderer tr;
    public Gradient enemyColor;
    public Gradient playerColor;

    public Vector3 dir;

    public int throughCount;

    public override void Start()
    {
        base.Start();
    }

    public virtual void FixedUpdate()
    {
        if (Vector3.Distance(K.player.transform.position, transform.position) >= 200)
        {
            ObjPool.Instance.Return(poolType, this);
        }
    }

    public override void Get()
    {
        base.Get();
        if (!tr) tr = GetComponent<TrailRenderer>();
        tr.Clear();
    }

    public IEnumerator EMove()
    {
        while (true)
        {
            yield return null;
            transform.Translate(dir * moveSpeed * K.GameDT);
        }
    }
}
