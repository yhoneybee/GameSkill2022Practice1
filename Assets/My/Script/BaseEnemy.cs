using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseObject
{
    public override void Start()
    {
        base.Start();
        isEnemy = true;
        StartCoroutine(EMove());

        sphereCollider.radius = 1;
    }

    public override void Get()
    {
        base.Get();

        GameInfo gameInfo = JsonLoader.Instance.gameInfo;
        int idx = ((int)poolType) - 2;
        Hp = MaxHp = gameInfo.enemiesHps[idx];
        damage = gameInfo.enemiesDamages[idx];
    }

    public abstract IEnumerator EMove();
}
