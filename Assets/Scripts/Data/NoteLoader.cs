using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLoader
{
    public static Dictionary<string, NoteModel> LoadNote(int building)
    {
        Dictionary<string, NoteModel> notes = new Dictionary<string, NoteModel>();
        string dictionaryPath = "Data/NoteDictionaryb" + building + ".json";
        NoteDictionary dictionary = JsonUtility.FromJson<NoteDictionary>(JsonFileUtility.LoadJsonFromFile(dictionaryPath, false));
        foreach (string dictionaryNotePath in dictionary.notes)
        {
            string myLoadedNoteModel = JsonFileUtility.LoadJsonFromFile(dictionaryNotePath, false);
            NoteModel myNoteModel = JsonUtility.FromJson<NoteModel>(myLoadedNoteModel);

            if (notes.ContainsKey(myNoteModel._noteID))
            {
                Debug.LogWarning("NoteModel " + myNoteModel + " Key already exsists with ID " + myNoteModel._noteID);
            }
            else
            {
                notes.Add(myNoteModel._noteID, myNoteModel);
            }
        }
        return notes;
    }
}
