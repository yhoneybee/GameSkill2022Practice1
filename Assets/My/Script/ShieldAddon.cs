using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShieldAddon : BaseAddon
{
    public Rigidbody rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseBullet bb = other.GetComponent<BaseBullet>();

        if (bb)
        {
            if (isEnemy == bb.isEnemy) return;

            ObjPool.Instance.Return(bb.poolType, bb);
        }
    }
}
