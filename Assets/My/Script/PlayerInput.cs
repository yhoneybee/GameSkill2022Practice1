using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Skill boom;
    public Skill laser;
    public Skill overload;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ObjPool.Instance.Get(PoolType.Bacteria, Vector3.zero);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var near = K.GetNearEnemy(K.player.transform);
            if (near)
            {
                var bezier = K.Shot<BezierBullet>(PoolType.BezierBullet, transform.position, Vector3.zero, K.player.damage, 200, K.player.bezierThroughCount);
                bezier.shoter = K.player.transform;
                bezier.target = K.GetNearEnemy(K.player.transform).transform;
                bezier.Init();
            }
        }
    }
}