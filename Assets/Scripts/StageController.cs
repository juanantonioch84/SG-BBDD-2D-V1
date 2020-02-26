using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public void GoTo(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
