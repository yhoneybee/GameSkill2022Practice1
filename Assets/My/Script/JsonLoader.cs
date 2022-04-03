using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
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

[Serializable]
public class Score
{
    public string name;
    public int score;
}

[Serializable]
public class RankInfo
{
    public List<Score> scores = new List<Score>();
    public List<Score> GetHighScores(int count)
        => scores.OrderByDescending(x => x.score).ToList().GetRange(0, count);
}

public class JsonLoader : Singletone<JsonLoader>
{
    public GameInfo gameInfo;
    public RankInfo rankInfo;

    public void Write<T>(string fileName, T saveTarget)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.json");
        if (!File.Exists(path))
        {
            string json = JsonUtility.ToJson(saveTarget);
            File.WriteAllText(path, json);
        }
    }

    public T Read<T>(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.json");
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }

    public T WriteAndRead<T>(string fileName, T saveTarget)
    {
        Write(fileName, saveTarget);
        return Read<T>(fileName);
    }

    public void Load()
    {
        gameInfo = WriteAndRead("GameInfo", gameInfo);
        rankInfo = WriteAndRead("RankInfo", rankInfo);

        K.gameInfo = gameInfo;
        K.rankInfo = rankInfo;
    }

    public override void Awake()
    {
        base.Awake();
        Load();
    }
}
