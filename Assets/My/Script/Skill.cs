using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSKILL_TYPE
{
    Boom,
    Laser,
    Overload,
}

public class Skill : MonoBehaviour
{
    public eSKILL_TYPE skillType;

    public float coolDown;
    public float curCoolDown;
    public bool IsWaitCoolDown => curCoolDown > 0;

    public bool hasRate;
    public float rate;
    public float time;

    private void Start()
    {
        StartCoroutine(ECoolDown());
    }

    public void Cast()
    {
        if (IsWaitCoolDown) return;
        curCoolDown = coolDown;
        StartCoroutine(ECast());
    }

    public IEnumerator ECast()
    {
        float timeValue = 0;
        float rateValue = 0;
        while (timeValue <= time)
        {
            yield return null;

            timeValue += K.GameDT;

            switch (skillType)
            {
                case eSKILL_TYPE.Boom:
                    break;
                case eSKILL_TYPE.Laser:
                    break;
                case eSKILL_TYPE.Overload:
                    break;
            }

            if (!hasRate) continue;

            rateValue += K.GameDT;

            if (rateValue >= rate)
            {
                rateValue = 0;

                switch (skillType)
                {
                    case eSKILL_TYPE.Boom:
                        break;
                    case eSKILL_TYPE.Laser:
                        break;
                    case eSKILL_TYPE.Overload:
                        break;
                }
            }
        }
    }

    public IEnumerator ECoolDown()
    {
        while (true)
        {
            yield return null;
            if (curCoolDown > 0) curCoolDown -= K.GameDT;
            else curCoolDown = 0;
        }
    }
}
