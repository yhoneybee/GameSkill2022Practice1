using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singletone<T> : MonoBehaviour
    where T : class
{
    public static T Instance { get; private set; } = null;

    public virtual void Awake()
    {
        Instance = GetComponent<T>();
    }

    public void OnDestroy()
    {
        Instance = null;
    }
}
