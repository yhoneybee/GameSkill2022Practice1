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
            int beforeHp = hp;
            hp = value;
            if (value <= 0)
            {
                hp = 0;
                Die();
            }
            else if (value > MaxHp) hp = MaxHp;
            ChangeHp(beforeHp, hp);
        }
    }
    public int hp;

    public int MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
            ChangeHp(hp, hp);
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

    public bool noDamage;

    public Rigidbody rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void Get()
    {
        base.Get();
        StartCoroutine(EWaitRate());
    }

    public IEnumerator EWaitRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Rate);
            if (K.IsGameStopped) continue;
            yield return StartCoroutine(EShot());
        }
    }

    public abstract IEnumerator EShot();

    public virtual void Die()
    {
        ObjPool.Instance.Return(poolType, this);
    }

    public virtual void ChangeHp(int now, int to) { }

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

            if (!noDamage) Hp -= bb.damage;
        }
        else if (bo)
        {
            if (isEnemy == bo.isEnemy) return;

            if (!noDamage) Hp -= bo.damage / 2;
        }
    }
}
