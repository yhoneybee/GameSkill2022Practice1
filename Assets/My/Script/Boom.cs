using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : BaseNotObject
{
    public override void Get()
    {
        base.Get();
        ObjPool.Instance.WaitReturn(poolType, this, 3);
    }

    public override void TriggerEnter(BaseAll ba)
    {
        BaseObject bo = ba.GetComponent<BaseObject>();
        if (bo)
        {
            bo.Hp -= damage;
        }
    }
}
