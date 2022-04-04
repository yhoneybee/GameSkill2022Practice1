using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseObject
{
    public int score;

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

    public override void Die()
    {
        GameManager.Instance.score += score;
        if (Random.Range(0, 100) < (GameManager.Instance.stage == 1 ? 20 : 10)) ObjPool.Instance.Get<Item>(ePOOL_TYPE.Item, transform.position);

        base.Die();
    }

    public abstract IEnumerator EMove();
}
