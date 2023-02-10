using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeBasedEditor : EditorWindow
{
    //[MenuItem("Window/ER Diagram Editor")]
    //private static void OpenWindow()
    //{
    //    NodeBasedEditor window = GetWindow<NodeBasedEditor>();
    //    window.titleContent = new GUIContent("ER Diagram Editor");
    //}

    [MenuItem("Window/ER Diagram Editor")]
    static void Init()
    {
        NodeBasedEditor window = (NodeBasedEditor)EditorWindow.GetWindow(typeof(NodeBasedEditor));
        window.titleContent = new GUIContent("ER Diagram Editor");
        window.Show();
    }

    private void OnGUI()
    {
        DrawNodes();

        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    private void DrawNodes()
    {
    }

    private void ProcessEvents(Event e)
    {
    }
}
