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

    public void ChangeBullet()
    {
        if (isEnemy)
        {
            if (dir != Vector3.zero) dir = -dir;
            else dir = Vector3.forward;

            tr.colorGradient = playerColor;
            isEnemy = false;
        }
    }

    public virtual IEnumerator EMove()
    {
        while (true)
        {
            yield return null;
            transform.Translate(dir * moveSpeed * K.GameDT);
        }
    }
}
