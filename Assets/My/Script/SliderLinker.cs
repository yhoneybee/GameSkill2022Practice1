using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLinker : MonoBehaviour
{
    public Slider main;
    public Slider sub;
    public Text txt;

    public string perfix;

    public int maxValue;
    public int curValue;

    public int MaxValue
    {
        get => maxValue;
        set
        {
            maxValue = value;
            main.maxValue = maxValue;
            sub.maxValue = maxValue;
        }
    }

    private void Start()
    {
        MaxValue = maxValue;
        StartCoroutine(ESliderValue());
    }

    public IEnumerator ESliderValue()
    {
        while (true)
        {
            yield return null;
            main.value = Mathf.MoveTowards(main.value, curValue, 0.2f);
            sub.value = Mathf.MoveTowards(sub.value, main.value, 0.04f);

            txt.text = $"{perfix} : {curValue} / {MaxValue}";
        }
    }
}
