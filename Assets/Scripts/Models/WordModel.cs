using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordModel
{
    public string Text { get; set; }
    public NodeType NodeType { get; set; }

    public WordModel(string text, NodeType nodeType = NodeType.None)
    {
        Text = text;
        NodeType = nodeType;
    }
}
