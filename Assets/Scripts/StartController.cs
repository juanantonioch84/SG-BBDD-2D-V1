using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public InputField inputPlayerID;

    public void onClick()
    {
        if (inputPlayerID.text != "") {

            // Init player gameplay
            GameDataController.InitPlayer(inputPlayerID.text);

            // Add the init of the game
            GameDataController.Add("init_game");

            SceneManager.LoadScene("MapScene");
        }
    }
}
