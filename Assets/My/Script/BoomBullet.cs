using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : BaseBullet
{
    public ePOOL_TYPE spawnType;

    public override void Return()
    {
        BaseNotObject bno = ObjPool.Instance.Get<BaseNotObject>(ePOOL_TYPE.Boom, transform.position);
        bno.damage = damage * 10;
        bno.isTrigger = true;
        bno.isEnemy = isEnemy;
        base.Return();
    }
}
