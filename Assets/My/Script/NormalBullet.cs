using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BaseBullet
{
    public override void Get()
    {
        base.Get();
        StartCoroutine(EMove());
    }

    public override void Return()
    {
        base.Return();
    }
}
