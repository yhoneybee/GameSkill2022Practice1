using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvoker : MonoBehaviour
{
    public Skill boom;
    public Skill laser;
    public Skill overload;

    private void Start()
    {
        StartCoroutine(ESkillInvoker());
        StartCoroutine(ECharge());
    }

    public IEnumerator ESkillInvoker()
    {
        while (true)
        {
            yield return null;

            if (Input.GetButton("SkillTrigger"))
            {
                if (Input.GetButtonDown("Skill1"))
                {
                    // ������
                    laser.Cast();
                }
                if (Input.GetButtonDown("Skill2"))
                {
                    // ����ȭ
                    overload.Cast();
                }
            }
            else
            {
                if (Input.GetButtonDown("Skill2"))
                {
                    // ����
                    boom.Cast();
                }
            }
        }
    }

    public IEnumerator ECharge()
    {
        while (true)
        {
            yield return null;

            if (Input.GetButtonDown("Skill1"))
            {
                // ��¡
            }
        }
    }
}
