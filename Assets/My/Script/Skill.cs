using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum eSKILL_TYPE
{
    Boom,
    Laser,
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

    bool isLaser;

    public void Start()
    {
        txtKey.text = skillType switch
        {
            eSKILL_TYPE.Boom => "A",
            eSKILL_TYPE.Laser => "S",
            eSKILL_TYPE.Reflect => "D",
            _ => "",
        };

        castKey = skillType switch
        {
            eSKILL_TYPE.Boom => KeyCode.A,
            eSKILL_TYPE.Laser => KeyCode.S,
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
                float add = 180 / 5;
                for (float i = 0; i <= 180; i += add)
                {
                    K.Shot<BoomBullet>(ePOOL_TYPE.BoomBullet, K.player.transform.position, K.Circle(i, 1), K.player.damage, 100);
                }
                break;
            case eSKILL_TYPE.Laser:
                if (!isLaser) StartCoroutine(ELaser());
                break;
            case eSKILL_TYPE.Reflect:
                K.bullets.ForEach(bb => bb.ChangeBullet());
                break;
        }
    }

    public IEnumerator ELaser()
    {
        isLaser = true;

        float time = 0;
        float rate = 0;
        Vector3 laserEndPos = K.player.transform.position;
        LineRenderer lr = K.player.lr;

        while (time <= K.player.LaserTime)
        {
            yield return null;
            if (K.IsGameStopped) continue;
            time += K.GameDT;
            rate += K.GameDT;


            lr.SetPosition(0, K.player.transform.position + Vector3.forward * 10);
            laserEndPos = Vector3.MoveTowards(laserEndPos, Vector3.forward * 200, 1);
            laserEndPos = new Vector3(K.player.transform.position.x, 0, laserEndPos.z);
            lr.SetPosition(1, laserEndPos);

            if (rate >= K.player.LaserRate)
            {
                var hits = Physics.BoxCastAll(K.player.transform.position, 5 * Vector3.one, Vector3.forward);

                foreach (var hit in hits)
                {
                    BaseObject bo = hit.transform.GetComponent<BaseObject>();
                    if (bo)
                    {
                        if (K.player.isEnemy == bo.isEnemy) continue;
                        bo.Hp -= K.player.damage;
                    }
                }
            }
        }

        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);

        isLaser = false;
    }
}
