using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierBullet : BaseBullet
{
    public Transform shoter;
    public Transform target;

    public Vector3[] points = new Vector3[2];

    float needTime;

    bool isInit;
    bool isMissingTarget;

    public override void Return()
    {
        base.Return();
        isInit = false;
    }

    public void Init()
    {
        needTime = Random.Range(0.6f, 1.0f);

        points[0] = shoter.position + (6 * Random.Range(-1.0f, 1.0f) * shoter.right) + (6 * Random.Range(-1.0f, -0.6f) * shoter.forward);
        points[1] = target.position + (6 * Random.Range(-1.0f, 1.0f) * target.right) + (6 * Random.Range(0.6f, 1.0f) * target.forward);

        transform.position = shoter.position;

        isInit = true;
        isMissingTarget = false;
    }

    public override IEnumerator EMove()
    {
        float time = 0;
        while (true)
        {
            yield return null;
            if (!isInit) continue;
            if (!target.gameObject.activeSelf && !isMissingTarget)
            {
                isMissingTarget = true;

                dir = (target.transform.position - transform.position).normalized;

                StartCoroutine(base.EMove());
            }
            if (isMissingTarget) continue;
            time += K.GameDT;
            transform.position = K.BezierCurve(shoter.position, points[0], points[1], target.position, time / needTime);
        }
    }
}
