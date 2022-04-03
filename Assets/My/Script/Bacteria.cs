using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : BaseEnemy
{
    public override IEnumerator EMove()
    {
        yield return null;
    }

    public override IEnumerator Shot()
    {
        float time = 0;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            time += K.GameDT;
            var v3s = K.Shapes(i + 3, 1, time);
            for (int j = 0; j < v3s.Length; j++)
            {
                yield return new WaitForSeconds(0.1f);
                K.Shot<NormalBullet>(PoolType.NormalBullet, transform.position, v3s[j], damage, 100, 0, true);
            }
        }
    }
}
