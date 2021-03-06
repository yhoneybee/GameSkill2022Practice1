using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class BaseAll : MonoBehaviour
{
    public SphereCollider sphereCollider;

    public ePOOL_TYPE poolType;

    public bool isEnemy = true;

    public int damage;

    public float moveSpeed;

    public virtual void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    public virtual void Get() { }
    public virtual void Return() { }
}
