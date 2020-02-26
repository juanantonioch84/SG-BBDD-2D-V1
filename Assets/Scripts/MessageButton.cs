using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public void AddAccessedMessages(string buildingCode)
    {
        // Store the access of the messages
        Debug.Log("Acess Messages seems Working");
        ActionInfo actionInfo = new ActionInfo();
        actionInfo.Add("building_code", buildingCode);
        GameDataController.Add("Message_Access", actionInfo);
    }
}
