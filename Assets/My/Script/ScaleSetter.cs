using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSetter : MonoBehaviour
{
    private void OnEnable()
    {
        float scale = transform.parent.localScale.x;
        transform.localScale = GameManager.Instance.mapIconScale / scale * Vector3.one;
    }
}
