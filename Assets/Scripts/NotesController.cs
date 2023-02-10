using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesController : MonoBehaviour
{
    public Text _notePrefab;

    private void Start()
    {
        List<string> notes = new List<string>(GlobalController.Instance._notes.Values);
        //List<string> notes = init();

        foreach (string note in notes) {
            Text tempTextBox = Instantiate(_notePrefab, Vector2.zero, Quaternion.identity) as Text;
            tempTextBox.transform.SetParent(this.transform, false);
            tempTextBox.transform.SetAsLastSibling();
            tempTextBox.supportRichText = true;
            tempTextBox.text = note;
        }
    }

    // Just for develop testing :_)
    private List<string> init()
    {
        return NotesData.GetAllNoteList();
    }
}
