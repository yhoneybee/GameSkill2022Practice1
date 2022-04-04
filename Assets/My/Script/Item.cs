using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eITEM_TYPE
{
    ShotUpgrade,
    NoDamage,
    HpHeal,
    PainHeal,
    End,
}

public class Item : BaseNotObject
{
    public eITEM_TYPE itemType;

    public override void Start()
    {
        base.Start();
        StartCoroutine(EMove());
    }

    public override void Get()
    {
        base.Get();
        itemType = (eITEM_TYPE)Random.Range(0, ((int)eITEM_TYPE.End));
    }

    public IEnumerator EMove()
    {
        while (true)
        {
            yield return null;
            transform.Translate(Vector3.back * moveSpeed * K.GameDT);
        }
    }

    public override void TriggerEnter(BaseAll ba)
    {
        var player = ba.GetComponent<Player>();

        if (player)
        {
            switch (itemType)
            {
                case eITEM_TYPE.ShotUpgrade:
                    if (player.onceShotCount < 5) ++player.onceShotCount;
                    GameManager.Instance.txtItem.text = $"Shot Upgrade";
                    break;
                case eITEM_TYPE.NoDamage:
                    player.noDamageTime = 3;
                    GameManager.Instance.txtItem.text = $"No Damage 3 sec";
                    break;
                case eITEM_TYPE.HpHeal:
                    player.Hp += 10;
                    GameManager.Instance.txtItem.text = $"Hp Heal 10";
                    break;
                case eITEM_TYPE.PainHeal:
                    GameManager.Instance.Pain -= 10;
                    GameManager.Instance.txtItem.text = $"Pain Heal 10";
                    break;
            }

            GameManager.Instance.pdItem.Stop();
            GameManager.Instance.pdItem.Play();

            ObjPool.Instance.Return(poolType, this);
        }
    }
}
