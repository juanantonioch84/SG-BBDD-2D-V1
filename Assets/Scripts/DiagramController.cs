using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum EditionType { None, Link, Unlink }

public class DiagramController : MonoBehaviour
{
    public UILineRenderer _erLinePrefab;
    public GameObject erEntityPrefab;
    public GameObject erAttributePrefab;
    public GameObject erRelationPrefab;
    public GameObject wordsContainer;

    private EditionType _editionType;
    private NodeType _nodeType;
    private RectTransform _nodeFirst;
    private RectTransform _nodeSecond;


    // Start is called before the first frame update
    void Start()
    {
        _editionType = EditionType.None;
        _nodeType = NodeType.None;
        _nodeFirst = null;
        _nodeSecond = null;

        // Load last diagram state nodes
        foreach (KeyValuePair<string, NodeModel> node in GlobalController.Instance.StoredNodes) {

            GameObject nodeInstance = null;

            switch (node.Value.NodeType) {
                case NodeType.Entity:
                    nodeInstance = Instantiate(erEntityPrefab, Vector2.zero, Quaternion.identity) as GameObject;
                    break;
                case NodeType.Attribute:
                    nodeInstance = Instantiate(erAttributePrefab, Vector2.zero, Quaternion.identity) as GameObject;
                    break;
                case NodeType.Relation:
                    nodeInstance = Instantiate(erRelationPrefab, Vector2.zero, Quaternion.identity) as GameObject;
                    break;
            }

            nodeInstance.name = node.Key;
            nodeInstance.transform.SetParent(gameObject.transform, false);
            nodeInstance.transform.SetAsFirstSibling();
            nodeInstance.GetComponent<RectTransform>().localPosition = node.Value.GetPosition();
            nodeInstance.GetComponentInChildren<Text>().text = node.Value.Text;

            GameObject wordObject = wordsContainer.transform.Find(node.Key).gameObject;
            nodeInstance.GetComponent<NodeController>().SetWordOrigin(wordObject);

            // Load word state
            WordController wordController = wordObject.GetComponent<WordController>();
            wordController.NodeType = node.Value.NodeType;
            wordController.ChangeNodeType(wordController.NodeType);
            wordController.DisableWord();
        }

        // Load last diagram state er lines
        foreach (KeyValuePair<string, ERLine> erLine in GlobalController.Instance.StoredLines) {

            GameObject node1 = transform.Find(erLine.Value.Node1Name).gameObject;
            GameObject node2 = transform.Find(erLine.Value.Node2Name).gameObject;

            UILineRendererController uiLineRendererController = InstantiateERLine(node1, node2);
            if (erLine.Value.EnabledCardinality) {
                uiLineRendererController.Dropdown.value = erLine.Value.Cardinality;
            }
        }
    }

    void Update()
    {
        // With any click outside the nodes, turn off linking mode
        if (_nodeFirst && Input.GetMouseButtonDown(0) && _nodeSecond == null) {
            StopEditing();
        }
    }

    // Return the type of the edition
    public EditionType GetEditionType()
    {
        return _editionType;
    }

    // Return the first node
    public RectTransform GetFirstNode()
    {
        return _nodeFirst;
    }

    // Return the nodetype of the first node
    public NodeType GetNodeType()
    {
        return _nodeType;
    }

    public void StartEditing(RectTransform node, NodeType nodeType, bool isLinking)
    {
        _editionType = isLinking ? EditionType.Link : EditionType.Unlink;
        _nodeFirst = node;
        _nodeType = nodeType;
    }

    // Set the second node to be linked/unlinked
    public void SetSecondNode(RectTransform node)
    {
        _nodeSecond = node;
    }

    public void UnsetSecondNode()
    {
        if (_nodeSecond != null) {
            _nodeSecond.GetComponent<NodeController>().StopBeingEdited();
            _nodeSecond = null;
        }
    }

