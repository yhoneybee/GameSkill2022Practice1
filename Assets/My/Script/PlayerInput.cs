using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(ECharge());
    }

    public IEnumerator ECharge()
    {
        float edgeCount = 0, theta = 0;
        while (true)
        {
            yield return null;
            if (Input.GetKey(KeyCode.Space))
            {
                if (edgeCount < 3)
                {
                    edgeCount += K.GameDT;
                    if (edgeCount < 1) continue;
                }
                else
                {
                    edgeCount = 3;
                }

                theta += 50;
                var shapes = K.Shapes(((int)edgeCount + 4), 10, theta);

                for (int i = 0; i < shapes.Count; i++)
                {
                    var bullet = K.Shot<NormalBullet>(PoolType.NormalBullet, transform.position + shapes[i], shapes[(i + 2) % shapes.Count].normalized, 0, 50, 200000);
                    ObjPool.Instance.WaitReturn(bullet.poolType, bullet, 0.05f);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                for (int i = 0; i < ((int)edgeCount + 2); i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    for (int j = 0; j < GameManager.Instance.Level; j++)
                    {
                        var near = K.GetNearEnemy(K.player.transform);
                        if (!near) continue;
                        var bezier = K.Shot<BezierBullet>(PoolType.BezierBullet, K.player.transform.position, Vector3.zero, K.player.damage + K.player.chargeDamage, 200);
                        bezier.shoter = K.player.transform;
                        bezier.target = near.transform;
                        bezier.isTracking = true;
                        bezier.Init();
                    }
                }

                edgeCount = 0;
            }
        }
    }
}