using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseObject : BaseAll
{
    public int Hp
    {
        get => hp;
        set
        {
            if (value <= 0) Die();
            else if (value > MaxHp) hp = MaxHp;
            else hp = value;
        }
    }
    public int hp;

    public int MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
        }
    }
    public int maxHp;

    public float Rate
    {
        get => rate;
        set
        {
            rate = value;
        }
    }
    public float rate;

    public Rigidbody rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(EShot());
    }

    public IEnumerator EShot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Rate);
            yield return StartCoroutine(Shot());
        }
    }

    public virtual IEnumerator Shot()
    {
        yield return null;
    }

    public virtual void Die()
    {
        ObjPool.Instance.Return(poolType, this);
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseBullet bb = other.GetComponent<BaseBullet>();
        BaseObject bo = other.GetComponent<BaseObject>();

        if (bb)
        {
            if (isEnemy == bb.isEnemy) return;

            if (bb.throughCount > 0)
            {
                bb.throughCount--;
            }
            else
            {
                ObjPool.Instance.Return(bb.poolType, bb);
            }

            Hp -= bb.damage;
        }
        else if (bo)
        {
            if (isEnemy == bo.isEnemy) return;

            Hp -= bo.damage / 2;
        }
    }
}
