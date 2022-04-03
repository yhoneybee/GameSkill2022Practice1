using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BaseBullet
{
    public override void ChangeBullet()
    {
        if (isEnemy)
        {
            dir = -dir;
        }
        base.ChangeBullet();
    }

    public override void Get()
    {
        base.Get();
    }

    public override void Return()
    {
        base.Return();
    }
}
