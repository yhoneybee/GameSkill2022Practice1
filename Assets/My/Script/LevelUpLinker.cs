using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpLinker : MonoBehaviour
{
    public Button btn;
    public Text txt;

    public eLEVELUP levelUp;

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            switch (levelUp)
            {
                case eLEVELUP.MaxHpUp:
                    K.player.MaxHp += 50;
                    break;
                case eLEVELUP.AttackAddonAdd:
                    var aa = ObjPool.Instance.Get<AttackAddon>(PoolType.AttackAddon, Vector3.zero);
                    aa.idx = BaseAddon.addonCount[((int)eADDON_TYPE.Attack)]++;
                    break;
                case eLEVELUP.ShieldAddonAdd:
                    var sa = ObjPool.Instance.Get<ShieldAddon>(PoolType.ShieldAddon, Vector3.zero);
                    sa.idx = BaseAddon.addonCount[((int)eADDON_TYPE.Shield)]++;
                    break;
                case eLEVELUP.DamageUp:
                    K.player.damage += 2;
                    break;
                case eLEVELUP.ThroughCountUp:
                    ++K.player.throughCount;
                    break;
                case eLEVELUP.MultiCountUp:
                    ++K.player.multiCount;
                    break;
                case eLEVELUP.SkillReset:
                    for (int i = 0; i < GameManager.Instance.skills.Length; i++)
                        GameManager.Instance.skills[i].CurCoolDown = 0;
                    break;
                case eLEVELUP.BoomDamageUp:
                    ++K.player.BoomDamage;
                    break;
                case eLEVELUP.OverloadingTimeUp:
                    K.player.OverloadingTime += 0.3f;
                    break;
                case eLEVELUP.OverloadingRateUp:
                    K.player.OverloadingRate -= 0.3f;
                    break;
                case eLEVELUP.ChargeDamageUp:
                    ++K.player.chargeDamage;
                    break;
            }

            K.gameTS = 1;
            GameManager.Instance.pdDisappearLevelUp.Play();
        });
    }
}
