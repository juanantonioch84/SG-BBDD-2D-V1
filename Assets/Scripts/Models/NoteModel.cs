using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteModel
{
    // Complete text of the note
    public string _text;

    // List with the highlighted words in the text
    public List<string> _highlights;

    // List with the words to be included as nodes in the diagram
    public List<string> _words;


    public NoteModel(string text, List<string> highlights, List<string> words)
    {
        _text = text;
        _text = text;
        _words = words;
    }

    public NoteModel(string text, string[] highlights, string[] words)
    {
        _text = text;

        _highlights = new List<string>();
        foreach (string highlight in highlights) {
            _highlights.Add(highlight);
        }

        _words = new List<string>();
        foreach (string word in words) {
            _words.Add(word);
        }
    }

    // Return the rich text of the note using the words
    public string GetRichText()
    {
        string richText = _text;

        // Mark each relevant word in the text
        foreach (string highlight in _highlights) {

            string[] substring = new string[] { highlight };
            string[] array = richText.Split(substring, System.StringSplitOptions.None);


            // If word exists ib the text
            if (array.Length >= 1) {

                richText = array[0];

                for (int i = 1; i < array.Length; i++) {
                    richText += "<b><color=orange>" + highlight + "</color></b>" + array[i];
                }
            }
        }

        return richText;
    }
}
