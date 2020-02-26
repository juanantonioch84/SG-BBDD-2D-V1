using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Ftest : MonoBehaviour
{
    public Text _notePrefab;

    private void Start()
    {
        _notePrefab.text = Path.GetDirectoryName(Application.dataPath);
    }
}