    // Stop the link/unlink of the nodes
    public void StopEditing()
    {
        if (_nodeSecond != null) {
            UnsetSecondNode();
        }

        _editionType = EditionType.None;
        _nodeType = NodeType.None;
        _nodeFirst.gameObject.GetComponent<NodeController>().StopBeingEdited();
        _nodeFirst = null;
    }

    // Create the line to Link the nodes
    public void LinkNodes()
    {
        // Check if those nodes are not already linked
        if (! GlobalController.Instance.ContainsERLine(_nodeFirst.gameObject.name, _nodeSecond.gameObject.name)) {

            // Instantiate the line object
            UILineRendererController uiLineRenderer = InstantiateERLine(_nodeFirst.gameObject, _nodeSecond.gameObject);

            // Add to the globalcontroller
            GlobalController.Instance.AddERLine(_nodeFirst.gameObject.name, _nodeSecond.gameObject.name, uiLineRenderer._cardinality.gameObject.activeSelf);

            // Store the action
            ActionInfo actionInfo = new ActionInfo();
            actionInfo.Add("node1_name", _nodeFirst.gameObject.name);
            actionInfo.Add("node1_type", NodeModel.NodeTypeString(_nodeFirst.GetComponent<NodeController>()._nodeType));
            actionInfo.Add("node2_name", _nodeSecond.gameObject.name);
            actionInfo.Add("node2_type", NodeModel.NodeTypeString(_nodeSecond.GetComponent<NodeController>()._nodeType));
            actionInfo.Add("line_name", uiLineRenderer.name);
            GameDataController.Add("add_erline", actionInfo);
        }
    }

    // Create the line to Link the nodes
    public UILineRendererController InstantiateERLine(GameObject node1, GameObject node2)
    {
        UILineRenderer _erLine = Instantiate(_erLinePrefab, Vector2.zero, Quaternion.identity) as UILineRenderer;
        _erLine.transform.SetParent(gameObject.transform, false);
        _erLine.transform.SetAsFirstSibling();
        _erLine.rectTransform.anchoredPosition = Vector2.zero;

        UILineRendererController uiLineRenderer = _erLine.GetComponent<UILineRendererController>();
        uiLineRenderer.SetLinkNodes(node1.GetComponent<RectTransform>(), node2.GetComponent<RectTransform>());
        bool cardinality = true;

        // If any node is an nodeattribute, set the node and hide the cardinality
        if (node1.GetComponent<NodeController>()._nodeType == NodeType.Attribute) {
            node1.GetComponent<NodeAttributeController>().SetNode(node2);
            cardinality = false;
        } else if (node2.GetComponent<NodeController>()._nodeType == NodeType.Attribute) {
            node2.GetComponent<NodeAttributeController>().SetNode(node1);
            cardinality = false;
        }

        uiLineRenderer._cardinality.gameObject.SetActive(cardinality);
        uiLineRenderer.name = ERLine.CreateName(node1.name, node2.name);

        return uiLineRenderer;
    }

    // Destroy the line which Link the nodes
    public void UnlinkNodes()
    {
        ERLine lineToRemove = GlobalController.Instance.SearchERLine(_nodeFirst.gameObject.name, _nodeSecond.gameObject.name);

        if (lineToRemove != null) {

            // Store the action
            ActionInfo actionInfo = new ActionInfo();
            actionInfo.Add("node1_name", _nodeFirst.gameObject.name);
            actionInfo.Add("node1_type", NodeModel.NodeTypeString(_nodeFirst.GetComponent<NodeController>()._nodeType));
            actionInfo.Add("node2_name", _nodeSecond.gameObject.name);
            actionInfo.Add("node2_type", NodeModel.NodeTypeString(_nodeSecond.GetComponent<NodeController>()._nodeType));
            actionInfo.Add("line_name", lineToRemove.Name);
            GameDataController.Add("destroy_erline", actionInfo);

            // Remove and destroy the erline
            GlobalController.Instance.RemoveERLine(lineToRemove, transform);
        }
    }
}
