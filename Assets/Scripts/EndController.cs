using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows;
using System;
using System.IO;

public class EndController : MonoBehaviour
{
    public GameObject Inventory;
    public Text Msg;
    public Button Confirm;
    public Button Copy;
    public Button Exit;


    public void onConfirm()
    {
        GameDataController.Add("exit_game");
        Inventory.SetActive(false);
        Confirm.gameObject.SetActive(false);
        Msg.text = "Los datos de su partida están en: " + System.Environment.NewLine + "<b>" + GameDataController.DataSet.Filepath + "</b>" + System.Environment.NewLine + "¡Gracias por jugar!";
        Msg.gameObject.SetActive(true);
        Copy.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
    }

    public void onCopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = GameDataController.DataSet.Filepath;
    }

    public void onExit()
    {
        Application.Quit();
    }
}