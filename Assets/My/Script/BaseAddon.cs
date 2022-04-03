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
    public eADDON_TYPE addonType;

    public static int[] addonCount = new int[((int)eADDON_TYPE.End)];
    public static float[] addonTheta = new float[((int)eADDON_TYPE.End)];

    public int idx;

    public float radius;

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
            transform.position = K.Shapes(addonCount[((int)addonType)], radius, addonTheta[((int)addonType)])[idx] + K.player.transform.position;
        }
    }
}