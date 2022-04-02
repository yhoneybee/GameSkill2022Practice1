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
                    // ·¹ÀÌÀú
                    laser.Cast();
                }
                if (Input.GetButtonDown("Skill2"))
                {
                    // °úºÎÈ­
                    overload.Cast();
                }
            }
            else
            {
                if (Input.GetButtonDown("Skill2"))
                {
                    // Æø¹ß
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
                // Â÷Â¡
            }
        }
    }
}
