using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
    public SliderLinker hpLinker;
    public SliderLinker painLinker;
    public SliderLinker expLinker;

    public int pain;

    public int maxPain = 100;

    public int Pain
    {
        get => pain;
        set
        {
            if (value > maxPain) value = maxPain;
            else if (value < 0) value = 0;
            pain = value;
            painLinker.curValue = value;
        }
    }

    public int level = 1;

    public int Level
    {
        get => level;
        set
        {
            level = value;
        }
    }

    public int NeedExp => level * 5 + 5;

    public int exp;

    public int Exp
    {
        get => exp;
        set
        {
            if (value >= NeedExp)
            {
                value %= NeedExp;
                ++Level;
                expLinker.MaxValue = NeedExp;
            }
            exp = value;

            expLinker.curValue = exp;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Exp += 10;
            Pain += 10;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Exp -= 10;
            Pain -= 10;
        }
    }
}
