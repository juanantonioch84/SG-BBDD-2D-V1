using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class ScenesHandler : MonoBehaviour
{

    public bool isGamePaused = false;

    public void LoadScene(string scene)
    {
        if (scene != SceneManager.GetActiveScene().name) {
            // Store the scene change of the game
            ActionInfo actionInfo = new ActionInfo();
            actionInfo.Add("scene_from", SceneManager.GetActiveScene().name);
            actionInfo.Add("scene_to", scene);
            GameDataController.Add("select_scene", actionInfo);

            SceneManager.LoadScene(scene);
        }
    }

    public void PauseGame(bool isPaused)
    {
        isGamePaused = isPaused;
    }
}
