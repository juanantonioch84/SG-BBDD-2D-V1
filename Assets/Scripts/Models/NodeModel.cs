using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeModel
{
    public float PosX { get; set; }
    public float PosY { get; set; }
    public NodeType NodeType { get; set; }
    public string NodeName { get; set; }
    public string Text { get; set; }

    public NodeModel(float posX, float posY, NodeType nodeType, string nodeName, string text)
    {
        PosX = posX;
        PosY = posY;
        NodeType = nodeType;
        NodeName = nodeName;
        Text = text;
    }

    public NodeModel(Vector2 position, NodeType nodeType, string nodeName, string text)
    {
        SetPosition(position);
        NodeType = nodeType;
        NodeName = nodeName;
        Text = text;
    }

    public void SetPosition(Vector2 position)
    {
        PosX = position.x;
        PosY = position.y;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(PosX, PosY);
    }

    public static string NodeTypeString(NodeType nodeType)
    {
        string nodeTypeString = "none";

        switch(nodeType) {
            case NodeType.Entity: nodeTypeString = "entity"; break;
            case NodeType.Attribute: nodeTypeString = "attribute"; break;
            case NodeType.Relation: nodeTypeString = "relation"; break;
        }

        return nodeTypeString;
    }
}
