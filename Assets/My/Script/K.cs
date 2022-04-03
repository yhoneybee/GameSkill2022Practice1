using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum eLEVELUP
{
    MaxHpUp,
    AttackAddonAdd,
    ShieldAddonAdd,
    DamageUp,
    ThroughCountUp,
    MultiCountUp,
    SkillReset,
    BoomDamageUp,
    OverloadingTimeUp,
    OverloadingRateUp,
    ChargeDamageUp,
    End,
}

public static class K
{
    public static Player player = null;

    public static GameInfo gameInfo = null;
    public static RankInfo rankInfo = null;

    public static List<BaseEnemy> enemies = new List<BaseEnemy>();
    public static List<BaseBullet> bullets = new List<BaseBullet>();

    public static string[] levelUpInfos = 
    {
        "[ Stat ]\nMaxHp\nUp",
        "[ Addon ]\nAttack\nAddon\nAdd",
        "[ Addon ]\nShield\nAddon\nAdd",
        "[ Stat ]\nDamage\nUp",
        "[ Stat ]\nThrough\nCount\nUp",
        "[ Stat ]\nMulti\nCount\nUp",
        "[ Skill ]\nCool\nTime\nReset",
        "[ Skill ]\nBoom\nDamage\nUp",
        "[ Skill ]\nOverloading\nTime\nUp",
        "[ Skill ]\nOverloading\nRate\nUp",
        "[ Stat ]\nCharge\nDamage\nUp",
    };

    public static BaseEnemy GetNearEnemy(Transform origin, Predicate<BaseEnemy> match)
        => enemies.FindAll(x => x.gameObject.activeSelf && x.transform != origin && match(x)).OrderBy(x => Vector3.Distance(x.transform.position, origin.transform.position)).FirstOrDefault();

    public static BaseEnemy GetNearEnemy(Transform origin)
        => enemies.FindAll(x => x.gameObject.activeSelf && x.transform != origin).OrderBy(x => Vector3.Distance(x.transform.position, origin.transform.position)).FirstOrDefault();

    public static float GameDT => Time.deltaTime * gameTS;
    public static float gameTS = 1;

    public static bool IsGameStopped => gameTS == 0;

    public static Vector3 BezierCurve(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 cd = Vector3.Lerp(c, d, t);

        Vector3 abbc = Vector3.Lerp(ab, bc, t);
        Vector3 bccd = Vector3.Lerp(bc, cd, t);

        return Vector3.Lerp(abbc, bccd, t);
    }

    public static Vector3 Circle(float theta, float x, float y)
    {
        return new Vector3(Mathf.Cos(theta * Mathf.Deg2Rad) * x, 0, Mathf.Sin(theta * Mathf.Deg2Rad) * y);
    }

    public static Vector3 Circle(float theta, float radius)
    {
        return Circle(theta, radius, radius);
    }

    public static List<Vector3> Shapes(int edgeCount, float radius, float theta)
    {
        List<Vector3> result = new List<Vector3>();

        float add = 360 / edgeCount;

        for (float i = 0; i < 360; i += add)
        {
            result.Add(Circle(i + theta, radius));
        }

        return result;
    }

    public static T Shot<T>(PoolType type, Vector3 pos, Vector3 dir, int damage, float moveSpeed, int throughCount = 0, bool isEnemy = false)
        where T : BaseBullet
    {
        var bullet = ObjPool.Instance.Get<T>(type, pos);
        bullet.dir = dir;
        bullet.damage = damage;
        bullet.moveSpeed = moveSpeed;
        bullet.isEnemy = isEnemy;
        bullet.throughCount = throughCount;
        bullet.tr.colorGradient = isEnemy ? bullet.enemyColor : bullet.playerColor;

        return bullet;
    }

    public static IEnumerator EChangeScale(Transform tf, Vector3 toScale, float speed)
    {
        while (Vector3.Distance(tf.localScale, toScale) >= 0.01f)
        {
            yield return null;
            tf.localScale = Vector3.Lerp(tf.localScale, toScale, speed * Time.deltaTime);
        }
        tf.localScale = toScale;
    }
}
