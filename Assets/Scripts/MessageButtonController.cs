using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageButtonController : MonoBehaviour
{
    public void AddAccessedMessages(string buildingCode)
    {
        // Store the access of the messages
        ActionInfo actionInfo = new ActionInfo();
        actionInfo.Add("building_code", buildingCode);
        GameDataController.Add("Message_Access", actionInfo);
    }
}
