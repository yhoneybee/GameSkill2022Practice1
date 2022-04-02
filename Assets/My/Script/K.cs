using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class K
{
    public static Player player = null;

    public static GameInfo gameInfo = null;

    public static float GameDT => Time.deltaTime * gameTS;
    public static float gameTS = 1;

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

    public static Vector3[] Shapes(int angleCount, float radius, float unClockRotate)
    {
        Vector3[] result = new Vector3[angleCount];

        int add = 360 / angleCount;

        int idx = 0;

        for (int i = 0; i < 360; i += add)
        {
            result[idx++] = Circle(i + unClockRotate, radius);
        }

        return result;
    }

    public static void Shot<T>(PoolType type, Vector3 pos, Vector3 dir, int damage, float moveSpeed, int throughCount = 0, bool isEnemy = false)
        where T : BaseBullet
    {
        var bullet = ObjPool.Instance.Get<T>(type, pos);
        bullet.dir = dir;
        bullet.damage = damage;
        bullet.moveSpeed = moveSpeed;
        bullet.isEnemy = isEnemy;
        bullet.throughCount = throughCount;

        if (isEnemy) bullet.tr.colorGradient = bullet.enemyColor;
        else bullet.tr.colorGradient = bullet.playerColor;
    }
}
