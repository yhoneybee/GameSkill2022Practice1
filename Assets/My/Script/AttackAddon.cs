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

    }
}
