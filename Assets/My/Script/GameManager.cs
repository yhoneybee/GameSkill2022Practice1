using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class GameManager : Singletone<GameManager>
{
    public SliderLinker hpLinker;
    public SliderLinker painLinker;
    public SliderLinker expLinker;

    public PlayableDirector pdAppearLevelUp;
    public PlayableDirector pdDisappearLevelUp;

    public LevelUpLinker[] levelUpLinkers = new LevelUpLinker[3];

    public Skill[] skills = new Skill[3];

    public float mapIconScale = 0.5f;

    public int pain;

    public int maxPain = 100;

    public int Pain
    {
        get => pain;
        set
        {
            if (value > maxPain)
            {
                value = maxPain;
                SadEnding();
            }
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
                K.gameTS = 0;
                RandomIndex();
                pdAppearLevelUp.Play();
                expLinker.MaxValue = NeedExp;
            }
            exp = value;

            expLinker.curValue = exp;
        }
    }

    public void SadEnding()
    {
        Debug.LogWarning($"SadEnding Method in GameManager is empty");
    }

    public void HappyEnding()
    {
        Debug.LogWarning($"HappyEnding Method in GameManager is empty");
    }

    public void RandomIndex()
    {
        List<int> idx = new List<int>();

        for (int i = 0; i < ((int)eLEVELUP.End); i++) idx.Add(i);

        int rand = Random.Range(0, idx.Count);

        levelUpLinkers[0].txt.text = K.levelUpInfos[idx[rand]];
        levelUpLinkers[0].levelUp = (eLEVELUP)idx[rand];

        idx.RemoveAt(rand);
        rand = Random.Range(0, idx.Count);

        levelUpLinkers[1].txt.text = K.levelUpInfos[idx[rand]];
        levelUpLinkers[1].levelUp = (eLEVELUP)idx[rand];

        idx.RemoveAt(rand);
        rand = Random.Range(0, idx.Count);

        levelUpLinkers[2].txt.text = K.levelUpInfos[idx[rand]];
        levelUpLinkers[2].levelUp = (eLEVELUP)idx[rand];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            var aa = ObjPool.Instance.Get<AttackAddon>(PoolType.AttackAddon, Vector3.zero);
            aa.idx = BaseAddon.addonCount[((int)eADDON_TYPE.Attack)]++;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            var sa = ObjPool.Instance.Get<ShieldAddon>(PoolType.ShieldAddon, Vector3.zero);
            sa.idx = BaseAddon.addonCount[((int)eADDON_TYPE.Shield)]++;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            K.gameTS = 0;
            RandomIndex();
            pdAppearLevelUp.Play();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            K.gameTS = 1;
            pdDisappearLevelUp.Play();
        }
    }
}
