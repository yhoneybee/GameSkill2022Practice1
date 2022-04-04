using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNotObject : BaseAll
{
    public bool isTrigger;

    public abstract void TriggerEnter(BaseAll ba);

    public void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;

        BaseAll ba = other.GetComponent<BaseAll>();

        if (ba) TriggerEnter(ba);
    }
}
