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
}
