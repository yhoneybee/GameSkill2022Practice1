using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eADDON_TYPE
{
    Attack,
    Shield,
    End,
}

public abstract class BaseAddon : BaseAll
{
    public static int[] addonCount = new int[((int)eADDON_TYPE.End)];
    public static float[] addonSpeed = new float[((int)eADDON_TYPE.End)];

    public int idx;

    public float theta;
    public float radius;

    public bool isUnClockRotate;

    public override void Start()
    {
        base.Start();
        StartCoroutine(EOrbit());
    }

    public IEnumerator EOrbit()
    {
        while (true)
        {
            yield return null;
            if (isUnClockRotate) theta += addonSpeed[((int)poolType)];
            else theta -= addonSpeed[((int)poolType)];
            transform.position = K.Shapes(addonCount[((int)poolType)], radius, theta)[idx];
        }
    }
}