using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseObject
{
    public GameObject[] rotatable;
    public List<BaseAddon>[] addons = new List<BaseAddon>[((int)eADDON_TYPE.End)];

    public int onceShotCount = 1;
    public int multiCount = 1;
    public int throughCount;
    public int bezierThroughCount;

    private void Awake()
    {
        K.player = this;
        BaseAddon.addonSpeed[0] = 30;
        BaseAddon.addonSpeed[1] = 60;
    }

    public override void Start()
    {
        base.Start();
        Hp = MaxHp = K.gameInfo.hp;
        damage = K.gameInfo.damage;
        moveSpeed = K.gameInfo.moveSpeed;

        StartCoroutine(EMoveNRotate());
    }

    public override void ChangeHp()
    {
        base.ChangeHp();
        GameManager.Instance.hpLinker.curValue = hp;
        GameManager.Instance.hpLinker.MaxValue = MaxHp;
    }

    public override void Die()
    {
        GameManager.Instance.SadEnding();
    }

    public IEnumerator EMoveNRotate()
    {
        float h, v;
        while (true)
        {
            yield return null;

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            transform.Translate(new Vector3(h, 0, v) * (Input.GetButton("Slow") ? (moveSpeed / 2) : moveSpeed) * K.GameDT);

            float x = Mathf.Clamp(transform.position.x, -195, 195);
            float z = Mathf.Clamp(transform.position.z, -100, 100);

            transform.position = new Vector3(x, 0, z);

            for (int i = 0; i < rotatable.Length; i++)
            {
                rotatable[i].transform.rotation = Quaternion.Lerp(rotatable[i].transform.rotation, Quaternion.Euler(0, 0, -h * (Input.GetButton("Slow") ? 20 : 30)), K.GameDT * 7);
            }
        }
    }

    public override IEnumerator Shot()
    {
        float w = 5;
        float b = -(w * onceShotCount) / 2;
        b += w / 2.0f;

        var wait = new WaitForSeconds(0.05f);

        for (int j = 0; j < multiCount; j++)
        {
            yield return wait;
            for (int i = 0; i < onceShotCount; i++)
            {
                K.Shot<NormalBullet>(PoolType.NormalBullet, transform.position + new Vector3(b + w * i, 0, 0), Vector3.forward, damage, 300);
            }
        }
    }

    public void AddAddon(BaseAddon ba)
    {
        
    }
}
