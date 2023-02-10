using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class EndController : MonoBehaviour
{
    public GameObject Inventory;
    public Text Msg;
    public Button Confirm;
    public Button Copy;
    public Button Exit;

    public GameObject _globalObject;

    private GlobalController globalController;

    void Start()
    {
        globalController = _globalObject.GetComponent<GlobalController>();
    }

    public void onConfirm()
    {
        SaveDiagramState();
        Inventory.SetActive(false);
        Confirm.gameObject.SetActive(false);

        Msg.text = "Los datos de su partida están en: " + System.Environment.NewLine + "<b>" + GameDataController.DataSet.Filepath + "</b>" + System.Environment.NewLine + "¡Gracias por jugar!";
        Msg.gameObject.SetActive(true);
        Copy.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
    }

    public void onCopyToClipboard()
    {
        GUIUtility.systemCopyBuffer = GameDataController.DataSet.Filepath;
    }

    public void onExit()
    {
        Application.Quit();
    }

    public void SaveDiagramState()
    {
        ERDiagram diagram = new ERDiagram();
        ActionInfo actionInfo = new ActionInfo();
        foreach (KeyValuePair<string, NodeModel> node in GlobalController.Instance.StoredNodes)
        {
            NodeType nodeType = node.Value.NodeType;
            switch (nodeType)
            {
                case NodeType.Entity:
                    diagram.entities.Add(node.Value.NodeName);

                    break;
                case NodeType.Attribute:
                    diagram.attributes.Add(node.Value.NodeName);
                    break;
                case NodeType.Relation:
                    diagram.relations.Add(node.Value.NodeName);
                    break;
            }
        }
        
        foreach (KeyValuePair<string, ERLine> erLine in GlobalController.Instance.StoredLines)
        {
            GlobalController.Instance.StoredNodes.TryGetValue(erLine.Value.Node1Name, out NodeModel node1);
            GlobalController.Instance.StoredNodes.TryGetValue(erLine.Value.Node2Name, out NodeModel node2);

            switch (node1.NodeType)
            {
                case NodeType.Entity:
                    if (node2.NodeType == NodeType.Relation)
                    {
                        diagram.ent_rel_links.Add(erLine.Value.Name);
                    }
                    else if (node2.NodeType == NodeType.Attribute)
                    {
                        diagram.ent_att_links.Add(erLine.Value.Name);
                    }
                    break;
                case NodeType.Attribute:
                    if (node2.NodeType == NodeType.Relation)
                    {
                        diagram.att_rel_links.Add(erLine.Value.Name);
                    }
                    else if (node2.NodeType == NodeType.Entity)
                    {
                        diagram.ent_att_links.Add(erLine.Value.Name);
                    }
                    break;
                case NodeType.Relation:
                    if (node2.NodeType == NodeType.Entity)
                    {
                        diagram.ent_rel_links.Add(erLine.Value.Name);
                    }
                    else if (node2.NodeType == NodeType.Attribute)
                    {
                        diagram.att_rel_links.Add(erLine.Value.Name);
                    }
                    break;
            }
            if (erLine.Value.EnabledCardinality)
            {
                diagram.cardinalities.Add(erLine.Value.Name, erLine.Value.Cardinality);
            }
        }

        GameDataController.Add("Exit_Game", actionInfo, diagram);
    }
}