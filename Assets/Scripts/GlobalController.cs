using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class GlobalController : MonoBehaviour
{
    public static GlobalController Instance;
    public static GameObject exitMenu;
    public static bool allowExit = false;

    public Dictionary<string, string> _notesB1, _notesB2, _notesB3;
    public Dictionary<string, Dictionary<string, string>> _notes;
    //public List<Dictionary<string, string>> _notes;

    public List<string> _words;

    public List<string> _visitedBuildings;

    // Nodos y lines del diagrama, se utilizan para cargar el diagrama tal y cómo se dejó
    public Dictionary<string, NodeModel> StoredNodes { get; set; }
    public Dictionary<string, ERLine> StoredLines { get; set; }

    void Awake()
    {
        if (Instance == null) {

            _notesB1 = new Dictionary<string, string>();
            _notesB2 = new Dictionary<string, string>();
            _notesB3 = new Dictionary<string, string>();
            _notes = new Dictionary<string, Dictionary<string, string>> {
                { "b1", _notesB1 },
                { "b2", _notesB2 },
                { "b3", _notesB3 }
            };
            _words = new List<string>();
            _visitedBuildings = new List<string>();
            StoredNodes = new Dictionary<string, NodeModel>();
            StoredLines = new Dictionary<string, ERLine>();

            DontDestroyOnLoad(gameObject);
            Instance = this;
            ////AIzaSyCViVi2POQSTVW2bTq_y4iZHIjMdx5juf4

            //Dictionary<string, string> content = new Dictionary<string, string>();
            ////Fill key and value
            ////content.Add("grant_type", "client_credentials");
            //content.Add("client_id", "628190706109-qh8v1en5tvf3njc2hqd2r01gdsan5t5f.apps.googleusercontent.com");
            //content.Add("client_secret", "CBrkTShBC_Z-06yeNHwxCdJQ");

            //UnityWebRequest www = UnityWebRequest.Post("https://accounts.google.com/o/oauth2/auth", content);

            ////Send request
            //www.SendWebRequest();

            //if (www.isNetworkError || www.isHttpError) {
            //    Debug.Log(www.error);
            //} else {

            //    string resultContent = www.downloadHandler.text;
            //    Debug.Log(resultContent);
            //    string json = JsonUtility.FromJson<string>(resultContent);
            //    string output = JsonUtility.ToJson(www.downloadHandler.text);
            //    Debug.Log(output);

            //    //Return result
            //    Debug.Log(json);
            //}






            //UnityWebRequest www = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1V5MaZyMRsn-eRbQbNAJwGXWBGS11ZYwjokkFBh5k-Qs/values/Sheet1!A1:B1");
            //www.SetRequestHeader("Content-Type", "application/json");
            //www.SendWebRequest();

            //if (www.isNetworkError || www.isHttpError) {
            //    Debug.Log(www.error);
            //} else {
            //    Debug.Log("HOLA");
            //    Debug.Log(www.responseCode.ToString());
            //    // Show results as text

            //    Debug.Log(www.downloadHandler.text.GetType());
            //    //var output = JsonUtility.ToJson(www.downloadHandler.text, true);
            //    string output = JsonUtility.ToJson(www.downloadHandler.text);
            //    Debug.Log(output);

            //    // Or retrieve results as binary data
            //    byte[] results = www.downloadHandler.data;
            //}


            //MyClass myObject = new MyClass();
            //myObject.level = 1;
            //myObject.timeElapsed = 47.5f;
            //string bodyJsonString = JsonUtility.ToJson(myObject);
            //Debug.Log(bodyJsonString);

            //string url = "https://sheets.googleapis.com/v4/spreadsheets/1V5MaZyMRsn-eRbQbNAJwGXWBGS11ZYwjokkFBh5k-Qs/values/Sheet1!A1:B1:append?valueInputOption=USER_ENTERED";


            ////var request = new UnityWebRequest(url, "POST");
            ////byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
            ////request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            ////request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            ////request.SetRequestHeader("Content-Type", "application/json");
            ////request.SendWebRequest();
            ////Debug.Log("Status Code: " + request.responseCode);





            //var jsonString = JsonUtility.ToJson(bodyJsonString) ?? "";
            //UnityWebRequest request = UnityWebRequest.Put(url, jsonString);
            //request.SetRequestHeader("Content-Type", "application/json");
            //request.SendWebRequest();
            //Debug.Log("Status Code: " + request.responseCode);


            ////UnityWebRequest www2;
            ////Hashtable postHeader = new Hashtable();
            ////postHeader.Add("Content-Type", "application/json");

            //// convert json string to byte
            ////var formData = System.Text.Encoding.UTF8.GetBytes("{
            ////    'range': 'Sheet1!A1:B1',
            ////    'majorDimension': 'ROWS',
            ////    'values': [
            ////    ['Door', '$15', '2', '3/15/2016'],
            ////    ['Engine', '$100', '1', '3/20/2016'],
            ////    ],
            ////}");

            ////List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            ////formData.Add( new MultipartFormDataSection("field1=foo&field2=bar") );
            ////www2 = UnityWebRequest.Put("https://sheets.googleapis.com/v4/spreadsheets/1V5MaZyMRsn-eRbQbNAJwGXWBGS11ZYwjokkFBh5k-Qs/values/Sheet1!A1:B1:append?valueInputOption=USER_ENTERED", formData);
            ////www2.SetRequestHeader("Content-Type", "application/json");
            ////www2.SendWebRequest();





            ////WWWForm form = new WWWForm();
            ////form.AddField("myField", "myData");

            ////UnityWebRequest www = UnityWebRequest.Post("https://sheets.googleapis.com/v4/spreadsheets/1V5MaZyMRsn-eRbQbNAJwGXWBGS11ZYwjokkFBh5k-Qs:batchUpdate", form);
            ////www.SendWebRequest();

            ////if (www.isNetworkError || www.isHttpError) {
            ////    Debug.Log(www.error);
            ////} else {
            ////    Debug.Log("Form upload complete!");
            ////}



        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void AddNotes(Dictionary<string, string> notes, string BuildingCode)
    {
        foreach (KeyValuePair<string, string> note in notes) {
            if (! _notes[BuildingCode].ContainsKey(note.Key)) {
                _notes[BuildingCode].Add(note.Key, note.Value);
            }
        }
    }

    public void AddWords(List<string> words)
    {
        foreach (string word in words) {
            if (! _words.Contains(word)) {
                _words.Add(word);
            }
        }
        
        _words.Sort();

        //List<Order> SortedList = objListOrder.OrderBy(o => o.OrderDate).ToList();
    }

    public void AddVisitedBuilding(string buildingCode)
    {
        // Store the visit of the building
        ActionInfo actionInfo = new ActionInfo();
        actionInfo.Add("building_code", buildingCode);

        if (! _visitedBuildings.Contains(buildingCode)) {
            _visitedBuildings.Add(buildingCode);
            actionInfo.Add("repeat", "0");
        } else {
            actionInfo.Add("repeat", "1");
        }

        GameDataController.Add("visit_stage", actionInfo);
    }

    // Add an ERLine which links 2 nodes.
    public void AddERLine(string node1, string node2, bool cardinality)
    {
        ERLine erLine = new ERLine(node1, node2, cardinality);
        StoredLines.Add(erLine.Name, erLine);
    }

    // Search and return the line model of two linked nodes
    // Return null if the nodes are not linked 
    public bool ContainsERLine(string node1, string node2)
    {
        string ERLineName = ERLine.CreateName(node1, node2);
        return StoredLines.ContainsKey(ERLineName);
    }

    // Search and return the line model of two linked nodes
    // Return null if the nodes are not linked 
    public ERLine SearchERLine(string node1, string node2)
    {
        string ERLineName = ERLine.CreateName(node1, node2);
        return StoredLines.ContainsKey(ERLineName) ? StoredLines[ERLineName] : null;
    }

    // Remove the node erlines. If the transform is received, destroy the objects
    public void RemoveNodeERLines(string node, Transform t_parent = null)
    {
        List<ERLine> ERLines = new List<ERLine>();

        foreach (KeyValuePair<string, ERLine> erLine in StoredLines) {

            // If the node is linked here, destroy the line instance and the linemodel of the list
            if (erLine.Value.IsNodeLinked(node)) {
                ERLines.Add(erLine.Value);
            }
        }

        foreach (ERLine erLine in ERLines) {
            RemoveERLine(erLine, t_parent);
        }
    }

    // Remove a link between nodes. If the transform is received, destroy the objects
    public void RemoveERLine(ERLine erLine, Transform t_parent = null)
    {
        StoredLines.Remove(erLine.Name);

        if (t_parent != null) {
            Destroy(t_parent.Find(erLine.Name).gameObject);

            // If any node is an nodeattribute, unset the node
            GameObject node1 = t_parent.Find(erLine.Node1Name).gameObject;
            GameObject node2 = t_parent.Find(erLine.Node2Name).gameObject;

            if (node1.GetComponent<NodeController>()._nodeType == NodeType.Attribute) {
                node1.GetComponent<NodeAttributeController>().UnsetNode();
            } else if (node2.GetComponent<NodeController>()._nodeType == NodeType.Attribute) {
                node2.GetComponent<NodeAttributeController>().UnsetNode();
            }
        }
    }

    [System.Serializable]
    public class TokenClassName
    {
        public string access_token;
    }

    //private static IEnumerator GetAccessToken(Action<string> result)
    //{
    //    Dictionary<string, string> content = new Dictionary<string, string>();
    //    //Fill key and value
    //    content.Add("grant_type", "client_credentials");
    //    content.Add("client_id", "login-secret");
    //    content.Add("client_secret", "secretpassword");

    //    UnityWebRequest www = UnityWebRequest.Post("https://someurl.com//oauth/token", content);
    //    //Send request
    //    yield return www.Send();

    //    if (!www.isError) {
    //        string resultContent = www.downloadHandler.text;
    //        TokenClassName json = JsonUtility.FromJson<TokenClassName>(resultContent);

    //        //Return result
    //        result(json.access_token);
    //    } else {
    //        //Return null
    //        result("");
    //    }
    //}
}
