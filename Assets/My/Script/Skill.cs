using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum eSKILL_TYPE
{
    Boom,
    Overload,
    Reflect,
}

public class Skill : MonoBehaviour
{
    public Image imgFade;
    public Image imgCool;
    public Text txtKey;
    public Text txtCool;

    public eSKILL_TYPE skillType;
    KeyCode castKey;

    public float coolDown;

    public float CurCoolDown
    {
        get => curCoolDown;
        set
        {
            if (value < 0) value = 0;
            else if (value > coolDown) value = coolDown;
            curCoolDown = value;
        }
    }
    public float curCoolDown;
    public bool IsWaitCoolDown => curCoolDown > 0;

    public void Start()
    {
        txtKey.text = skillType switch
        {
            eSKILL_TYPE.Boom => "A",
            eSKILL_TYPE.Overload => "S",
            eSKILL_TYPE.Reflect => "D",
            _ => "",
        };

        castKey = skillType switch
        {
            eSKILL_TYPE.Boom => KeyCode.A,
            eSKILL_TYPE.Overload => KeyCode.S,
            eSKILL_TYPE.Reflect => KeyCode.D,
            _ => KeyCode.None,
        };

        StartCoroutine(ESkillCoolDown());
    }

    public IEnumerator ESkillCoolDown()
    {
        while (true)
        {
            yield return null;
            if (IsWaitCoolDown)
            {
                imgFade.fillAmount = 1;
                imgCool.fillAmount = Mathf.InverseLerp(0, coolDown, CurCoolDown);
                CurCoolDown = Mathf.MoveTowards(CurCoolDown, 0, K.GameDT);
                txtCool.text = $"{Mathf.Round(CurCoolDown)}";
            }
            else
            {
                imgFade.fillAmount = 0;
                imgCool.fillAmount = 0;
                txtCool.text = $"";
            }
        }
    }

    public void Update()
    {
        CurCoolDown -= K.GameDT;
        if (Input.GetKeyDown(castKey))
        {
            Cast();
        }
    }

    public void Cast()
    {
        if (IsWaitCoolDown) return;
        CurCoolDown = coolDown;

        switch (skillType)
        {
            case eSKILL_TYPE.Boom:
                break;
            case eSKILL_TYPE.Overload:
                break;
            case eSKILL_TYPE.Reflect:
                K.bullets.ForEach(bb => bb.ChangeBullet());
                break;
        }
    }
}
