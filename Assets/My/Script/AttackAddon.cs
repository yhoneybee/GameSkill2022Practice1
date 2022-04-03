using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAddon : BaseAddon
{
    public float rate;

    public override void Start()
    {
        base.Start();
        StartCoroutine(EShot());
    }

    public IEnumerator EShot()
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            if (K.IsGameStopped) continue;
            Shot();
        }
    }

    public void Shot()
    {
        var near = K.GetNearEnemy(K.player.transform);
        if (!near) return;
        var bezier = K.Shot<BezierBullet>(PoolType.BezierBullet, transform.position, Vector3.zero, K.player.damage, 200);
        bezier.shoter = transform;
        bezier.target = near.transform;
        bezier.isTracking = true;
        bezier.Init();
    }
}
