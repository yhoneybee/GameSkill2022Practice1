using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : BaseAll
{
    public TrailRenderer tr;
    public Gradient enemyColor;
    public Gradient playerColor;

    public Vector3 dir = Vector3.zero;

    public int throughCount;

    public override void Start()
    {
        base.Start();
        sphereCollider.radius = 2;
    }

    public virtual void FixedUpdate()
    {
        if (Vector3.Distance(K.player.transform.position, transform.position) >= 400)
        {
            ObjPool.Instance.Return(poolType, this);
        }
    }

    public override void Get()
    {
        base.Get();
        if (!tr) tr = GetComponent<TrailRenderer>();
        tr.Clear();
        StartCoroutine(EMove());
    }

    public virtual void ChangeBullet()
    {
        if (isEnemy)
        {
            tr.colorGradient = playerColor;
            isEnemy = false;
        }
    }

    public virtual IEnumerator EMove()
    {
        while (true)
        {
            yield return null;
            transform.Translate(K.GameDT * moveSpeed * dir);
        }
    }
}
