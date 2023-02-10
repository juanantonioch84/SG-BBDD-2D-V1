using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordsWindowController : MonoBehaviour
{
    public Button _wordPrefab;

    private void Awake()
    {
        List<string> words = new List<string>(GlobalController.Instance._words);
        //List<string> words = init();

        foreach (string word in words) {
            Button tempButton = Instantiate(_wordPrefab, Vector2.zero, Quaternion.identity) as Button;
            tempButton.transform.SetParent(this.transform, false);
            tempButton.name = char.ToUpper(word[0]) + word.Substring(1);
            tempButton.GetComponentInChildren<Text>().text = char.ToUpper(word[0]) + word.Substring(1);
        }
    }

    // Just for develop testing :_)
    private List<string> init()
    {
        return NotesData.GetAllWordList();
    }
}
