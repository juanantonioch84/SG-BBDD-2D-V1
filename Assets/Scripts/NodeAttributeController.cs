using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeAttributeController : NodeController
{
    private GameObject _node;

    private void Awake()
    {
        _node = null;
        _nodeType = NodeType.Attribute;
    }

    // Return if attribute is linked with the node
    public bool isLinked()
    {
        return _node != null;
    }

    // Use this method instead parent's controller method
    public override void OnPointerEnter(PointerEventData eventData)
    {
        // Is user is editing another node of a different type, set node to link/unlink
        if (IsAnotherNodeBeingEdited() && _nodeType != _diagramController.GetNodeType()) {

            // Set the node as the second node if another node is trying to be linked and this nodeattribute is not already linked
            if (IsAnotherNodeBeingLinked() && ! isLinked()) {
                _currentGraphic.color = Color.green;
                _diagramController.SetSecondNode(_rt);
            }
            // Set the node as the second node if another node is trying to be unlinked and this nodeattribute is already linked with the first node
            else if (!IsAnotherNodeBeingLinked() && isLinked() && ReferenceEquals(_diagramController.GetFirstNode().gameObject, _node)) {
                _currentGraphic.color = Color.red;
                _diagramController.SetSecondNode(_rt);
            }
        }
    }

    public override void OnPointerClick(PointerEventData e)
    {
        // With left click, link/unlink this node with the one which started the edition
        if (IsAnotherNodeBeingEdited() && e.button == PointerEventData.InputButton.Left) {

            // Link this node if another node is trying to be linked and they are not linked
            if (IsAnotherNodeBeingLinked() && ! isLinked()) {
                LinkNode();
            }
            // Unlink this node if another node is trying to be unlinked and they are linked
            else if (!IsAnotherNodeBeingLinked() && isLinked() && ReferenceEquals(_diagramController.GetFirstNode().gameObject, _node)) {
                UnlinkNode();
            }
        } else if (IsAnotherNodeBeingEdited()) {
            StopEditingNode();
        }
        // Right click open the menu
        if (e.button == PointerEventData.InputButton.Right) {
            _nodeMenu.GetComponent<NodeMenuController>().Init(gameObject, _canvas.transform.InverseTransformPoint(e.position));
        }
    }

    // Is the user clicks over link/unlink button, start editing
    public override void StartEditingNode(bool isLinking)
    {
        base.StartEditingNode(isLinking);

        // If it's unlinking, directly remove the only link
        if (!isLinking) {
            _diagramController.SetSecondNode(_node.GetComponent<RectTransform>());
            UnlinkNode();
        }
    }

    // Link the atribute with a node
    public void SetNode(GameObject node)
    {
        _node = node;
    }

    // Unlink the atribute
    public void UnsetNode()
    {
        _node = null;
    }
}
