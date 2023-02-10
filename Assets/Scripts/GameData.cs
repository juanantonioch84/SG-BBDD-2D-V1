using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this) {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }
}


[Serializable] public class ActionInfo : SerializableDictionary<string, string> { }

[Serializable]
public class ERDiagram
{
    public List<string> entities;
    public List<string> attributes;
    public List<string> relations;
    public List<string> ent_rel_links;
    public List<string> ent_att_links;
    public List<string> att_rel_links;
    public ActionInfo cardinalities;

    public ERDiagram()
    {
        entities = new List<string>();
        attributes = new List<string>();
        relations = new List<string>();
        ent_rel_links = new List<string>();
        ent_att_links = new List<string>();
        att_rel_links = new List<string>();
        cardinalities = new ActionInfo();
    }
}

[Serializable]
public class GameDataRow
{
    public string player_id;
    public string case_id;
    public string date_time;
    public string action_name;
    public ActionInfo action_info;
    public ERDiagram er_diagram;
}

[Serializable]
public class GameDataSet
{
    public List<GameDataRow> dataset;

    public string Player { get; set; }
    public string CaseId { get; set; }

    public string Filepath { get; }

    public GameDataSet(string player)
    {
        dataset = new List<GameDataRow>();

        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int cur_time = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        Player = player;
        CaseId = player + "-" + cur_time.ToString();

        Filepath = Path.Combine(Application.persistentDataPath, "data-" + CaseId + ".json");
    }

    public void Add(string action_name, ActionInfo action_info, ERDiagram er_diagram)
    {
        // TODO - TO BE REMOVED
        //System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        //int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        //Player = "UAN";
        //CaseId = "UAN" + "-" + cur_time.ToString();


        GameDataRow gameDataRow = new GameDataRow();
        gameDataRow.player_id = Player;
        gameDataRow.case_id = CaseId;
        gameDataRow.date_time = DateTime.UtcNow.ToString("dd/MM/yyyy H:mm:ss (zzz)");
        gameDataRow.action_name = action_name;
        gameDataRow.action_info = action_info;
        gameDataRow.er_diagram = er_diagram;

        dataset.Add(gameDataRow);

        // Save all the current content into the file
        Save();
    }

    public void Save()
    {
        string gameDataJsonString = JsonHelper.ToJson(dataset.ToArray());

        using (StreamWriter streamWriter = File.CreateText(Filepath)) {
            streamWriter.Write(gameDataJsonString);
        }
    }
}

[Serializable]
public static class GameDataController
{    
    // Game data set to store user's actions
    public static GameDataSet DataSet;

    // Init player gameplay
    public static void InitPlayer(string player)
    {
        DataSet = new GameDataSet(player);
    }

    // Register an user's action
    public static void Add(string actionName, ActionInfo actionInfo = null, ERDiagram erdiagram = null)
    {
        DataSet.Add(actionName, actionInfo, erdiagram);
    }

    //// Save the game data set into a file
    //public static void Save()
    //{
    //    //string filePath = Application.dataPath + "/StreamingAssets/data.json";
    //    string gameDataJsonString = JsonUtility.ToJson(DataSet);

    //    //if (! Directory.Exists(Application.dataPath + "/StreamingAssets/")) {
    //    //    Directory.CreateDirectory(Application.dataPath + "/StreamingAssets/");
    //    //}

    //    //Debug.Log(Application.persistentDataPath);

    //    //File.WriteAllText(filePath, gameDataJsonString);


    //    //string filePath = Path.Combine(Application.dataPath, "data.json");

    //    //using (StreamWriter streamWriter = File.CreateText(filePath)) {
    //    //    streamWriter.Write(gameDataJsonString);
    //    //}

    //}
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}