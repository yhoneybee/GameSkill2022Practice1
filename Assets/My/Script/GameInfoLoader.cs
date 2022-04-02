using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class GameInfo
{
    public int hp;
    public int damage;
    public float moveSpeed;

    public int[] enemiesHps = new int[6];
    public int[] enemiesDamages = new int[6];

    [Range(0.0f, 100.0f)]
    public float appearWhitePersent;
    [Range(0.0f, 100.0f)]
    public float appearRedPersent;
}

public class GameInfoLoader : Singletone<GameInfoLoader>
{
    public GameInfo gameInfo;

    public void MakeFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameInfo.json");
        if (!File.Exists(path))
        {
            string json = JsonUtility.ToJson(gameInfo);
            File.WriteAllText(path, json);
        }
    }

    public override void Awake()
    {
        base.Awake();
        Load();
    }

    public void Load()
    {
        MakeFile();

        string path = Path.Combine(Application.persistentDataPath, "GameInfo.json");
        string json = File.ReadAllText(path);
        gameInfo = JsonUtility.FromJson<GameInfo>(json);

        K.gameInfo = gameInfo;
    }
}
